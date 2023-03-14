using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;


namespace ProjectSims.ModelDAO
{
    class TourDAO : ISubject
    {

        private TourFileHandler tourFile;
        private List<Tour> tours;
        private List<IObserver> observers;


        public int GenerateID()
        {
            if (tours.Count == 0)
                return 1;
            else
                return tours[tours.Count - 1].Id + 1;
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
            tours.Add(newTour);
            tourFile.Save(tours);
            NotifyObservers();
        }


        public TourDAO()
        {
            tourFile = new TourFileHandler();
            tours = tourFile.Load();
            observers = new List<IObserver>();
        }

        public int NextId()
        {
            return tours.Max(t => t.Id) + 1;
        }

        public void Add(Tour tour)
        {
            tour.Id = NextId();
            tours.Add(tour);
            tourFile.Save(tours);
            NotifyObservers();
        }

        public void Remove(Tour tour)
        {
            tours.Remove(tour);
            tourFile.Save(tours);
            NotifyObservers() ;
        }

        public void Update(Tour tour)
        {
            int index = tours.FindIndex(t => tour.Id == t.Id);
            if(index != -1)
            {
                tours[index] = tour;
            }
            tourFile.Save(tours);
            NotifyObservers();
        }


        public List<Tour> GetAll()
        {
            return tours;
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
            foreach(var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
