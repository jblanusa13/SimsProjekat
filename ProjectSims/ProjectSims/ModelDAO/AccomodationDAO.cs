using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.Observer;

namespace ProjectSims.ModelDAO
{
    class AccomodationDAO : ISubject
    {
        private readonly List<IObserver> _observers;

        private readonly List<Accomodation> _accommodations;
        private readonly List<Location> _locations;
        private readonly AccomodationFileHandler _accommodationFileHandler;
        private readonly LocationFileHandler _locationFileHandler;

        public AccomodationDAO()
        {
            _observers = new List<IObserver>();
            _accommodationFileHandler = new AccomodationFileHandler();
            _accommodations = _accommodationFileHandler.Load();
            _locationFileHandler = new LocationFileHandler();
            _locations = _locationFileHandler.Load();
        }

        public List<Accomodation> GetAll()
        {
            return _accommodations;
        }

        public Location FindLocation(int locationId)
        {
            Location location = _locations.Find(l => l.Id == locationId);
            return location;
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
            foreach(var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
