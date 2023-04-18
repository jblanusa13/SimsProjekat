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

        public List<TourAndGuideRating> GetAllTourRaitngs()
        {
            return tourRatings.GetAll();
        }

        public void Create(TourAndGuideRating tourRaitng)
        {
            tourRatings.Create(tourRaitng);
        }

        public void Delete(TourAndGuideRating tourRaitng)
        {
            tourRatings.Remove(tourRaitng);
        }

        public void Update(TourAndGuideRating tourRaitng)
        {
            tourRatings.Update(tourRaitng);
        }

        public void Subscribe(IObserver observer)
        {
            tourRatings.Subscribe(observer);
        }
    }
}
