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
        private AccommodationFileHandler accommodationFileHandler;
        private LocationFileHandler locationFileHandler;
        private List<Accommodation> accommodations;
        private List<Location> locations;
        private readonly OwnerFileHandler _ownerFileHandler;
        private readonly List<Owner> _owners;
        private List<IObserver> observers;

        public OwnerRepository ownerRepository { get; set; }

        public AccommodationRepository()
        {
            accommodationFileHandler = new AccommodationFileHandler();
            accommodations = accommodationFileHandler.Load();
            locationFileHandler = new LocationFileHandler();
            locations = locationFileHandler.Load();
            observers = new List<IObserver>();
        }

        public int NextId()
        {
            return accommodations.Max(a => a.Id) + 1;
        }

        public void Add(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            accommodations.Add(accommodation);
            accommodationFileHandler.Save(accommodations);
            NotifyObservers();
        }

        public void Remove(Accommodation accommodation)
        {
            accommodations.Remove(accommodation);
            accommodationFileHandler.Save(accommodations);
            NotifyObservers();
        }

        public void Update(Accommodation accommodation)
        {
            int index = accommodations.FindIndex(a => accommodation.Id == a.Id);
            if (index != -1)
            {
                accommodations[index] = accommodation;
            }
            accommodationFileHandler.Save(accommodations);
            NotifyObservers();
        }


        public List<Accommodation> GetAll()
        {
            return accommodations;
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
        public int NextLocationId() 
        {
            return locations.Max(l => l.Id) + 1; 
        }
        
        public int Add(string location) 
        {
            int id = -1;
            if (!ExistLocation(location)) 
            {
                id = NextLocationId();
                Location loc = new Location(id, location.Split(",")[0], location.Split(",")[1]);
                locations.Add(loc);
                locationFileHandler.Save(locations);
                NotifyObservers();
            }
            return id;
        }
        
        public bool ExistLocation(string location)
        {
            string city = location.Split(",")[0];
            string country = location.Split(",")[1];

            foreach (Location l in locations)
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

            foreach (Location l in locations)
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