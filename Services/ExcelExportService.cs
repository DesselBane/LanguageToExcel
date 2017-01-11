using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DomainModels;
using Contracts.Services;
using OfficeOpenXml;

namespace Services
{
    public class ExcelExportService : IExcelExportService
    {
        private IExportPropertiesFileService _propertiesFileService;

        public ExcelExportService(IExportPropertiesFileService propertiesFileService)
        {
            _propertiesFileService = propertiesFileService;
        }

        #region Implementation of IExcelExportService

        public async Task ExportToExcelAsync(FileInfo outputFile)
        {
            var merged = await MergePropertiesFileyAsync(_propertiesFileService.RegisteredFiles().ToArray());

            if(outputFile.Exists)
                outputFile.Delete();

            var excel = new ExcelPackage(outputFile);

            await WriteToExcelAsync(merged,excel);

            excel.Save();
            
        }

        #endregion

        private Task<List<Tuple<string, string[]>>> MergePropertiesFileyAsync(params IPropertiesFile[] properties)
        {
            return Task.Run(() => MergePropertiesFiles(properties));
        }

        private List<Tuple<string,string[]>> MergePropertiesFiles(params IPropertiesFile[] properties)
        {
            var languages = new string[properties.Length];
            int position = 0;
            Dictionary<string,string[]> result = new Dictionary<string, string[]>();

            foreach (IPropertiesFile propertiesFile in properties)
            {
                languages[0] = propertiesFile.Language;

                if (propertiesFile.Data == null || propertiesFile.Data.Count == 0)
                    propertiesFile.ReadData();

                foreach (KeyValuePair<string, string> keyValuePair in propertiesFile.Data)
                {
                    if (!result.ContainsKey(keyValuePair.Key))
                        result.Add(keyValuePair.Key,new string[properties.Length]);

                    result[keyValuePair.Key][position] = keyValuePair.Value;
                }
                position++;
            }

            var returnValue = result.Select(x => new Tuple<string, string[]>(x.Key, x.Value)).OrderBy(x => x.Item1).ToList();
            returnValue.Insert(0,new Tuple<string, string[]>("Keys",languages));
            return returnValue;
        }

        private Task WriteToExcelAsync(List<Tuple<string, string[]>> mergedProperties,ExcelPackage excelPackage)
        {
            return Task.Run(() => WriteToExcel(mergedProperties,excelPackage));
        }

        private void WriteToExcel(List<Tuple<string,string[]>> mergedProperties,ExcelPackage excelPackage)
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
    }
}
