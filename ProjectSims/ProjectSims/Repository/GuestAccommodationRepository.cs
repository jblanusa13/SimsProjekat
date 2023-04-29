using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;

namespace ProjectSims.Repository
{
    public class GuestAccommodationRepository : ISubject  
    {
            private GuestAccommodationFileHandler _guestAccommodationFileHandler;
            private List<GuestAccommodation> _guestAccommodations;

            private List<IObserver> _observers;

            public GuestAccommodationRepository()
            {
                _guestAccommodationFileHandler = new GuestAccommodationFileHandler();
                _guestAccommodations = _guestAccommodationFileHandler.Load();
                _observers = new List<IObserver>();
            }

            public int NextId()
            {
                return _guestAccommodations.Max(a => a.Id) + 1;
            }

            public void Add(GuestAccommodation accommodation)
            {
                accommodation.Id = NextId();
                _guestAccommodations.Add(accommodation);
                _guestAccommodationFileHandler.Save(_guestAccommodations);
                NotifyObservers();
            }

            public void Remove(GuestAccommodation accommodation)
            {
                _guestAccommodations.Remove(accommodation);
                _guestAccommodationFileHandler.Save(_guestAccommodations);
                NotifyObservers();
            }

            public void Update(GuestAccommodation accommodation)
            {
                int index = _guestAccommodations.FindIndex(a => accommodation.Id == a.Id);
                if (index != -1)
                {
                    _guestAccommodations[index] = accommodation;
                }
                _guestAccommodationFileHandler.Save(_guestAccommodations);
                NotifyObservers();
            }

            public List<GuestAccommodation> GetAll()
            {
                return _guestAccommodations;
            }

            public void Subscribe(IObserver observer)
            {
                _observers.Add(observer);
            }

            public void Unsubscribe(IObserver observer)
            {
                _observers.Remove(observer);
            }

            public void NotifyObservers()
            {
                foreach (var observer in _observers)
                {
                    observer.Update();
                }
            }
            public GuestAccommodation Get(int id)
            {
                return _guestAccommodations.Find(g => g.Id == id);
            }
        }
}
