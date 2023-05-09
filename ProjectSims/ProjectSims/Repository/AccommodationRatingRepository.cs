using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.Repository
{
    public class AccommodationRatingRepository : IAccommodationRatingRepository
    {
        private AccommodationRatingFileHandler ratingFileHandler;
        private List<AccommodationAndOwnerRating> ratings;

        public AccommodationRatingRepository()
        {
            ratingFileHandler = new AccommodationRatingFileHandler();
            ratings = ratingFileHandler.Load();
        }
        public AccommodationAndOwnerRating GetByReservationId(int reservationId)
        {
            return ratings.Find(r => r.ReservationId == reservationId);
        }
        public List<AccommodationAndOwnerRating> GetAllByGuestId(int guestId)
        {
            List<AccommodationAndOwnerRating> ratingsByGuest = new List<AccommodationAndOwnerRating>();

            foreach(AccommodationAndOwnerRating rating in ratings)
            {
                if(rating.Reservation.Guest.Id == guestId)
                {
                    ratingsByGuest.Add(rating);
                }
            }

            return ratingsByGuest;
        }
        public List<AccommodationAndOwnerRating> GetAll()
        {
            ReloadRatingList();
            return ratings;
        }

        public void ReloadRatingList() 
        {
            ratings = ratingFileHandler.Load();
        }
        public int NextId()
        {
            if (ratings.Count == 0)
            {
                return 0;
            }
            return ratings.Max(r => r.Id) + 1;
        }

        public void Create(AccommodationAndOwnerRating entity)
        {
            ratings.Add(entity);
            ratingFileHandler.Save(ratings);
        }
        public void Update(AccommodationAndOwnerRating entity)
        {
            int index = ratings.FindIndex(r => entity.Id == r.Id);
            if (index != -1)
            {
                ratings[index] = entity;
            }
            ratingFileHandler.Save(ratings);
        }

        public void Remove(AccommodationAndOwnerRating entity)
        {
            ratings.Remove(entity);
            ratingFileHandler.Save(ratings); 
        }

        public AccommodationAndOwnerRating GetById(int key)
        {
            return ratings.Find(r => r.Id == key);
        }
    }
}
