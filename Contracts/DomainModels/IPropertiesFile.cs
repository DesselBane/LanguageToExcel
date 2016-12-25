using System.IO;

namespace Contracts.DomainModels
{
    public interface IPropertiesFile
    {
        FileInfo FilePath { get; set; }
        string Language { get; set; }
    }
}
