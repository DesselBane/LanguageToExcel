using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DomainModels;

namespace Contracts.Services
{
    public interface IExcelExportService
    {
        Task ExportToExcelAsync(FileInfo outputFile, params IPropertiesFile[] properties);
    }
}
