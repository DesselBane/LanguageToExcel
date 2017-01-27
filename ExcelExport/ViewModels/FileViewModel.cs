using System;
using System.Windows.Input;
using Contracts.Presentation;
using ExcelExport.Contracts;
using ExcelExport.Contracts.Model;
using Prism.Commands;
using Services;

namespace ExcelExport.ViewModels
{
    public class FileViewModel : ViewModelBase
    {
        #region Vars

        private IPropertiesFile _propertiesFile;
        private DelegateCommand _selectFilePath;
        private bool _canInteract;
        private IFileService _fileService;
        private DelegateCommand _validateCommand;

        #endregion

        #region Properties

        public IPropertiesFile DataObject => _propertiesFile;

        public string FilePath => _propertiesFile.FilePath?.FullName;

        public ICommand SelectFilePath => _selectFilePath ?? (_selectFilePath = new DelegateCommand(OnSelectFilePath, CanSelectFilePath));

        public ICommand Validate => _validateCommand ?? (_validateCommand = new DelegateCommand(OnValidate, CanValidate));

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
                _selectFilePath?.RaiseCanExecuteChanged();
                _validateCommand?.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Constructors

        public FileViewModel(IFileService fileService)
        {
            //TODO create file model
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