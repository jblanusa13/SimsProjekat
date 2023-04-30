using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Cache;
using System.Windows.Controls;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Repository
{
    class Guest2Repository : IGuest2Repository
    {
        private Guest2FileHandler guest2FileHandler;
        private List<Guest2> guests;
        private List<IObserver> observers;
        public Guest2Repository()
        {
            guest2FileHandler = new Guest2FileHandler();
            guests = guest2FileHandler.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (guests.Count == 0)
            {
                return 0;
            }
            return guests.Max(t => t.Id) + 1;
        }
        public void Create(Guest2 guest)
        {
            guest.Id = NextId();
            guests.Add(guest);
            guest2FileHandler.Save(guests);
            NotifyObservers();
        }
        public void Remove(Guest2 guest)
        {
            guests.Remove(guest);
            guest2FileHandler.Save(guests);
            NotifyObservers();
        }
        public void Update(Guest2 guest)
        {
            int index = guests.FindIndex(g => guest.Id == g.Id);
            if (index != -1)
            {
                guests[index] = guest;
            }
            guest2FileHandler.Save(guests);
            NotifyObservers();
        }
        public List<Guest2> GetAll()
        {
            return guest2FileHandler.Load();
        }
        public Guest2 GetById(int id)
        {
            return guests.Find(guest => guest.Id == id);
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
