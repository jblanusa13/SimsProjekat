using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;


namespace ProjectSims.Repository
{
    class TourRepository : ISubject
    {

        private TourFileHandler tourFile;
        private List<Tour> tours;
        private List<IObserver> observers;
        public TourRepository()
        {
            tourFile = new TourFileHandler();
            tours = tourFile.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (tours.Count == 0)
            {
                return 0;
            }
            return tours.Max(t => t.Id) + 1;
        }
        public void Create(Tour tour)
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
            NotifyObservers();
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
        public Tour GetTourById(int id)
        {
            return tours.Find(t=>t.Id == id);
        }
        public List<Tour> GetToursByStateAndGuideId(TourState state,int guideId)
        {
            return tours.Where(t => t.State == state && t.GuideId == guideId).ToList();
        }
        public Tour GetTourByStateAndGuideId(TourState state, int guideId)
        {
            return tours.Find(t=> t.State == state && t.GuideId == guideId);
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