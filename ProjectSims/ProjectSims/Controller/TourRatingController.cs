using ProjectSims.Model;
using ProjectSims.ModelDAO;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Controller
{
    public class TourRatingController
    {
        private TourRatingDAO tourRatings;

        public TourRatingController()
        {
            tourRatings = new TourRatingDAO();
        }

        public List<TourAndGuideRating> GetAllTourRaitngs()
        {
            return tourRatings.GetAll();
        }

        public void Create(TourAndGuideRating tourRaitng)
        {
            tourRatings.Add(tourRaitng);
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
