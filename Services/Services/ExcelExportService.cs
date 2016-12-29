using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DomainModels;
using Contracts.Services;
using OfficeOpenXml;

namespace Services.Services
{
    public class ExcelExportService : IExcelExportService
    {
        #region Implementation of IExcelExportService

        public async Task ExportToExcelAsync(FileInfo outputFile, params IPropertiesFile[] properties)
        {
            var merged = await MergePropertiesFileyAsync(properties);

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
                {
                    var valid = propertiesFile.ReadData();
                    if(valid.Any())
                        throw new ValidationException();
                }

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
