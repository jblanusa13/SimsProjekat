using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class GuestRatingService
    {
        private GuestRatingRepository guestRatings;

        public GuestRatingService()
        {
            guestRatings = new GuestRatingRepository();
        }

        public List<GuestRating> GetAllGuestAccommodations()
        {
            return guestRatings.GetAll();
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
