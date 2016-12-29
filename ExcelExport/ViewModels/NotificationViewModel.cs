using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace ExcelExport.ViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private string _message;
        private readonly DateTime _time;
        private NotificationLevel _level;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value; 
                FirePropertyChanged();
            }
        }

        public DateTime Time => _time;

        public NotificationLevel Level
        {
            get { return _level; }
            set
            {
                _level = value; 
                FirePropertyChanged();
            }
        }

        public NotificationViewModel()
        {
            _time = DateTime.Now;
        }

        public NotificationViewModel(string message) : this()
        {
            Message = message;
        }

        public NotificationViewModel(string message, NotificationLevel level) : this(message)
        {
            Level = level;
        }
    }
}
