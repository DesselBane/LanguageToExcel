using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DomainModels;

namespace Contracts.Factories
{
    public interface IFileFactory
    {
        IPropertiesFile GetPropertiesFile();
    }
}
