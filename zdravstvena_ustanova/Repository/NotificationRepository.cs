using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class NotificationRepository: INotificationRepository
    {
        private const string NOT_FOUND_ERROR = "NOTIFICATION NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _notificationMaxId;

        public NotificationRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _notificationMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Notification> notifications)
        {
            return notifications.Count() == 0 ? 0 : notifications.Max(notification => notification.Id);
        }

        public IEnumerable<Notification> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToNotification)
                .ToList();
        }

        public Notification Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(notification => notification.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Notification Create(Notification notification)
        {
            notification.Id = ++_notificationMaxId;
            AppendLineToFile(_path, NotificationToCSVFormat(notification));
            return notification;
        }
        public bool Update(Notification notification)
        {
            var notifications = GetAll();

            foreach (Notification n in notifications)
            {
                if (n.Id == notification.Id)
                {
                    n.Message = notification.Message;
                    n.Receiver = notification.Receiver;
                    WriteLinesToFile(_path, NotificationsToCSVFormat((List<Notification>)notifications));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long NotificationId)
        {
            var Notifications = (List<Notification>)GetAll();

            foreach (Notification n in Notifications)
            {
                if (n.Id == NotificationId)
                {
                    Notifications.Remove(n);
                    WriteLinesToFile(_path, NotificationsToCSVFormat((List<Notification>)Notifications));
                    return true;
                }
            }
            return false;
        }
        private string NotificationToCSVFormat(Notification Notification)
        {
            return string.Join(_delimiter,
                Notification.Id,
                Notification.Receiver.Id,
                Notification.Message
                );
        }

        private Notification CSVFormatToNotification(string NotificationCSVFormat)
        {
            var tokens = NotificationCSVFormat.Split(_delimiter.ToCharArray());
            return new Notification(
                long.Parse(tokens[0]),
                new Account(long.Parse(tokens[1])),
                tokens[2]
                );
        }
        private List<string> NotificationsToCSVFormat(List<Notification> Notifications)
        {
            List<string> lines = new List<string>();

            foreach (Notification Notification in Notifications)
            {
                lines.Add(NotificationToCSVFormat(Notification));
            }
            return lines;
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }
    }
}
