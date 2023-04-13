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
        private Tour Tour { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public TourDetailsAndStatisticsView(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            keyPointService = new KeyPointService();
            tourService = new TourService();
            Tour = selectedTour;
            TitleTextBox.Text = Tour.Name + "," + Tour.StartOfTheTour.ToString("dd/MM/yyyy HH:mm");
            LocationTextBox.Text = Tour.Location;
            LanguageTextBox.Text = Tour.Language;
            DurationTextBox.Text = Tour.Duration.ToString();
            PresenceGuestsTextBox.Text = (Tour.NumberOfPresentGuestsUnder18 + Tour.NumberOfPresentGuestsBetween18And50 + Tour.NumberOfPresentGuestsOver50).ToString();
            KeyPoints = new List<KeyPoint>();
            foreach(int keyPointId in Tour.KeyPointIds)
            {
                KeyPoints.Add(keyPointService.GetKeyPointById(keyPointId));
            }
            GuestsUnder18TextBox.Text = Tour.NumberOfPresentGuestsUnder18.ToString();
            GuestsBetween18And50TextBox.Text = Tour.NumberOfPresentGuestsBetween18And50.ToString();
            GuestsOver50TextBox.Text = Tour.NumberOfPresentGuestsOver50.ToString();

        }
    }
}
