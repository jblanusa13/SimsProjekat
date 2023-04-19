using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Service;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public partial class AccommodationRatingViewModel
    {
        public AccommodationReservation AccommodationReservation { get; set; }
        public Guest1 Guest { get; set; }
        public int Cleanliness { get; set; }
        public int Fairness { get; set; }
        public int Location { get; set; }
        public int ValueForMoney { get; set; }
        public string Comment { get; set; }

        private AccommodationRatingService ratingService;
        private AccommodationReservationService reservationService;

        public AccommodationRatingViewModel(AccommodationReservation accommodationReservation, Guest1 guest)
        {
            AccommodationReservation = accommodationReservation;
            Guest = guest;

            ratingService = new AccommodationRatingService();
            reservationService = new AccommodationReservationService();
        }

        public void Confirm(string cleanliness, string fairness, string location, string valueForMoney, string comment, string imagesString)
        {
            Cleanliness = Convert.ToInt32(cleanliness);
            Fairness = Convert.ToInt32(fairness);
            Location = Convert.ToInt32(location);
            ValueForMoney = Convert.ToInt32(valueForMoney);


            List<string> imageList = new List<string>();
            if (!string.IsNullOrEmpty(imagesString))
            {
                string images = imagesString.Remove(imagesString.Length - 2, 2);
                foreach (string image in images.Split(",\n"))
                {
                    imageList.Add(image);
                }
            }
            else
            {
                imageList.Add("");
            }

            if (string.IsNullOrEmpty(comment))
            {
                Comment = "";
            }
            else
            {
                Comment = comment;
            }

            ratingService.CreateRating(Guest.Id, Guest, AccommodationReservation.Accommodation.Id, AccommodationReservation.Accommodation, Cleanliness, Fairness, Location, ValueForMoney, Comment, imageList);
            reservationService.ChangeReservationRatedState(AccommodationReservation);
        }
    }
}
