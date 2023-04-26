using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.Service
{
    public class AccommodationRatingService
    {
        private AccommodationRatingRepository ratingRepository;

        public AccommodationRatingService()
        {
            ratingRepository = new AccommodationRatingRepository();
        }

        public AccommodationAndOwnerRating GetRating(int id)
        {
            return ratingRepository.Get(id);
        }

        public AccommodationAndOwnerRating GetRatingByReservationId(int reservationId)
        {
            return ratingRepository.GetByReservationId(reservationId);
        }

        public void CreateRating(int reservationId, AccommodationReservation reservation, int cleanliness, int ownerFairness, int location, int valueForMoney, string comment, List<string> imageList, int recommendationId, RenovationRecommendation recommendation)
        {
            int id = ratingRepository.NextId();
            AccommodationAndOwnerRating rating = new AccommodationAndOwnerRating(id, reservationId, reservation, cleanliness, ownerFairness, location, valueForMoney, comment, imageList, recommendationId, recommendation);
            ratingRepository.Add(rating);
        }
    }
}
