using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class TourRatingService
    {
        private TourRatingRepository tourRatings;

        public TourRatingService()
        {
            tourRatings = new TourRatingRepository();
        }

        public List<TourAndGuideRating> GetAll()
        {
            return tourRatings.GetAll();
        }
        public List<TourAndGuideRating> GetAllRatingsByTour(Tour tour)
        {
            return tourRatings.GetAllRatingsByTour(tour);
        }

        public void Create(TourAndGuideRating tourRating)
        {
            tourRatings.Create(tourRating);
        }

        public void Delete(TourAndGuideRating tourRating)
        {
            tourRatings.Remove(tourRating);
        }

        public void Update(TourAndGuideRating tourRating)
        {
            tourRatings.Update(tourRating);
        }
        public void ReportRating(TourAndGuideRating tourRaitng)
        {
            tourRatings.ReportRating(tourRaitng);
        }

        public void Subscribe(IObserver observer)
        {
            tourRatings.Subscribe(observer);
        }
    }
}
