using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelExport.Contracts.Model;
using ExcelExport.Contracts.Services;
using OfficeOpenXml;

namespace ExcelExport
{
    public class ExcelExportService : IExcelExportService
    {
        #region Vars

        private IPropertiesFileService _propertiesFileService;
        private IPropertyFileValidationService _propertyFileValidationService;

        #endregion

        #region Constructors

        public ExcelExportService(IPropertiesFileService propertiesFileService,IPropertyFileValidationService propertyFileValidationService)
        {
            _propertiesFileService = propertiesFileService;
            _propertyFileValidationService = propertyFileValidationService;
        }

        #endregion

        #region Implementation of IExcelExportService

        public async Task ExportToExcelAsync(FileInfo outputFile)
        {
            var files = _propertiesFileService.RegisteredFiles().ToArray();

            bool allOk = true;
            foreach (IPropertiesFile propertiesFile in files)
            {
                if ((await _propertyFileValidationService.ValidateFile(propertiesFile)).Any())
                    allOk = false;
            }

            if(!allOk)
                throw new ValidationException("One or more files are invalid");

            var merged = await MergePropertiesFileyAsync(files).ConfigureAwait(false);

            if (outputFile.Exists)
                outputFile.Delete();

            var excel = new ExcelPackage(outputFile);

            await WriteToExcelAsync(merged, excel).ConfigureAwait(false);

            excel.Save();
        }

        #endregion

        #region Worker Methods

        private async Task<List<Tuple<string, string[]>>> MergePropertiesFileyAsync(params IPropertiesFile[] properties)
        {
            var languages = new string[properties.Length];
            int position = 0;
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();

            foreach (IPropertiesFile propertiesFile in properties)
            {
                languages[position] = propertiesFile.Language;

                var data = await _propertiesFileService.ParseFileAsync(propertiesFile).ConfigureAwait(false);

                foreach (KeyValuePair<string, string> keyValuePair in data)
                {
                    if (!result.ContainsKey(keyValuePair.Key))
                        result.Add(keyValuePair.Key, new string[properties.Length]);

                    result[keyValuePair.Key][position] = keyValuePair.Value;
                }
                position++;
            }

            var returnValue = result.Select(x => new Tuple<string, string[]>(x.Key, x.Value)).OrderBy(x => x.Item1).ToList();
            returnValue.Insert(0, new Tuple<string, string[]>("Keys", languages));
            return returnValue;
        }

        private Task WriteToExcelAsync(IEnumerable<Tuple<string, string[]>> mergedProperties, ExcelPackage excelPackage)
        {
            return Task.Run(() => WriteToExcel(mergedProperties, excelPackage));
        }

        private void WriteToExcel(IEnumerable<Tuple<string, string[]>> mergedProperties, ExcelPackage excelPackage)
        {
            ExcelWorksheet sheet = excelPackage.Workbook.Worksheets.Add("Languages");

            int lineCount = 1;

            foreach (Tuple<string, string[]> mergedProperty in mergedProperties)
            {
                sheet.Cells["A" + lineCount].Value = mergedProperty.Item1;
                var values = mergedProperty.Item2;

                for (int i = 0; i < values.Length; i++)
                {
                    char pos = (char) ('B' + i);
                    sheet.Cells[pos + lineCount.ToString()].Value = values[i];
                }
                lineCount++;
            }
        }

        #endregion Worker Methods
    }
}