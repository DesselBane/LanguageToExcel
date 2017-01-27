using System.IO;
using System.Threading.Tasks;

namespace ExcelExport.Contracts
{
    public interface IExcelExportService
    {
        Task ExportToExcelAsync(FileInfo outputFile);
    }
}
