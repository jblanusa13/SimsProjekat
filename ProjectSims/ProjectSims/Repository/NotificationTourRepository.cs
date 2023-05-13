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
    public class NotificationTourRepository : INotificationTourRepository
    {
        private NotificationTourFileHandler notificationTourFile;
        private List<NotificationTour> notificationTours;
        private List<IObserver> observers;
        public NotificationTourRepository()
        {
            notificationTourFile = new NotificationTourFileHandler();
            notificationTours = notificationTourFile.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (notificationTours.Count == 0)
            {
                return 0;
            }
            return notificationTours.Max(t => t.Id) + 1;
        }
        public void Create(NotificationTour notificationTour)
        {
            notificationTour.Id = NextId();
            notificationTours.Add(notificationTour);
            notificationTourFile.Save(notificationTours);
            NotifyObservers();
        }
        public void Remove(NotificationTour notificationTour)
        {
            notificationTours.Remove(notificationTour);
            notificationTourFile.Save(notificationTours);
            NotifyObservers();
        }
        public void Update(NotificationTour notificationTour)
        {
            int index = notificationTours.FindIndex(t => t.Id == notificationTour.Id);
            if (index != -1)
            {
                notificationTours[index] = notificationTour;
            }
            notificationTourFile.Save(notificationTours);
            NotifyObservers();
        }
        public NotificationTour GetById(int id)
        {
            return notificationTours.Find(nt => nt.Id == id);
        }
        public List<NotificationTour> GetAll()
        {
            return notificationTours;
        }
        public List<NotificationTour> GetByGuest2Id(int guest2Id)
        {
            return notificationTours.Where(nt => nt.Guest2Id == guest2Id).ToList();
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
