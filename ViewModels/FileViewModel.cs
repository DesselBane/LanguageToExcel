using System;
using System.Windows.Input;
using Contracts.DomainModels;
using Contracts.Factories;
using ViewModels.Commands;

namespace ViewModels
{
    public class FileViewModel : ViewModelBase
    {
        #region Vars

        private IPropertiesFile _propertiesFile;
        private RelayCommand _selectFilePath;
        private bool _canInteract;

        #endregion

        #region Properties

        public IPropertiesFile DataObject => _propertiesFile;

        public string FilePath => _propertiesFile.FilePath?.FullName;

        public ICommand SelectFilePath => _selectFilePath ?? (_selectFilePath = new RelayCommand(OnSelectFilePath, CanSelectFilePath));

        public bool CanInteract
        {
            get { return _canInteract; }
            internal set
            {
                _canInteract = value;
                FirePropertyChanged();
                _selectFilePath?.ReevaluatePermissions();
            }
        }

        #endregion

        #region Constructors

        public FileViewModel(IFileFactory fileFactory)
        {
            _propertiesFile = fileFactory.GetPropertiesFile();
        }

        #endregion

        #region Command Handler

        private void OnSelectFilePath()
        {
            throw new NotImplementedException();
        }

        private bool CanSelectFilePath()
        {
            return CanInteract;
        }

        #endregion Command Handler
    }
}