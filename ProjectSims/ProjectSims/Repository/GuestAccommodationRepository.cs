using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Repository
{
    public class GuestAccommodationRepository : IGuestAccommodationRepository
    {
            private GuestAccommodationFileHandler _guestAccommodationFileHandler;
            private List<GuestAccommodation> guestAccommodations;
            private List<IObserver> _observers;

            public GuestAccommodationRepository()
            {
                _guestAccommodationFileHandler = new GuestAccommodationFileHandler();
                guestAccommodations = _guestAccommodationFileHandler.Load();
                _observers = new List<IObserver>();
            }

            public int NextId()
            {
                return guestAccommodations.Max(a => a.Id) + 1;
            }

            public void Create(GuestAccommodation accommodation)
            {
                accommodation.Id = NextId();
                guestAccommodations.Add(accommodation);
                _guestAccommodationFileHandler.Save(guestAccommodations);
                NotifyObservers();
            }

            public void Remove(GuestAccommodation accommodation)
            {
                guestAccommodations.Remove(accommodation);
                _guestAccommodationFileHandler.Save(guestAccommodations);
                NotifyObservers();
            }

            public void Update(GuestAccommodation accommodation)
            {
                int index = guestAccommodations.FindIndex(a => accommodation.Id == a.Id);
                if (index != -1)
                {
                    guestAccommodations[index] = accommodation;
                }
                _guestAccommodationFileHandler.Save(guestAccommodations);
                NotifyObservers();
            }

            public List<GuestAccommodation> GetAll()
            {
                return guestAccommodations;
            }

            public GuestAccommodation GetById(int id) 
            {
                return guestAccommodations.Find(g => g.Id == id);
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
        }
}
