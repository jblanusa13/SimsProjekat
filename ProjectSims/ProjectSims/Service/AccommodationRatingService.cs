using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.Service
{
    public class AccommodationRatingService
    {
        private IAccommodationRatingRepository ratingRepository;

        public AccommodationRatingService()
        {
            ratingRepository = Injector.CreateInstance<IAccommodationRatingRepository>();
        }

        public List<AccommodationAndOwnerRating> GetAllRatings()
        {
            return ratingRepository.GetAll();
        }
        public List<AccommodationAndOwnerRating> GetRatingsWhereGuestRated(int ownerId)
        {
            List<AccommodationAndOwnerRating> ratings = new List<AccommodationAndOwnerRating>();
            foreach (var rating in GetAllRatings()) 
            {
                if (rating.Reservation.RatedGuest == true && rating.Reservation.Accommodation.IdOwner == ownerId)
                {
                    ratings.Add(rating);
                }
            }
            return ratings;
        }
        public AccommodationAndOwnerRating GetRating(int id)
        {
            return ratingRepository.GetById(id);
        }

        public AccommodationAndOwnerRating GetRatingByReservationId(int reservationId)
        {
            return ratingRepository.GetByReservationId(reservationId);
        }

        public List<AccommodationAndOwnerRating> GetAllRatingsByGuestId(int guestId)
        {
            return ratingRepository.GetAllByGuestId(guestId);
        }

        public void CreateRating(int reservationId, AccommodationReservation reservation, int cleanliness, int ownerFairness, int location, int valueForMoney, string comment, List<string> imageList, int recommendationId, RenovationRecommendation recommendation)
        {
            int id = ratingRepository.NextId();
            AccommodationAndOwnerRating rating = new AccommodationAndOwnerRating(id, reservationId, reservation, cleanliness, ownerFairness, location, valueForMoney, comment, imageList, recommendationId, recommendation);
            ratingRepository.Create(rating);
        }
    }
}
