using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Contracts.Presentation;
using ExcelExport.Contracts.Services;
using Prism.Commands;
using Services;

namespace ExcelExport.ViewModels
{
    public class ControlsViewModel : ViewModelBase
    {
        #region Vars

        private IExcelExportService _excelExportService;
        private IFileService _fileService;
        private DelegateCommand _openCommand;
        private FileInfo _outputPath;
        private DelegateCommand _runCommand;
        private DelegateCommand _selectOutputPathCommand;
        private bool _isWorking;

        #endregion

        #region Properties

        public string OutputPath => _outputPath?.FullName;
        public ICommand SelectOutputPath => _selectOutputPathCommand ?? (_selectOutputPathCommand = new DelegateCommand(OnSelectOutputPath, CanSelectOutputPath));
        public ICommand Run => _runCommand ?? (_runCommand = new DelegateCommand(OnRun, CanRun));
        public ICommand Open => _openCommand ?? (_openCommand = new DelegateCommand(OnOpen, CanOpen));

        public bool IsWorking
        {
            get { return _isWorking; }
            set
            {
                _isWorking = value;
                FirePropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public ControlsViewModel(IFileService fileService, IExcelExportService excelExportService)
        {
            _fileService = fileService;
            _excelExportService = excelExportService;

            PropertyChanged += OnPropertyChanged;
        }

        #endregion

        #region Events

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IsWorking):
                    _selectOutputPathCommand.RaiseCanExecuteChanged();
                    _runCommand.RaiseCanExecuteChanged();
                    _openCommand.RaiseCanExecuteChanged();
                    break;

                case nameof(OutputPath):
                    _openCommand.RaiseCanExecuteChanged();
                    _runCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        #endregion Events

        #region Command Handler

        private void OnSelectOutputPath()
        {
            IsWorking = true;
            FileInfo newOutput = _fileService.SaveFile(new FileServiceOptions
            {
                DefaultExtensions = ".xlsx",
                DereferenceLinks = true,
                Filter = "Excel Workbook|*.xlsx"
            });

            if (newOutput == null)
            {
                IsWorking = true;
                return;
            }
            _outputPath = newOutput;

            if(_outputPath.Extension != ".xlsx")
                _outputPath = new FileInfo(_outputPath.FullName + ".xlsx");

            FirePropertyChanged(nameof(OutputPath));
            IsWorking = false;
        }

        private bool CanSelectOutputPath()
        {
            return !IsWorking;
        }

        private async void OnRun()
        {
            IsWorking = true;
            await _excelExportService.ExportToExcelAsync(_outputPath);
            _outputPath.Refresh();
            IsWorking = false;
        }

        private bool CanRun()
        {
            return !IsWorking && !string.IsNullOrWhiteSpace(OutputPath);
        }

        private void OnOpen()
        {
            try
            {
                Process.Start(OutputPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //throw new NotImplementedException();
            }
        }

        private bool CanOpen()
        {
            return !IsWorking && !string.IsNullOrWhiteSpace(OutputPath) && OutputPath.EndsWith(".xlsx") && _outputPath != null && _outputPath.Exists;
        }

        #endregion Command Handler
    }
}