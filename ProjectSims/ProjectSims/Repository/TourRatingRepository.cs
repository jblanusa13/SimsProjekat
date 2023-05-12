using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Repository
{
    class TourRatingRepository : ITourRatingRepository
    {
        private TourRatingFileHandler tourRatingFile;
        private List<TourAndGuideRating> toursRating;
        private List<IObserver> observers;

        public TourRatingRepository()
        {
            tourRatingFile = new TourRatingFileHandler();
            toursRating = tourRatingFile.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (toursRating.Count == 0)
            {
                return 0;
            }
            return toursRating.Max(t => t.Id) + 1;
        }
        public void Create(TourAndGuideRating tourRating)
        {
            tourRating.Id = NextId();
            toursRating.Add(tourRating);
            tourRatingFile.Save(toursRating);
            NotifyObservers();
        }
        public void Remove(TourAndGuideRating tourRating)
        {
            toursRating.Remove(tourRating);
            tourRatingFile.Save(toursRating);
            NotifyObservers();
        }
        public void Update(TourAndGuideRating tourRating)
        {
            int index = toursRating.FindIndex(t => tourRating.Id == t.Id);
            if (index != -1)
            {
                toursRating[index] = tourRating;
            }
            tourRatingFile.Save(toursRating);
            NotifyObservers();
        }
        public List<TourAndGuideRating> GetAll()
        {
            return toursRating;
        }
        public TourAndGuideRating GetById(int id)
        {
            return toursRating.Find(t=> t.Id == id);
        }
        public List<TourAndGuideRating> GetAllRatingsByTour(Tour tour)
        {
            return toursRating.Where(r => r.TourId == tour.Id).ToList();
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
    }
}
