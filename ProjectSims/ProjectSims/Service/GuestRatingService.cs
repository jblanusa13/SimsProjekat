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
    public class GuestRatingService
    {
        private IGuestRatingRepository guestRatings;

        public GuestRatingService()
        {
            guestRatings = Injector.CreateInstance<IGuestRatingRepository>();
        }

        public List<GuestRating> GetAllGuestAccommodations()
        {
            return guestRatings.GetAll();
        }

        public List<GuestRating> GetAllRatingsForGuest(int guestId)
        {
            List<GuestRating> ratings = guestRatings.GetAllForGuest(guestId);
            List<GuestRating> ratingsToShow = new List<GuestRating>();
            foreach(var rating in ratings)
            {
                if(rating.Reservation.Rated == true)
                {
                    ratingsToShow.Add(rating);
                }
            }

            return ratingsToShow;
        }

        public void Create(GuestRating guestRating)
        {
            guestRatings.Create(guestRating);
        }

        public void Delete(GuestRating guestRating)
        {
            guestRatings.Remove(guestRating);
        }

        public void Update(GuestRating guestRating)
        {
            guestRatings.Update(guestRating);
        }

        public void Subscribe(IObserver observer)
        {
            guestRatings.Subscribe(observer);
        }
    }
}
