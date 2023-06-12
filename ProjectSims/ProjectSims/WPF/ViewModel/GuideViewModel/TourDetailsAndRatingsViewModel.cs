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
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class TourDetailsAndRatingsViewModel
    {
        private KeyPointService keyPointService;
        private ReservationTourService reservationTourService;
        private TourRatingService ratingService;
        private Guest2Service guestService;
        public RelayCommand HomeCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public NavigationService navigationService { get; set; }
        public int NumberOfPresentGuests { get; set; }
        public Tour SelectedTour { get; set; }
        public Guide Guide { get; set; }
        public TourAndGuideRating SelectedTourRating { get; set; }
        public List<TourAndGuideRating> TourRatings { get; set; }
        public TourDetailsAndRatingsViewModel(Tour selectedTour,NavigationService ns,Guide guide)
        {
            keyPointService = new KeyPointService();
            reservationTourService = new ReservationTourService();
            ratingService = new TourRatingService();
            guestService = new Guest2Service();
            navigationService = ns;
            SelectedTour = selectedTour;
            HomeCommand = new RelayCommand(Execute_HomeCommand);
            BackCommand = new RelayCommand(Execute_BackCommand);
            NumberOfPresentGuests = reservationTourService.GetNumberOfPresentGuests(selectedTour);
            TourRatings = ratingService.GetAllRatingsByTour(selectedTour);
            Guide = guide;
        }
        public void Execute_HomeCommand(object obj)
        {
            navigationService.Navigate(new HomeView(Guide, navigationService));
        }
        public void Execute_BackCommand(object obj)
        {
            navigationService.Navigate(new FinishedToursRatingsView(Guide,navigationService));
        }
    }
}
