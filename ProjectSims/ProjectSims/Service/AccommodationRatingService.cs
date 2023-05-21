using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using ProjectSims.Repository;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.Service
{
    public class AccommodationRatingService
    {
        private IAccommodationRatingRepository ratingRepository;
        private IAccommodationReservationRepository reservationRepository;
        private IRenovationRecommendationRepository renovationRecommendationRepository;

        public AccommodationRatingService()
        {
            ratingRepository = Injector.CreateInstance<IAccommodationRatingRepository>();
            reservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            renovationRecommendationRepository = Injector.CreateInstance<IRenovationRecommendationRepository>();

            InitializeRecommendation();
            InitializeReservation();
        }

        public void InitializeReservation()
        {
            foreach(var rating in ratingRepository.GetAll())
            {
                rating.Reservation = reservationRepository.GetById(rating.ReservationId);
            }
        }

        public void InitializeRecommendation()
        {
            foreach (var rating in ratingRepository.GetAll())
            {
                rating.RenovationRecommendation = renovationRecommendationRepository.GetById(rating.RenovationId);
            }
        }

        public List<AccommodationAndOwnerRating> GetAllRatings()
        {
            return ratingRepository.GetAll();
        }

        public List<AccommodationAndOwnerRating> GetRatingsWhereGuestRated(int ownerId)
        {
            ratingRepository.ReloadRatingList();
            InitializeRecommendation();
            InitializeReservation();
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

        public int GetAllRenovationReccommendationsByYear(string year, int ownerId)
        {
            int number = 0;
            foreach (AccommodationAndOwnerRating rating in ratingRepository.GetAllByOwnerId(ownerId))
            {
                if (rating.Reservation.CheckInDate.Year.ToString().Equals(year) && rating.RenovationId != -1)
                {
                    number++;
                }
            }
            return number;
        }

        public int GetAllRenovationReccommendationsByMonth(string month, string year, int ownerId)
        {
            int number = 0;
            foreach (AccommodationAndOwnerRating rating in ratingRepository.GetAllByOwnerId(ownerId))
            {
                if (rating.Reservation.CheckInDate.Year.ToString().Equals(year) && rating.Reservation.CheckInDate.Month.ToString().Equals(month) && rating.RenovationId != -1)
                {
                    number++;
                }
            }
            return number;
        }

        public void CreateRating(int reservationId, AccommodationReservation reservation, int cleanliness, int ownerFairness, int location, int valueForMoney, string comment, List<string> imageList, int recommendationId, RenovationRecommendation recommendation)
        {
            int id = ratingRepository.NextId();
            AccommodationAndOwnerRating rating = new AccommodationAndOwnerRating(id, reservationId, reservation, cleanliness, ownerFairness, location, valueForMoney, comment, imageList, recommendationId, recommendation);
            ratingRepository.Create(rating);
        }

        public void Subscribe(IObserver observer)
        {
            ratingRepository.Subscribe(observer);
        }

        public bool IsSuperowner(Owner owner) 
        {
            bool isSuperowner = false;
            double[] averageAndTotal = AverageAndNumberOfAllGrades(owner);
            if (averageAndTotal[0] > 4.5 && averageAndTotal[1] >= 50) 
            {
                isSuperowner = true;
            }
            return isSuperowner;
        }

        public double[] AverageAndNumberOfAllGrades(Owner owner)
        {
            double[] result = {0, 0};
            foreach (var rating in GetAllRatings())
            {
                if (rating.Reservation.Accommodation.IdOwner == owner.Id && rating.Reservation.RatedAccommodation == true && rating.Reservation.RatedGuest == true)
                {
                    result[0] += AverageGrade(rating);
                    result[1] += 1;
                }
            }
            result[0] = result[0] / result[1];
            return result;
        }

        public double AverageGrade(AccommodationAndOwnerRating rating)
        {
            return (rating.Cleanliness + rating.OwnerFairness + rating.ValueForMoney + rating.Location)/ 4;
        }
    }
}
