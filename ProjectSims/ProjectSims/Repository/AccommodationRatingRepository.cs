using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.WPF.View.Guest1View.Pages;

namespace ProjectSims.Repository
{
    public class AccommodationRatingRepository
    {
        private AccommodationRatingFileHandler ratingFileHandler;
        private List<AccommodationAndOwnerRating> ratings;

        public AccommodationRatingRepository()
        {
            ratingFileHandler = new AccommodationRatingFileHandler();
            ratings = ratingFileHandler.Load();
        }

        public List<AccommodationAndOwnerRating> GetAll()
        {
            return ratings;
        }

        public AccommodationAndOwnerRating Get(int id)
        {
            return ratings.Find(r => r.Id == id);
        }

        public void Add(AccommodationAndOwnerRating rating)
        {
            ratings.Add(rating);
            ratingFileHandler.Save(ratings);
        }
    }
}
