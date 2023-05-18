using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.FileHandler.Images;
using ProjectSims.Observer;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.Repository
{
    public class SuperGuestRepository : ISuperGuestRepository
    {
        private SuperGuestFileHandler guestFileHandler;
        private List<SuperGuest> guests;
        private List<IObserver> observers;

        public SuperGuestRepository()
        {
            guestFileHandler = new SuperGuestFileHandler();
            guests = guestFileHandler.Load();
            observers = new List<IObserver>();
        }

        public List<SuperGuest> GetAll()
        {
            return guests;
        }

        public int NextId()
        {
            if (guests.Count == 0)
            {
                return 0;
            }
            return guests.Max(t => t.Id) + 1;
        }

        public void Create(SuperGuest entity)
        {
            entity.Id = NextId();
            guests.Add(entity);
            guestFileHandler.Save(guests);
            NotifyObservers();
        }
        public void Update(SuperGuest entity)
        {
            int index = guests.FindIndex(guest => guest.Id == guest.Id);
            if (index != -1)
            {
                guests[index] = entity;
            }
            guestFileHandler.Save(guests);
            NotifyObservers();
        }

        public void Remove(SuperGuest entity)
        {
            guests.Remove(entity);
            guestFileHandler.Save(guests);
            NotifyObservers();
        }
        public SuperGuest GetById(int key)
        {
            return guests.Find(guest => guest.Id == key);
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
