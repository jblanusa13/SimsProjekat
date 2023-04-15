using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.WPF.View.Guest1View.Pages;

namespace ProjectSims.Service
{
    public class AccommodationRatingService
    {
        private AccommodationRatingRepository ratingRepository;
        private List<AccommodationAndOwnerRating> ratings;

        public AccommodationRatingService()
        {
            ratingRepository = new AccommodationRatingRepository();
            ratings = ratingRepository.GetAll();
        }

        public AccommodationAndOwnerRating GetRating(int id)
        {
            return ratingRepository.Get(id);
        }


        public int NextId()
        {
            return ratings.Max(r => r.Id) + 1;
        }

    }
}
