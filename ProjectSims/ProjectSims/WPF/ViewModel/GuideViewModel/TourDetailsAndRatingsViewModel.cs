using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class TourDetailsAndRatingsViewModel
    {
        private KeyPointService keyPointService;
        private ReservationTourService reservationTourService;
        private TourRatingService ratingService;
        private Guest2Service guestService;
        public int NumberOfPresentGuests { get; set; }
        public Tour SelectedTour { get; set; }
        public TourAndGuideRating SelectedTourRating { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public List<TourAndGuideRating> TourRatings { get; set; }
        public TourDetailsAndRatingsViewModel(Tour selectedTour)
        {
            keyPointService = new KeyPointService();
            reservationTourService = new ReservationTourService();
            ratingService = new TourRatingService();
            guestService = new Guest2Service();
            SelectedTour = selectedTour;
            NumberOfPresentGuests = reservationTourService.GetNumberOfPresentGuests(selectedTour);
            KeyPoints = new List<KeyPoint>();
            foreach (int keyPointId in selectedTour.KeyPointIds)
            {
                KeyPoints.Add(keyPointService.GetKeyPointById(keyPointId));
            }
            TourRatings = ratingService.GetAllRatingsByTour(selectedTour);
        }
    }
}
