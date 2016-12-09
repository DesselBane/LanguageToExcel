using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Presentation
{
    public interface IFileService
    {
        FileInfo OpenFile(IFileServiceOptions options = null);
        FileInfo SaveFile(IFileServiceOptions options = null);
    }
}
