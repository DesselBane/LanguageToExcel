using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels
{
    public class MainViewModel : ViewModelBase
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
        private bool _isWorking;

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

        public bool IsWorking
        {
            get { return _isWorking; }
            private set
            {
                _isWorking = value; 
                FirePropertyChanged();
                _startExportCommand?.ReevaluatePermissions();
                _closeProgramCommand?.ReevaluatePermissions();
                _selectOutputFilePathCommand?.ReevaluatePermissions();
                _addNewSourceFileCommand?.ReevaluatePermissions();
                _removeSourceFileCommand?.ReevaluatePermissions();
                _openOutputFileCommand?.ReevaluatePermissions();
                
            }
        }

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
            
        }

        private bool CanCloseProgram() 
        {
            return true;
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
            var fileViewModel = new FileViewModel();
            SelectedInputFiles.Add(fileViewModel);
        }

        private bool CanAddNewSourceFile()
        {
            return !IsWorking;
        }

        private void OnRemoveSourceFile(object param)
        {
            SelectedInputFiles.Remove((FileViewModel) param);
        }

        private bool CanRemoveSourceFile(object param)
        {
            return param is FileViewModel && !IsWorking;
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