using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Presentation
{
    public interface IFileServiceOptions
    {
        bool DereferenceLinks { get; }
        string DefaultExtensions { get; }
        string Filter { get; }
        string InitialDirectory { get; }
        string Title { get; }
    }
}
