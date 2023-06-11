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
    public class NotificationOwnerGuestService
    {
        private INotificationOwnerGuestRepository notificationRepository;
        public NotificationOwnerGuestService()
        {
            notificationRepository = Injector.CreateInstance<INotificationOwnerGuestRepository>();           
        }
        public int NextId()
        {
            return notificationRepository.NextId();
        }
        public List<NotificationOwnerGuest> GetAllNotifications()
        {
            return notificationRepository.GetAll();
        }
        public NotificationOwnerGuest GetById(int id)
        {
            return notificationRepository.GetById(id);
        }
/*
        public int GetNumberUnseenNotificationsByGuest2(int guest2Id)
        {
            int number = 0;
            foreach(NotificationOwnerGuest notificationTour in GetAllNotificationsByGuest2(guest2Id))
            {
                if (notificationTour.Seen == false) number++;
            }
            return number;
        }
*/
        public void Create(NotificationOwnerGuest notificationTour)
        {
            notificationRepository.Create(notificationTour);
        }
        public void Delete(NotificationOwnerGuest notificationTour)
        {
            notificationRepository.Remove(notificationTour);
        }
        public void Update(NotificationOwnerGuest notificationTour)
        {
            notificationRepository.Update(notificationTour);
        }
        public void Subscribe(IObserver observer)
        {
            notificationRepository.Subscribe(observer);
        }
    }
}
