using System.IO;

namespace Contracts.Presentation
{
    public interface IFileService
    {
        FileInfo OpenFile(IFileServiceOptions options = null);
        FileInfo SaveFile(IFileServiceOptions options = null);
    }
}
