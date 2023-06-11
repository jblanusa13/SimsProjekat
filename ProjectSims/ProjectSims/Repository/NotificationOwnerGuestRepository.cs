using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Repository
{
    public class NotificationOwnerGuestRepository : INotificationOwnerGuestRepository
    { 
        private NotificationOwnerGuestFileHandler notificationFileHandler;
        private List<NotificationOwnerGuest> notifications;
        private List<IObserver> observers;
        public NotificationOwnerGuestRepository()
        {
            notificationFileHandler = new NotificationOwnerGuestFileHandler();
            notifications = notificationFileHandler.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (notifications.Count == 0)
            {
                return 0;
            }
            return notifications.Max(t => t.Id) + 1;
        }
        public void Create(NotificationOwnerGuest notification)
        {
            notification.Id = NextId();
            notifications.Add(notification);
            notificationFileHandler.Save(notifications);
            NotifyObservers();
        }
        public void Remove(NotificationOwnerGuest notification)
        {
            notifications.Remove(notification);
            notificationFileHandler.Save(notifications);
            NotifyObservers();
        }
        public void Update(NotificationOwnerGuest notification)
        {
            int index = notifications.FindIndex(t => t.Id == notification.Id);
            if (index != -1)
            {
                notifications[index] = notification;
            }
            notificationFileHandler.Save(notifications);
            NotifyObservers();
        }
        public NotificationOwnerGuest GetById(int id)
        {
            return notifications.Find(n => n.Id == id);
        }
        public List<NotificationOwnerGuest> GetAll()
        {
            return notifications;
        }        
        public List<Forum> GetAllForums()
        {
            List<Forum> forums = new List<Forum>();
            foreach (var item in notifications)
            {
                if (item.ForumId != -1 && item.Seen == false)
                {
                    forums.Add(item.Forum);
                }
            }
            return forums;
        }
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
