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
using ProjectSims.Observer;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for ScheduledToursView.xaml
    /// </summary>
    public partial class ScheduledToursView : Page, IObserver
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;
        private Guest2Service guest2Service;
        public ObservableCollection<Tour> ScheduledTours { get; set; }
        public Tour SelectedTour { get; set; }
        public Guide Guide { get; set; }
        public ScheduledToursView(Guide guide)
        {
            InitializeComponent();
            DataContext = this;
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            guest2Service = new Guest2Service();
            tourService.Subscribe(this);
            Guide = guide;
            ScheduledTours = new ObservableCollection<Tour>(tourService.GetToursByStateAndGuideId(TourState.Inactive,Guide.Id));
            SelectedTour = new Tour();
        }

        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            SelectedTour = ((FrameworkElement)sender).DataContext as Tour;
            if ((SelectedTour.StartOfTheTour - DateTime.Now).TotalHours < 48)
            {
                MessageBox.Show("Tura pocinje za manje od 48 sati i ne moze se otkazati!");
                return;
            }
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da otkazete turu?", "", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {               
                SelectedTour = ((FrameworkElement)sender).DataContext as Tour;
                tourService.UpdateTourState(SelectedTour,TourState.Cancelled);
                List<int> guestIds = reservationTourService.GetGuestIdsByStateAndTourId(SelectedTour,Guest2State.InactiveTour);
                if (guestIds.Count > 0)
                {
                    guestIds.ForEach(id => guest2Service.GiveVoucher(id));
                }
            }
        }
        public void Update()
        {
            ScheduledTours.Clear();
            foreach (var tour in tourService.GetToursByStateAndGuideId(TourState.Inactive, Guide.Id))
            {
                ScheduledTours.Add(tour);
            }
        }
    }
}

