using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Contracts.Presentation;
using ExcelExport.Contracts.Services;
using Prism.Commands;
using Services;

namespace ExcelExport.ViewModels
{
    public class ControlsViewModel : ViewModelBase
    {
        private FileInfo _outputPath;
        private DelegateCommand _selectOutputPathCommand;
        private IFileService _fileService;
        private DelegateCommand _runCommand;
        private IExcelExportService _excelExportService;

        public string OutputPath => _outputPath?.FullName;
        public ICommand SelectOutputPath => _selectOutputPathCommand ?? (_selectOutputPathCommand = new DelegateCommand(OnSelectOutputPath,CanSelectOutputPath));
        public ICommand Run => _runCommand ?? (_runCommand = new DelegateCommand(OnRun, CanRun));

        public ControlsViewModel(IFileService fileService, IExcelExportService excelExportService)
        {
            _fileService = fileService;
            _excelExportService = excelExportService;
        }

        #region Command Handler

        private void OnSelectOutputPath()
        {
            _outputPath = _fileService.SaveFile();
            FirePropertyChanged(nameof(OutputPath));
        }
        private bool CanSelectOutputPath()
        {
            return true;
        }

        private async void OnRun()
        {
            await _excelExportService.ExportToExcelAsync(_outputPath);
        }

        private bool CanRun()
        {
            return true;
        }

        #endregion Command Handler

    }
}
