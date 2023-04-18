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

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for ActivatedToursView.xaml
    /// </summary>
    public partial class ActivatedToursView : Page
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;

        public ObservableCollection<Tour> ListTour { get; set; }
        public Tour SelectedTour { get; set; }
        public Guest2 guest2 { get; set; }
        public ActivatedToursView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            guest2 = g;
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            ListTour = new ObservableCollection<Tour>(GetToursWhichActivatedWhereGuestPresent());

        }

        public List<Tour> GetToursWhichActivatedWhereGuestPresent()
        {
            List<Tour> toursActivated = new List<Tour>();
            foreach (int id in reservationTourService.FindTourIdsWhereGuestPresent(guest2.Id))
            {
                Tour tour = tourService.GetActivatedTourById(id);
                if (tour != null)
                {
                    toursActivated.Add(tour);
                }
            }
            return toursActivated;
        }

        private void ButtonTrackingTour(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                var ratingTourWindow = new Guest2TrackingTourView(SelectedTour);
                ratingTourWindow.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati turu koju zelite da pratite!");
            }
        }
    }
}
