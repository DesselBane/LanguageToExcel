using System.Windows.Input;
using ExcelExport.Contracts.Model;
using ExcelExport.Contracts.Services;
using Prism.Commands;
using Services;

namespace ExcelExport.Files
{
    public class FileViewModel : ViewModelBase
    {
        #region Vars

        private IPropertiesFile _propertiesFile;
        private IPropertiesFileService _propertiesFileService;
        private DelegateCommand _removeFileCommand;

        #endregion

        #region Properties

        public IPropertiesFile DataObject => _propertiesFile;

        public string FilePath => _propertiesFile.FilePath?.FullName;

        public string Language
        {
            get { return _propertiesFile.Language; }
            set
            {
                _propertiesFile.Language = value; 
                FirePropertyChanged();
            }
        }

        public ICommand RemoveFile => _removeFileCommand ?? (_removeFileCommand = new DelegateCommand(OnRemoveFile, CanRemoveFile));

        #endregion

        #region Constructors

        public FileViewModel(IPropertiesFile propFile, IPropertiesFileService propertiesFileService)
        {
            _propertiesFile = propFile;
            _propertiesFileService = propertiesFileService;

        }

        #endregion

        #region Command Handlers

        private void OnRemoveFile()
        {
            try
            {
                _propertiesFileService.RemovePropertiesFile(DataObject);
            }
            catch
            {
                // ignored
            }
        }

        private bool CanRemoveFile()
        {
            return DataObject != null;
        }

        #endregion Command Handlers
    }
}