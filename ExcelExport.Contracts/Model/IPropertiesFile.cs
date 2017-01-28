using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace ExcelExport.Contracts.Model
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
