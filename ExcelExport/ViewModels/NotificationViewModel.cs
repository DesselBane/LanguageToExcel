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

        public NotificationViewModel()
        {
            _time = DateTime.Now;
        }

        public NotificationViewModel(string message) : this()
        {
            Message = message;
        }
    }
}
