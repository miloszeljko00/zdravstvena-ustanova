using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class NotificationController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IEnumerable<Notification> GetAll()
        {
            return _notificationService.GetAll();
        }
        
        public Notification Create(Notification Notification)
        {
            return _notificationService.Create(Notification);
        }
        public bool Update(Notification Notification)
        {
            return _notificationService.Update(Notification);
        }
        public bool Delete(long NotificationId)
        {
            return _notificationService.Delete(NotificationId);
        }
        
    }
}
