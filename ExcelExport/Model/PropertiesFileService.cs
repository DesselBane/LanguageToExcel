using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelExport.Contracts.Events;
using ExcelExport.Contracts.Model;
using ExcelExport.Contracts.Services;
using Prism.Events;

namespace ExcelExport.Model
{
    public class PropertiesFileService : IPropertiesFileService
    {
        #region Vars

        private EventAggregator _eventAggregator;
        private List<IPropertiesFile> _propertiesFiles = new List<IPropertiesFile>();

        #endregion

        #region Constructors

        public PropertiesFileService(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        #endregion

        public IPropertiesFile AddPropertiesFile(FileInfo fileInfo)
        {
            if (_propertiesFiles.Any(x => x.FilePath.FullName == fileInfo.FullName))
                throw new ArgumentException("This file path is already added");

            if(fileInfo == null || !fileInfo.Exists)
                throw new FileNotFoundException();

            var propFile = new PropertiesFile {FilePath = fileInfo};
            _propertiesFiles.Add(propFile);

            _eventAggregator.GetEvent<PubSubEvent<AddedPropertiesFileEvent>>().Publish(new AddedPropertiesFileEvent(propFile));

            return propFile;
        }

        public void RemovePropertiesFile(IPropertiesFile file)
        {
            if (!_propertiesFiles.Contains(file))
                throw new ArgumentException("No such file is tracked");

            _propertiesFiles.Remove(file);

            _eventAggregator.GetEvent<PubSubEvent<RemovedPropertiesFileEvent>>().Publish(new RemovedPropertiesFileEvent(file));
        }

        public IEnumerable<IPropertiesFile> RegisteredFiles()
        {
            return _propertiesFiles.ToArray();
        }

        #region Implementation of IPropertiesFileService

        public Task<IEnumerable<IReadOnlyDictionary<string, string>>> ParseFilesAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}