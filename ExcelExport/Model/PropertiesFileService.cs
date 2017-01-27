using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelExport.Contracts;
using ExcelExport.Contracts.Events;
using ExcelExport.Contracts.Model;
using ExcelExport.Contracts.Services;
using Prism.Events;

namespace ExcelExport.Model
{
    public class PropertiesFileService : IPropertiesFileService
    {
        private List<IPropertiesFile> _propertiesFiles = new List<IPropertiesFile>();
        private EventAggregator _eventAggregator;

        public PropertiesFileService(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        #region Implementation of IExportPropertiesFileService

        public IPropertiesFile AddPropertiesFile(FileInfo fileInfo)
        {
            if(_propertiesFiles.Any(x => x.FilePath.FullName == fileInfo.FullName))
                throw new ArgumentException("This file path is already added");

            

            var propFile = new PropertiesFile {FilePath = fileInfo};
            _propertiesFiles.Add(propFile);

            _eventAggregator.GetEvent<PubSubEvent<AddedPropertiesFileEvent>>().Publish(new AddedPropertiesFileEvent(propFile));

            return propFile;
        }

        public void RemovePropertiesFile(IPropertiesFile file)
        {
            if(!_propertiesFiles.Contains(file))
                throw new ArgumentException("No such file is tracked");

            _propertiesFiles.Remove(file);

            _eventAggregator.GetEvent<PubSubEvent<RemovedPropertiesFileEvent>>().Publish(new RemovedPropertiesFileEvent(file));
        }

        public IEnumerable<IPropertiesFile> RegisteredFiles()
        {
            return _propertiesFiles.ToArray();
        }

        #endregion
    }
}
