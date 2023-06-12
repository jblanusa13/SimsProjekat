using GalaSoft.MvvmLight.Views;
using LiveCharts;
using LiveCharts.Wpf;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class TourDetailsAndStatisticsViewModel
    {
        private KeyPointService keyPointService;
        private ReservationTourService reservationTourService;
        public int NumberOfPresentGuestsUnder18 { get; set; }
        public int NumberOfPresentGuestsBetween18And50 { get; set; }
        public int NumberOfPresentGuestsOver50 { get; set; }
        public int NumberOfPresentGuests { get; set; }
        public double PercentageOfGuestsWithVoucher { get; set; }
        public double PercentageOfGuestsWithoutVoucher { get; set; }
        public RelayCommand HomeCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public NavigationService navigationService { get; set; }
        public Guide Guide { get; set; }
        public Tour SelectedTour { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public TourDetailsAndStatisticsViewModel(Tour selectedTour,NavigationService ns,Guide guide)
        {
            keyPointService = new KeyPointService();
            reservationTourService = new ReservationTourService();
            navigationService = ns;
            SelectedTour = selectedTour;
            NumberOfPresentGuestsUnder18 = reservationTourService.GetNumberOfPresentGuestsByAgeLimit(SelectedTour, 0);
            NumberOfPresentGuestsBetween18And50 = reservationTourService.GetNumberOfPresentGuestsByAgeLimit(SelectedTour, 18);
            NumberOfPresentGuestsOver50 = reservationTourService.GetNumberOfPresentGuestsByAgeLimit(SelectedTour, 50);
            NumberOfPresentGuests = NumberOfPresentGuestsUnder18 + NumberOfPresentGuestsBetween18And50 + NumberOfPresentGuestsOver50;
            PercentageOfGuestsWithVoucher = reservationTourService.GetPercentageOfPresentGuestsWithVoucher(SelectedTour);
            PercentageOfGuestsWithoutVoucher = 100 - reservationTourService.GetPercentageOfPresentGuestsWithVoucher(SelectedTour);
            KeyPoints = new List<KeyPoint>();
            Guide = guide;
            foreach (int keyPointId in SelectedTour.KeyPointIds)
            {
                KeyPoints.Add(keyPointService.GetKeyPointById(keyPointId));
            }
            SeriesCollection = new SeriesCollection();
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "procenat gostiju sa vaucerom",
                Values = new ChartValues<double> { PercentageOfGuestsWithVoucher },
            });
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "procenat gostiju bez vauceroa",
                Values = new ChartValues<double> { PercentageOfGuestsWithoutVoucher },
            });
        }
        public void Execute_HomeCommand(object obj)
        {
            navigationService.Navigate(new HomeView(Guide, navigationService));
        }
        public void Execute_BackCommand(object obj)
        {
            navigationService.Navigate(new FinishedToursStatisticsView(Guide, navigationService));
        }
    }
}
