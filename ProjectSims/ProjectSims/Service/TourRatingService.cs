using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Service
{
    public class TourRatingService
    {
        private ITourRatingRepository tourRatingRepository;
        private IGuest2Repository guest2Repository;
        private ITourRepository tourRepository;

        public TourRatingService()
        {
            tourRatingRepository = Injector.CreateInstance<ITourRatingRepository>();
            guest2Repository = Injector.CreateInstance<IGuest2Repository>();
            tourRepository = Injector.CreateInstance<ITourRepository>();
            InitializeGuest();
            InitializeTour();
        }
        private void InitializeGuest()
        {
            foreach(var item in tourRatingRepository.GetAll())
            {
                item.Guest = guest2Repository.GetById(item.GuestId);
            }
        }
        private void InitializeTour()
        {
            foreach (var item in tourRatingRepository.GetAll())
            {
                item.Tour = tourRepository.GetById(item.TourId);
            }
        }
        public List<TourAndGuideRating> GetAll()
        {
            return tourRatingRepository.GetAll();
        }
        public List<TourAndGuideRating> GetAllRatingsByTour(Tour tour)
        {
            return tourRatingRepository.GetAllRatingsByTour(tour);
        }
        public void Create(TourAndGuideRating tourRating)
        {
            tourRatingRepository.Create(tourRating);
        }
        public void ReportRating(TourAndGuideRating tourRating)
        {
            tourRating.IsValid = false;
            tourRatingRepository.Update(tourRating);
        }
        public void Delete(TourAndGuideRating tourRating)
        {
            tourRatingRepository.Remove(tourRating);
        }
        public void Update(TourAndGuideRating tourRating)
        {
            tourRatingRepository.Update(tourRating);
        }
        public void Subscribe(IObserver observer)
        {
            tourRatingRepository.Subscribe(observer);
        }
    }
}
