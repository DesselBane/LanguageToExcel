using System.Collections.Generic;

namespace ExcelExport.Contracts
{
    public interface IExportPropertiesFileService
    {
        IPropertiesFile AddPropertiesFile();
        void RemovePropertiesFile(IPropertiesFile file);
        IEnumerable<IPropertiesFile> RegisteredFiles();
    }
}
