using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class AccommodationRatingViewModel : INotifyPropertyChanged
    {
        private AccommodationReservationService reservationService;
        private AccommodationRatingService ratingService;
        private RenovationRecommendationService recommendationService;
        public AccommodationReservation AccommodationReservation { get; set; }
        private string cleanliness;
        public string Cleanliness
        {
            get => cleanliness;
            set
            {
                if (value != cleanliness)
                {
                    cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        private string fairness;
        public string Fairness
        {
            get => fairness;
            set
            {
                if (value != fairness)
                {
                    fairness = value;
                    OnPropertyChanged();
                }
            }
        }

        private string location;
        public string Location
        {
            get => location;
            set
            {
                if (value != location)
                {
                    location = value;
                    OnPropertyChanged();
                }
            }
        }

        private string valueForMoney;
        public string ValueForMoney
        {
            get => valueForMoney;
            set
            {
                if (value != valueForMoney)
                {
                    valueForMoney = value;
                    OnPropertyChanged();
                }
            }
        }
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

        public void AddRating(string comment, List<string> images)
        {

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
            ratingService.CreateRating(AccommodationReservation.Id, AccommodationReservation, Convert.ToInt32(Cleanliness), Convert.ToInt32(Fairness), Convert.ToInt32(Location), Convert.ToInt32(ValueForMoney), Comment, ImageList, RecommendationId, RenovationRecommendation);
            reservationService.ChangeReservationRatedState(AccommodationReservation);
        }
        public String Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Cleanliness")
                {
                    if (string.IsNullOrEmpty(Cleanliness))
                        return "Odaberite ocenu";
                }
                else if (columnName == "Fairness")
                {
                    if (string.IsNullOrEmpty(Fairness))
                        return "Odaberite ocenu";
                }
                else if (columnName == "Location")
                {
                    if (string.IsNullOrEmpty(Location))
                        return "Odaberite ocenu";
                }
                else if (columnName == "ValueForMoney")
                {
                    if (string.IsNullOrEmpty(ValueForMoney))
                        return "Odaberite ocenu";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "Cleanliness", "Location", "ValueForMoney", "Fairness" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
