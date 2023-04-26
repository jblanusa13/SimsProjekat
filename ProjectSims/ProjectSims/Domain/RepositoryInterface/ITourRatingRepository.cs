using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Observer;
using ProjectSims.Repository;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface ITourRatingRepository
    {
        public void Create(TourAndGuideRating tourRating);
        public void Update(TourAndGuideRating tourRating);
        public void Remove(TourAndGuideRating tourRating);
        public List<TourAndGuideRating> GetAll();
        public List<TourAndGuideRating> GetAllRatingsByTour(Tour tour);
        public void ReportRating(TourAndGuideRating tourRaitng);
        public int GetNextId();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
    }
}
