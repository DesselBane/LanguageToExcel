using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Services;

namespace ExcelExport.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Vars

        private readonly ObservableCollection<FileViewModel> _selectedInputFiles = new ObservableCollection<FileViewModel>();
        private DelegateCommand _addNewSourceFileCommand;
        private DelegateCommand _closeProgramCommand;
        private bool _isWorking;
        private DelegateCommand _openOutputFileCommand;
        private DelegateCommand<FileViewModel> _removeSourceFileCommand;
        private DelegateCommand _selectOutputFilePathCommand;
        private DelegateCommand _startExportCommand;

        #endregion

        #region Properties

        public ObservableCollection<FileViewModel> SelectedInputFiles => _selectedInputFiles;

        public ICommand StartExport => _startExportCommand ?? (_startExportCommand = new DelegateCommand(OnStartExport, CanStartExport));
        public ICommand CloseProgram => _closeProgramCommand ?? (_closeProgramCommand = new DelegateCommand(OnCloseProgram, CanCloseProgram));
        public ICommand SelectOutputFilePath => _selectOutputFilePathCommand ?? (_selectOutputFilePathCommand = new DelegateCommand(OnSelectOutputFile, CanSelectOutputFile));
        public ICommand AddNewSourceFile => _addNewSourceFileCommand ?? (_addNewSourceFileCommand = new DelegateCommand(OnAddNewSourceFile, CanAddNewSourceFile));
        public ICommand RemoveSourceFile => _removeSourceFileCommand ?? (_removeSourceFileCommand = new DelegateCommand<FileViewModel>(OnRemoveSourceFile, CanRemoveSourceFile));
        public ICommand OpenOutputFile => _openOutputFileCommand ?? (_openOutputFileCommand = new DelegateCommand(OnOpenOutputFile, CanOpenOutputFile));

        public bool IsWorking
        {
            get { return _isWorking; }
            private set
            {
                _isWorking = value;
                FirePropertyChanged();
                _startExportCommand?.RaiseCanExecuteChanged();
                _closeProgramCommand?.RaiseCanExecuteChanged();
                _selectOutputFilePathCommand?.RaiseCanExecuteChanged();
                _addNewSourceFileCommand?.RaiseCanExecuteChanged();
                _removeSourceFileCommand?.RaiseCanExecuteChanged();
                _openOutputFileCommand?.RaiseCanExecuteChanged();
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
            //TODO create fileViewModel
            SelectedInputFiles.Add(null);
        }

        private bool CanAddNewSourceFile()
        {
            return !IsWorking;
        }

        private void OnRemoveSourceFile(FileViewModel param)
        {
            SelectedInputFiles.Remove(param);
        }

        private bool CanRemoveSourceFile(FileViewModel param)
        {
            return param != null && !IsWorking;
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