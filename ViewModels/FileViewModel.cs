using System;
using System.Windows.Input;
using Contracts.DomainModels;
using Contracts.Factories;
using Contracts.Presentation;
using ViewModels.Commands;

namespace ViewModels
{
    public class FileViewModel : ViewModelBase
    {
        #region Vars

        private IPropertiesFile _propertiesFile;
        private RelayCommand _selectFilePath;
        private bool _canInteract;
        private IFileService _fileService;
        private RelayCommand _validateCommand;

        #endregion

        #region Properties

        public IPropertiesFile DataObject => _propertiesFile;

        public string FilePath => _propertiesFile.FilePath?.FullName;

        public ICommand SelectFilePath => _selectFilePath ?? (_selectFilePath = new RelayCommand(OnSelectFilePath, CanSelectFilePath));

        public ICommand Validate => _validateCommand ?? (_validateCommand = new RelayCommand(OnValidate, CanValidate));

        public string Language
        {
            get { return _propertiesFile.Language; }
            set
            {
                _propertiesFile.Language = value; 
                FirePropertyChanged();
            }
        }

        public bool CanInteract
        {
            get { return _canInteract; }
            internal set
            {
                _canInteract = value;
                FirePropertyChanged();
                _selectFilePath?.ReevaluatePermissions();
                _validateCommand?.ReevaluatePermissions();
            }
        }

        #endregion

        #region Constructors

        public FileViewModel(IFileFactory fileFactory,IFileService fileService)
        {
            _propertiesFile = fileFactory.GetPropertiesFile();
            _fileService = fileService;
        }

        #endregion

        #region Command Handler

        private void OnSelectFilePath()
        {
            _propertiesFile.FilePath = _fileService.OpenFile();
        }

        private bool CanSelectFilePath()
        {
            return CanInteract;
        }

        private void OnValidate()
        {
            throw new NotImplementedException();
        }

        private bool CanValidate()
        {
            throw new NotImplementedException();
        }

        #endregion Command Handler
    }
}