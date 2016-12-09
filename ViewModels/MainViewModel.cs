using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels
{
    public class MainViewModel
    {
        #region Vars

        private readonly ObservableCollection<NotificationListItemViewModel> _notificationListItems = new ObservableCollection<NotificationListItemViewModel>();
        private readonly ObservableCollection<FileViewModel> _selectedInputFiles = new ObservableCollection<FileViewModel>();
        private RelayCommand _startExportCommand;
        private RelayCommand _closeProgramCommand;
        private RelayCommand _selectOutputFilePathCommand;
        private RelayCommand _addNewSourceFileCommand;
        private RelayCommand _removeSourceFileCommand;
        private RelayCommand _openOutputFileCommand;

        #endregion

        #region Properties

        public ObservableCollection<FileViewModel> SelectedInputFiles => _selectedInputFiles;

        public ObservableCollection<NotificationListItemViewModel> NotificationListItems => _notificationListItems;

        public ICommand StartExport => _startExportCommand ?? (_startExportCommand = new RelayCommand(OnStartExport, CanStartExport));
        public ICommand CloseProgram => _closeProgramCommand ?? (_closeProgramCommand = new RelayCommand(OnCloseProgram, CanCloseProgram));
        public ICommand SelectOutputFilePath => _selectOutputFilePathCommand ?? (_selectOutputFilePathCommand = new RelayCommand(OnSelectOutputFile, CanSelectOutputFile));
        public ICommand AddNewSourceFile => _addNewSourceFileCommand ?? (_addNewSourceFileCommand = new RelayCommand(OnAddNewSourceFile, CanAddNewSourceFile));
        public ICommand RemoveSourceFile => _removeSourceFileCommand ?? (_removeSourceFileCommand = new RelayCommand(OnRemoveSourceFile, CanRemoveSourceFile));
        public ICommand OpenOutputFile => _openOutputFileCommand ?? (_openOutputFileCommand = new RelayCommand(OnOpenOutputFile, CanOpenOutputFile));

        #endregion

        #region Command Handler

        private void OnStartExport()
        {
            throw new NotImplementedException();
        }

        private bool CanStartExport()
        {
            throw new NotImplementedException();
        }

        private void OnCloseProgram()
        {
            throw new NotImplementedException();
        }

        private bool CanCloseProgram()
        {
            throw new NotImplementedException();
        }

        private void OnSelectOutputFile()
        {
            throw new NotImplementedException();
        }

        private bool CanSelectOutputFile()
        {
            throw new NotImplementedException();
        }

        private void OnAddNewSourceFile()
        {
            throw new NotImplementedException();
        }

        private bool CanAddNewSourceFile()
        {
            throw new NotImplementedException();
        }

        private void OnRemoveSourceFile()
        {
            throw new NotImplementedException();
        }

        private bool CanRemoveSourceFile()
        {
            throw new NotImplementedException();
        }

        private void OnOpenOutputFile()
        {
            throw new NotImplementedException();
        }

        private bool CanOpenOutputFile()
        {
            throw new NotImplementedException();
        }

        #endregion Command Handler
    }
}