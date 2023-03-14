using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using ProjectSims.Observer;
using ProjectSims.Model;
using ProjectSims.FileHandler;

namespace ProjectSims.ModelDAO
{
    class TourDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly List<Tour> _tours;
        private readonly TourFileHandler _fileHandler;

        public TourDAO()
        {
            _observers = new List<IObserver>();
            _fileHandler = new TourFileHandler();
            _tours = _fileHandler.Load();
        }
        public int GenerateID()
        {
            if (_tours.Count == 0)
                return 1;
            else
                return _tours[_tours.Count - 1].Id + 1;
        }
        public void Save(string name, string location, string description, string language, int maxNumberGuests, List<string> keyPointNames, DateTime tourStart, double duration, List<string> images)
        {
            int id = GenerateID();
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            foreach(string keyPointName  in keyPointNames) 
            { 
                keyPoints.Add(new KeyPoint(keyPointName, id));
            }
            Tour newTour = new Tour(id, name, location, description, language, maxNumberGuests, keyPoints, tourStart, duration, images, maxNumberGuests);
            _tours.Add(newTour);
            _fileHandler.Save(_tours);
            NotifyObservers();
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
                observer.UpdateTour();
            }
        }



    }
}
