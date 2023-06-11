using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<NotificationTour> GetAllNotificationsByGuest2(int guest2Id)
        {
            return notificationTourRepository.GetByGuest2Id(guest2Id);
        }

        public int GetNumberUnseenNotificationsByGuest2(int guest2Id)
        {
            int number = 0;
            foreach(NotificationTour notificationTour in GetAllNotificationsByGuest2(guest2Id))
            {
                if (notificationTour.Seen == false) number++;
            }
            return number;
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
