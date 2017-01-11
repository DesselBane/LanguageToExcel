using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DomainModels;
using Contracts.Services;

namespace Services
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
