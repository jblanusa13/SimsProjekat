using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Repository
{
    class Guest1Repository : ISubject
        {
            private Guest1FileHandler guestFileHandler;
            private List<Guest1> guests;

            private List<IObserver> observers;

            public Guest1Repository()
            {
                guestFileHandler = new Guest1FileHandler();
                guests = guestFileHandler.Load();
                observers = new List<IObserver>();
            }

            public int NextId()
            {
                return guests.Max(t => t.Id) + 1;
            }

            public void Add(Guest1 guest)
            {
                guest.Id = NextId();
                guests.Add(guest);
                guestFileHandler.Save(guests);
                NotifyObservers();
            }

            public void Remove(Guest1 guest)
            {
                guests.Remove(guest);
                guestFileHandler.Save(guests);
                NotifyObservers();
            }

            public void Update(Guest1 guest)
            {
                int index = guests.FindIndex(guest => guest.Id == guest.Id);
                if (index != -1)
                {
                    guests[index] = guest;
                }
                guestFileHandler.Save(guests);
                NotifyObservers();
            }


            public List<Guest1> GetAll()
            {
                return guests;
            }

            public Guest1 Get(int id)
            {
                return guests.Find(g => g.Id == id);
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
            public Guest1 FindById(int id)
            {
                return guests.Find(guest => guest.Id == id);
            }
        }
}