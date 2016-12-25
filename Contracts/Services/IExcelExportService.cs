using System.IO;
using System.Threading.Tasks;
using Contracts.DomainModels;
using OfficeOpenXml;

namespace Contracts.Services
{
    public interface IExcelExportService
    {
        Task<ExcelPackage> ExportToExcelAsync(FileInfo outputFile, params IPropertiesFile[] properties);
    }
}
