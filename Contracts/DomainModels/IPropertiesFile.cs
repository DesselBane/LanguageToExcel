using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace Contracts.DomainModels
{
    public interface IPropertiesFile
    {
        FileInfo FilePath { get; set; }
        string Language { get; set; }
        IReadOnlyDictionary<string,string> Data { get; }

        IEnumerable<ValidationResult> ReadData();
        Task<IEnumerable<ValidationResult>> ReadDataAsync();
    }
}
