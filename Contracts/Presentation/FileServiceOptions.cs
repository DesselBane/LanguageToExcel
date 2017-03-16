using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Presentation
{
    public class FileServiceOptions : IFileServiceOptions
    {
        private bool _dereferenceLinks;
        private string _defaultExtensions;
        private string _filter;
        private string _initialDirectory;
        private string _title;

        #region Implementation of IFileServiceOptions

        public bool DereferenceLinks
        {
            get { return _dereferenceLinks; }
            set { _dereferenceLinks = value; }
        }

        public string DefaultExtensions
        {
            get { return _defaultExtensions; }
            set { _defaultExtensions = value; }
        }

        public string Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        public string InitialDirectory
        {
            get { return _initialDirectory; }
            set { _initialDirectory = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        #endregion
    }
}
