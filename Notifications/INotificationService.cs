using System;
using System.ComponentModel;

namespace Notifications
{
    public interface INotificationService
    {
        event CollectionChangeEventHandler PropertiesFilesCollectionChanged;
        event EventHandler<Tuple<string, NotificationLevel>> Notification;
    }
}
