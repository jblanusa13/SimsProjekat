using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class NotificationTourService
    {
        private INotificationTourRepository notificationTourRepository;
        public NotificationTourService()
        {
            notificationTourRepository = Injector.CreateInstance<INotificationTourRepository>();           
        }
        public int NextId()
        {
            return notificationTourRepository.NextId();
        }
        public List<NotificationTour> GetAllNotifications()
        {
            return notificationTourRepository.GetAll();
        }
        public NotificationTour GetById(int id)
        {
            return notificationTourRepository.GetById(id);
        }

        public void Create(NotificationTour notificationTour)
        {
            notificationTourRepository.Create(notificationTour);
        }
        public void Delete(NotificationTour notificationTour)
        {
            notificationTourRepository.Remove(notificationTour);
        }
        public void Update(NotificationTour notificationTour)
        {
            notificationTourRepository.Update(notificationTour);
        }
        public void Subscribe(IObserver observer)
        {
            notificationTourRepository.Subscribe(observer);
        }
    }
}
