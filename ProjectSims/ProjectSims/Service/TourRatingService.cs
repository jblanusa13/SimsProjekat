﻿using ProjectSims.Domain.Model;
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
       // private TourRatingRepository tourRatingRepository;

        public TourRatingService()
        {
          // tourRatingRepository = new TourRatingRepository();
          tourRatingRepository = Injector.CreateInstance<ITourRatingRepository>();
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
        public void Delete(TourAndGuideRating tourRating)
        {
            tourRatingRepository.Remove(tourRating);
        }
        public void Update(TourAndGuideRating tourRating)
        {
            tourRatingRepository.Update(tourRating);
        }
        public void ReportRating(TourAndGuideRating tourRaitng)
        {
            tourRatingRepository.ReportRating(tourRaitng);
        }
        public void Subscribe(IObserver observer)
        {
            tourRatingRepository.Subscribe(observer);
        }
    }
}
