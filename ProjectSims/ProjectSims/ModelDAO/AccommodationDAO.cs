using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.ModelDAO
{
    class AccommodationDAO : ISubject
    {
        private AccommodationFileHandler _accommodationFileHandler;
        private List<Accommodation> _accommodations;
        private readonly OwnerFileHandler _ownerFileHandler;
        private readonly List<Owner> _owners;
        private List<IObserver> _observers;

        public OwnerDAO OwnerDao { get; set; }

        public AccommodationDAO()
        {
            _accommodationFileHandler = new AccommodationFileHandler();
            _accommodations = _accommodationFileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _accommodations.Max(a => a.Id) + 1;
        }

        public void Add(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations.Add(accommodation);
            _accommodationFileHandler.Save(_accommodations);
            NotifyObservers();
        }

        public void Remove(Accommodation accommodation)
        {
            _accommodations.Remove(accommodation);
            _accommodationFileHandler.Save(_accommodations);
            NotifyObservers();
        }

        public void Update(Accommodation accommodation)
        {
            int index = _accommodations.FindIndex(a => accommodation.Id == a.Id);
            if (index != -1)
            {
                _accommodations[index] = accommodation;
            }
            _accommodationFileHandler.Save(_accommodations);
            NotifyObservers();
        }


        public List<Accommodation> GetAll()
        {
            return _accommodations;
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
        public void ConnectAccommodationWithOwner() 
        {
            foreach (Accommodation accommodation in _accommodations)
            {
                Owner owner = OwnerDao.FindById(accommodation.IdOwner);
                if (owner != null) 
                {
                    owner.OwnersAccommodations.Add(accommodation);
                    accommodation.Owner = owner;
                }
            }
        }

    }
}
