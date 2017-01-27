using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Contracts.Presentation;
using ExcelExport.Contracts;
using ExcelExport.Contracts.Services;
using Prism.Commands;
using Prism.Events;
using Services;

namespace ExcelExport.ViewModels
{
    public class PropertiesFileListViewModel : ViewModelBase
    {
        #region Vars

        private DelegateCommand _addFileCommand;
        private DelegateCommand<FileViewModel> _removeFileCommand;
        private ObservableCollection<FileViewModel> _selectedPropertyFiles = new ObservableCollection<FileViewModel>();
        private IFileService _fileService;
        private IPropertiesFileService _propertiesFileService;
        private EventAggregator _eventAggregator;

        #endregion

        #region Properties

        public ObservableCollection<FileViewModel> SelectedPropertyFiles => _selectedPropertyFiles;

        public ICommand AddFileCommand => _addFileCommand ?? (_addFileCommand = new DelegateCommand(OnAddFile, CanAddFile));
        public ICommand RemoveFileCommand => _removeFileCommand ?? (_removeFileCommand = new DelegateCommand<FileViewModel>(OnRemoveFile, CanRemoveFile));

        #endregion

        public PropertiesFileListViewModel(IFileService fileService, IPropertiesFileService propertiesFileService, EventAggregator eventAggregator)
        {
            _fileService = fileService;
            _propertiesFileService = propertiesFileService;
            _eventAggregator = eventAggregator;
        }

        #region Command Handlers

        private void OnAddFile()
        {
            FileInfo fileinfo = _fileService.OpenFile();
            _propertiesFileService.AddPropertiesFile(fileinfo);
        }

        private bool CanAddFile()
        {
            return true;
        }

        private void OnRemoveFile(FileViewModel file)
        {
            _propertiesFileService.RemovePropertiesFile(file.DataObject);
        }

        private bool CanRemoveFile(FileViewModel file)
        {
            return file != null;
        }

        #endregion Command Handlers
    }
}