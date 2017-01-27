using System;
using System.Collections.Generic;
using ExcelExport.Contracts;

namespace ExcelExport.Model
{
    public class PropertiesFileService : IExportPropertiesFileService
    {
        #region Implementation of IExportPropertiesFileService

        public IPropertiesFile AddPropertiesFile()
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
