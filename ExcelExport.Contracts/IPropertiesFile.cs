using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ExcelExport.Contracts
{
    public interface IPropertiesFile
    {
        FileInfo FilePath { get; set; }
        string Language { get; set; }
        IReadOnlyDictionary<string,string> Data { get; }

        void ReadData();
        Task ReadDataAsync();
    }
}
