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
    class AccommodationRepository : ISubject
    {
        private AccommodationFileHandler _accommodationFileHandler;
        private LocationFileHandler _locationFileHandler;
        private List<Accommodation> _accommodations;
        private List<Location> _locations;
        private readonly OwnerFileHandler _ownerFileHandler;
        private readonly List<Owner> _owners;
        private List<IObserver> _observers;

        public OwnerRepository OwnerDao { get; set; }

        public AccommodationRepository()
        {
            _accommodationFileHandler = new AccommodationFileHandler();
            _accommodations = _accommodationFileHandler.Load();
            _locationFileHandler = new LocationFileHandler();
            _locations = _locationFileHandler.Load();
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

       // public Location FindLocationById(int id)
        //{
          //  return _locations.Find(l => l.Id == id);
        //}


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
        public int NextLocationId() 
        {
            return _locations.Max(l => l.Id) + 1; 
        }
        
        public int Add(string location) 
        {
            int id = -1;
            if (!ExistLocation(location)) 
            {
                id = NextLocationId();
                Location loc = new Location(id, location.Split(",")[0], location.Split(",")[1]);
                _locations.Add(loc);
                _locationFileHandler.Save(_locations);
                NotifyObservers();
            }
            return id;
        }
        
        public bool ExistLocation(string location)
        {
            string city = location.Split(",")[0];
            string country = location.Split(",")[1];

            foreach (Location l in _locations)
            {
                if (city == l.City && country == l.Country)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetLocationId(string location) 
        {
            string city = location.Split(",")[0];
            string country = location.Split(",")[1];

            foreach (Location l in _locations)
            {
                if (city == l.City && country == l.Country)
                {
                    return l.Id;
                }
            }
            return -1;
        }
    }   
}