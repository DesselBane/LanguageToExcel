using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelExport.Contracts.Events;
using ExcelExport.Contracts.Model;
using ExcelExport.Contracts.Services;
using Prism.Events;

namespace ExcelExport.Files
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

            if (fileInfo == null || !fileInfo.Exists)
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

        public Task<IReadOnlyDictionary<string, string>> ParseFileAsync(IPropertiesFile file)
        {
            return Task.Run(() =>
            {
                var dataWriteable = new Dictionary<string, string>();

                var streamReader = new StreamReader(file.FilePath.OpenRead());
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    var strings = line.Split('=');

                    dataWriteable.Add(strings[0], strings[1]);
                }

                return new ReadOnlyDictionary<string, string>(dataWriteable) as IReadOnlyDictionary<string, string>;
            });
        }
    }
}