using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface INotificationService
    {
        event CollectionChangeEventHandler PropertiesFilesCollectionChanged;
        event EventHandler<Tuple<string, NotificationLevel>> Notification;
    }
}
