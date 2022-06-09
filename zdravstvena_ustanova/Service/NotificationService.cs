using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public IEnumerable<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public Notification Create(Notification Notification)
        {
            return _notificationRepository.Create(Notification);
        }
        public bool Update(Notification Notification)
        {
            return _notificationRepository.Update(Notification);
        }
        public bool Delete(long NotificationId)
        {
            return _notificationRepository.Delete(NotificationId);
        }

        public Notification Get(long id)
        {
            return _notificationRepository.Get(id);
        }
    }
}
