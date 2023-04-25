using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
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
        public int Cleanliness { get; set; }
        public int Fairness { get; set; }
        public int Location { get; set; }
        public int ValueForMoney { get; set; }
        public string Comment { get; set; }
        public List<string> ImageList { get; set; }

        public AccommodationRatingViewModel(AccommodationReservation accommodationReservation)
        {
            reservationService = new AccommodationReservationService();
            ratingService = new AccommodationRatingService();
            recommendationService = new RenovationRecommendationService();

            AccommodationReservation = accommodationReservation;

            ImageList = new List<string>();
        }

        public void Confirm(string cleanliness, string fairness, string location, string valueForMoney, string comment, string imagesString)
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
        }

        public void RateAcommodation(RenovationRecommendation renovationRecommendation, int recommendationId)
        {
            ratingService.CreateRating(AccommodationReservation.Id, AccommodationReservation, Cleanliness, Fairness, Location, ValueForMoney, Comment, ImageList, recommendationId, renovationRecommendation);
            reservationService.ChangeReservationRatedState(AccommodationReservation);
        }

        public void AddRecommendation(int urgency, string recommendation)
        {
            RenovationRecommendation renovationRecommendation = recommendationService.GetNewRecommendation(urgency, recommendation);
            RateAcommodation(renovationRecommendation, renovationRecommendation.Id);
        }
        public void SkipRecommendation()
        {
            RateAcommodation(null, -1);
        }
    }
}
