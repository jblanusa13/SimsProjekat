using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.View.GuideView;
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
using ProjectSims.Observer;
using System.Data;
using System.Xml.Linq;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for AvailableToursView.xaml
    /// </summary>
    public partial class AvailableToursView : Page, IObserver
    {
        private TourService tourService;
        private ReservationTourService reservationService;
        public ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; }
        public Guide Guide { get; set; }
        public AvailableToursView(Guide g)
        {
            InitializeComponent();
            DataContext = this;

            tourService = new TourService();
            tourService.Subscribe(this);
            reservationService = new ReservationTourService();
            Guide = g;
            TodayTours = new ObservableCollection<Tour>(tourService.GetToursByDateAndGuideId(DateTime.Now,Guide.Id));   
        }
        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
            if(tourService.GetTourByStateAndGuideId(TourState.Active,Guide.Id) == null)
            {
                SelectedTour = ((FrameworkElement)sender).DataContext as Tour;
                tourService.UpdateTourState(SelectedTour, TourState.Active);
                reservationService.UpdateGuestsState(SelectedTour, Guest2State.ActiveTour);
                this.NavigationService.Navigate(new TourTrackingView(SelectedTour, Guide));
            }
            else
            {
                MessageBox.Show("Vec postoji aktivna tura!");
            }
           
        }
        private void UpdateAvailableTours()
        {
            TodayTours.Clear();
            foreach (var tour in tourService.GetToursByDateAndGuideId(DateTime.Now,Guide.Id))
            {
                TodayTours.Add(tour);
            }
        }
        public void Update()
        {
            UpdateAvailableTours();
        }
    }
}
