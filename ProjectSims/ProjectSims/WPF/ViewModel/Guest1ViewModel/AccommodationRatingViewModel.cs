using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.View.Guest1View.RatingPages;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public partial class AccommodationRatingViewModel
    {
        private AccommodationReservationService reservationService;
        private AccommodationRatingService ratingService;
        private RenovationRecommendationService recommendationService;
        private RatingsView ratingsView;
        public AccommodationReservation AccommodationReservation { get; set; }
        public int Cleanliness { get; set; }
        public int Fairness { get; set; }
        public int Location { get; set; }
        public int ValueForMoney { get; set; }
        public string Comment { get; set; }
        public List<string> ImageList { get; set; }
        public RenovationRecommendation RenovationRecommendation { get; set; }
        public int RecommendationId { get; set; }

        public AccommodationRatingViewModel(AccommodationReservation accommodationReservation)
        {
            reservationService = new AccommodationReservationService();
            ratingService = new AccommodationRatingService();
            recommendationService = new RenovationRecommendationService();

            AccommodationReservation = accommodationReservation;

            ImageList = new List<string>();
        }

        public void AddRating(string cleanliness, string fairness, string location, string valueForMoney, string comment, List<string> images)
        {
            Cleanliness = Convert.ToInt32(cleanliness);
            Fairness = Convert.ToInt32(fairness);
            Location = Convert.ToInt32(location);
            ValueForMoney = Convert.ToInt32(valueForMoney);

            foreach(var image in images)
            {
                ImageList.Add(image);
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

        public void AddRecommendation(int urgency, string recommendation)
        {
            RenovationRecommendation = recommendationService.GetNewRecommendation(urgency, recommendation);
            RecommendationId = RenovationRecommendation.Id;
        }
        public void SkipRecommendation()
        {
            RenovationRecommendation = null;
            RecommendationId = -1;
        }

        public void RateAcommodation()
        {
            ratingService.CreateRating(AccommodationReservation.Id, AccommodationReservation, Cleanliness, Fairness, Location, ValueForMoney, Comment, ImageList, RecommendationId, RenovationRecommendation);
            reservationService.ChangeReservationRatedState(AccommodationReservation);
        }
    }
}
