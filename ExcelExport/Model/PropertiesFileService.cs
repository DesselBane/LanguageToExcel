using System;
using System.Collections.Generic;
using System.IO;
using ExcelExport.Contracts;
using ExcelExport.Contracts.Model;
using ExcelExport.Contracts.Services;

namespace ExcelExport.Model
{
    public class PropertiesFileService : IPropertiesFileService
    {
        #region Implementation of IExportPropertiesFileService

        public IPropertiesFile AddPropertiesFile(FileInfo fileInfo)
        {
            throw new NotImplementedException();
        }

        public void RemovePropertiesFile(IPropertiesFile file)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPropertiesFile> RegisteredFiles()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
