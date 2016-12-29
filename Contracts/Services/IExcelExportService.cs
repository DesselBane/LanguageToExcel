using System.IO;
using System.Threading.Tasks;
using Contracts.DomainModels;

namespace Contracts.Services
{
    public interface IExcelExportService
    {
        Task ExportToExcelAsync(FileInfo outputFile, params IPropertiesFile[] properties);
    }
}
