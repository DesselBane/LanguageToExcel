using System.IO;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IExcelExportService
    {
        Task ExportToExcelAsync(FileInfo outputFile);
    }
}
