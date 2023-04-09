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
            TodayTours = new ObservableCollection<Tour>(tourService.GetTodayTours(Guide.Id));
        }
        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
            SelectedTour = ((FrameworkElement)sender).DataContext as Tour;
            tourService.UpdateTourState(SelectedTour, TourState.Active);
            reservationService.UpdateGuestsState(SelectedTour,Guest2State.ActiveTour);
            Page tourTrackingView = new TourTrackingView(SelectedTour, Guide);
            //otvori novi page i onemoguci da se pritiska na dugmad ako postoji aktivna tura
        }
        private void UpdateAvailableTours()
        {
            TodayTours.Clear();
            foreach (var tour in tourService.GetTodayTours(Guide.Id))
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
