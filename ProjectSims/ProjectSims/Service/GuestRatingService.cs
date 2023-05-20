using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;
using System.Windows;
using System.IO;

namespace ProjectSims.Service
{
    public class GuestRatingService
    {
        private IGuestRatingRepository guestRatings;
        private IAccommodationReservationRepository accommodationReservationRepository;

        public GuestRatingService()
        {
            guestRatings = Injector.CreateInstance<IGuestRatingRepository>();
            accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();

            InitializeReservation();
        }

        private void InitializeReservation()
        {
            foreach(var rating in guestRatings.GetAll())
            {
                rating.Reservation = accommodationReservationRepository.GetById(rating.ReservationId);
            }
        }

        public List<GuestRating> GetAllGuestAccommodations()
        {
            return guestRatings.GetAll();
        }

        public List<GuestRating> GetAllRatingsForGuest(int guestId)
        {
            guestRatings.ReloadRatingList();
            InitializeReservation();
            List<GuestRating> ratings = guestRatings.GetAllForGuest(guestId);
            List<GuestRating> ratingsToShow = new List<GuestRating>();
            foreach(var rating in ratings)
            {
                if(rating.Reservation.RatedAccommodation == true)
                {
                    ratingsToShow.Add(rating);
                }
            }

            return ratingsToShow;
        }

        public void Create(GuestRating guestRating)
        {
            guestRatings.Create(guestRating);
            InitializeReservation();
        }

        public void Delete(GuestRating guestRating)
        {
            guestRatings.Remove(guestRating);
        }

        public void Update(GuestRating guestRating)
        {
            guestRatings.Update(guestRating);
            InitializeReservation();
        }

        public void Subscribe(IObserver observer)
        {
            guestRatings.Subscribe(observer);
        }

        public void NotifyOwnerAboutRating()
        {
            string fileName = "../../../Resources/Data/lastShown.csv";
            try
            {
                string lastShownText = File.ReadAllText(fileName);
                DateOnly lastShownDate = DateOnly.Parse(lastShownText);
                NotifyIfAnyGuestUnrated(fileName, lastShownDate, lastShownText);
            }
            catch (Exception)
            {

            }
        }

        public void NotifyIfAnyGuestUnrated(string fileName, DateOnly lastShownDate, string lastShowntext)
        {
            AccommodationReservationService accommodationReservationService = new AccommodationReservationService();

            if (lastShownDate < DateOnly.FromDateTime(DateTime.Today))
            {
                if (accommodationReservationService.IsAnyGuestRatable())
                {
                    MessageBox.Show("Imate neocijenjenih gostiju!", "Ocjenjivanje gostiju", MessageBoxButton.OK);
                    File.WriteAllText(fileName, DateOnly.FromDateTime(DateTime.Today).ToString());
                }
            }
        }
    }
}
