using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ProjectSims.Observer;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourDetailsAndRatingsView.xaml
    /// </summary>
    public partial class TourDetailsAndRatingsView : Page
    {
        private KeyPointService keyPointService;
        private TourService tourService;
        private ReservationTourService reservationTourService;
        private TourRatingService ratingService;
        private int NumberOfPresentGuests { get; set; }
        private Tour Tour { get; set; }
        private TourAndGuideRating SelectedTourRating { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public List<TourAndGuideRating> TourRatings { get; set; }
        public TourDetailsAndRatingsView(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            keyPointService = new KeyPointService();
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            ratingService = new TourRatingService();
            Tour = selectedTour;
            NumberOfPresentGuests = reservationTourService.GetNumberOfPresentGuests(Tour);
            TitleTextBox.Text = Tour.Name + "," + Tour.StartOfTheTour.ToString("dd/MM/yyyy HH:mm");
            LocationTextBox.Text = Tour.Location;
            LanguageTextBox.Text = Tour.Language;
            DurationTextBox.Text = Tour.Duration.ToString();
            PresentGuestsTextBox.Text = NumberOfPresentGuests.ToString();
            KeyPoints = new List<KeyPoint>();
            foreach (int keyPointId in Tour.KeyPointIds)
            {
                KeyPoints.Add(keyPointService.GetKeyPointById(keyPointId));
            }
            TourRatings = ratingService.GetAllRatingsByTour(Tour);

        }
        public void ViewComment_Click(object sender, EventArgs e)
        {
            SelectedTourRating = ((FrameworkElement)sender).DataContext as TourAndGuideRating;
            if (SelectedTourRating != null)
            {
                this.NavigationService.Navigate(new CommentAndRatingsView(SelectedTourRating,Tour));
            }
        }
    }
}
