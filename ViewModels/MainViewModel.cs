using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels
{
    public class MainViewModel
    {
        private readonly ObservableCollection<FileViewModel> _selectedInputFiles = new ObservableCollection<FileViewModel>();
        private readonly ObservableCollection<NotificationListItemViewModel> _notificationListItems = new ObservableCollection<NotificationListItemViewModel>();

        public ObservableCollection<FileViewModel> SelectedInputFiles => _selectedInputFiles;

        public ObservableCollection<NotificationListItemViewModel> NotificationListItems => _notificationListItems;

        public ICommand StartExport { get; }
        public ICommand CloseProgramm { get; }
        public ICommand SelectOutputFilePath { get; }
        public ICommand AddNewSourceFile { get; }
        public ICommand RemoveSourceFile { get; }
        public ICommand OpenOutputFile { get; }

        public MainViewModel()
        {
            
        }
    }
}
