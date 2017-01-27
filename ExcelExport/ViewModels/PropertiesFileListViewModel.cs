using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Services;

namespace ExcelExport.ViewModels
{
    public class PropertiesFileListViewModel : ViewModelBase
    {
        #region Vars

        private DelegateCommand _addFileCommand;
        private DelegateCommand<FileViewModel> _removeFileCommand;
        private ObservableCollection<FileViewModel> _selectedPropertyFiles = new ObservableCollection<FileViewModel>();

        #endregion

        #region Properties

        public ObservableCollection<FileViewModel> SelectedPropertyFiles => _selectedPropertyFiles;

        public ICommand AddFileCommand => _addFileCommand ?? (_addFileCommand = new DelegateCommand(OnAddFile, CanAddFile));
        public ICommand RemoveFileCommand => _removeFileCommand ?? (_removeFileCommand = new DelegateCommand<FileViewModel>(OnRemoveFile, CanRemoveFile));

        #endregion

        public PropertiesFileListViewModel()
        {
            
        }

        #region Command Handlers

        private void OnAddFile()
        {
            throw new NotImplementedException();
        }

        private bool CanAddFile()
        {
            throw new NotImplementedException();
        }

        private void OnRemoveFile(FileViewModel file)
        {
            throw new NotImplementedException();
        }

        private bool CanRemoveFile(FileViewModel file)
        {
            throw new NotImplementedException();
        }

        #endregion Command Handlers
    }
}