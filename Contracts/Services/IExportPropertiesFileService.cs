using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DomainModels;

namespace Contracts.Services
{
    public interface IExportPropertiesFileService
    {
        IPropertiesFile AddPropertiesFile();
        void RemovePropertiesFile(IPropertiesFile file);
        IEnumerable<IPropertiesFile> RegisteredFiles();
    }
}
