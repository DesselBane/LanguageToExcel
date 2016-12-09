using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DomainModels
{
    public interface IPropertiesFile
    {
        FileInfo FilePath { get; set; }
        string Language { get; set; }
    }
}
