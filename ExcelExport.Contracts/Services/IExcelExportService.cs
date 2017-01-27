using System.IO;
using System.Threading.Tasks;

namespace ExcelExport.Contracts.Services
{
    public interface IExcelExportService
    {
        Task ExportToExcelAsync(FileInfo outputFile);
    }
}
