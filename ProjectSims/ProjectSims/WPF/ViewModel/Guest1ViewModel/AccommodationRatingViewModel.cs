using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.RatingPages;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public partial class AccommodationRatingViewModel
    {
        private AccommodationReservationService reservationService;
        private AccommodationRatingService ratingService;
        private RenovationRecommendationService recommendationService;
        public AccommodationReservation AccommodationReservation { get; set; }
        public Guest1 Guest { get; set; }
        public int Cleanliness { get; set; }
        public int Fairness { get; set; }
        public int Location { get; set; }
        public int ValueForMoney { get; set; }
        public string Comment { get; set; }
        public List<string> ImageList { get; set; }
        private Frame selectedTab;

        public AccommodationRatingViewModel(AccommodationReservation accommodationReservation, Guest1 guest, Frame selectedTab)
        {
            reservationService = new AccommodationReservationService();
            ratingService = new AccommodationRatingService();
            recommendationService = new RenovationRecommendationService();

            AccommodationReservation = accommodationReservation;
            Guest = guest;

            ImageList = new List<string>();

            this.selectedTab = selectedTab;
        }

        public void Confirm(string cleanliness, string fairness, string location, string valueForMoney, string comment, string imagesString, Frame selectedTab)
        {
            Cleanliness = Convert.ToInt32(cleanliness);
            Fairness = Convert.ToInt32(fairness);
            Location = Convert.ToInt32(location);
            ValueForMoney = Convert.ToInt32(valueForMoney);

            if (!string.IsNullOrEmpty(imagesString))
            {
                string images = imagesString.Remove(imagesString.Length - 2, 2);
                foreach (string image in images.Split(",\n"))
                {
                    ImageList.Add(image);
                }
            }
            else
            {
                ImageList.Add("");
            }

            if (string.IsNullOrEmpty(comment))
            {
                Comment = "";
            }
            else
            {
                Comment = comment;
            }

            selectedTab.Content = new RenovationRecommendationView(AccommodationReservation, Guest, selectedTab);
        }

        public void RateAccommodation(RenovationRecommendation recommendation, int recommendationId)
        {
            ratingService.CreateRating(Guest.Id, Guest, AccommodationReservation.Accommodation.Id, AccommodationReservation.Accommodation, Cleanliness, Fairness, Location, ValueForMoney, Comment, ImageList, recommendationId, recommendation);
            reservationService.ChangeReservationRatedState(AccommodationReservation);
        }
        public void AddRecommendation(int urgency, string recommendation)
        {
            RenovationRecommendation renovationRecommendation = recommendationService.GetNewRecommendation(urgency, recommendation);
            RateAccommodation(renovationRecommendation, renovationRecommendation.Id);
        }

        public void SkipRecommendation()
        {
            RateAccommodation(null, -1);
        }
    }
}
