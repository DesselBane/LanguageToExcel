using System.Collections.Generic;
using System.IO;
using ExcelExport.Contracts.Model;

namespace ExcelExport.Contracts.Services
{
    public interface IPropertiesFileService
    {
        IPropertiesFile AddPropertiesFile(FileInfo fileInfo);
        void RemovePropertiesFile(IPropertiesFile file);
        IEnumerable<IPropertiesFile> RegisteredFiles();
    }
}
