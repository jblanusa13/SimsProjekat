using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourDetailsAndStatisticsView.xaml
    /// </summary>
    public partial class TourDetailsAndStatisticsView : Page
    {
        private KeyPointService keyPointService;
        private TourService tourService;
        private ReservationTourService reservationTourService;
        private int NumberOfPresentGuestsUnder18 { get; set; }
        private int NumberOfPresentGuestsBetween18And50 { get; set; }
        private int NumberOfPresentGuestsOver50 { get; set; }
        private int NumberOfPresentGuests { get; set; }
        private double PercentageOfGuestsWithVoucher { get; set; }
        private Tour Tour { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public TourDetailsAndStatisticsView(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            keyPointService = new KeyPointService();
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            Tour = selectedTour;
            NumberOfPresentGuestsUnder18 = reservationTourService.GetNumberOfPresentGuestsByAgeLimit(Tour, 0);
            NumberOfPresentGuestsBetween18And50 = reservationTourService.GetNumberOfPresentGuestsByAgeLimit(Tour, 18);
            NumberOfPresentGuestsOver50 = reservationTourService.GetNumberOfPresentGuestsByAgeLimit(Tour, 50);
            NumberOfPresentGuests = NumberOfPresentGuestsUnder18 + NumberOfPresentGuestsBetween18And50 + NumberOfPresentGuestsOver50;
            PercentageOfGuestsWithVoucher = reservationTourService.GetPercentageOfPresentGuestsWithVoucher(Tour);
            TitleTextBox.Text = Tour.Name + "," + Tour.StartOfTheTour.ToString("dd/MM/yyyy HH:mm");
            LocationTextBox.Text = Tour.Location;
            LanguageTextBox.Text = Tour.Language;
            DurationTextBox.Text = Tour.Duration.ToString();
            PresentGuestsTextBox.Text = NumberOfPresentGuests.ToString();
            GuestsUnder18TextBox.Text = NumberOfPresentGuestsUnder18.ToString();
            GuestsBetween18And50TextBox.Text = NumberOfPresentGuestsBetween18And50.ToString();
            GuestsOver50TextBox.Text = NumberOfPresentGuestsOver50.ToString();
            GuestsWithVoucherTextBox.Text = PercentageOfGuestsWithVoucher.ToString();
            GuestsWithoutVoucherTextBox.Text = (100 - PercentageOfGuestsWithVoucher).ToString();

            KeyPoints = new List<KeyPoint>();
            foreach(int keyPointId in Tour.KeyPointIds)
            {
                KeyPoints.Add(keyPointService.GetKeyPointById(keyPointId));
            }
           
        }
    }
}
