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
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.WPF.View.Guest1View.RatingPages
{
    /// <summary>
    /// Interaction logic for AccommodationsForRatingView.xaml
    /// </summary>
    public partial class AccommodationsForRatingView : Page, IObserver
    {
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public Guest1 Guest { get; set; }
        private AccommodationReservationService reservationService;
        public AccommodationsForRatingView(Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;

            reservationService = new AccommodationReservationService();

            Guest = guest;
            Reservations = new ObservableCollection<AccommodationReservation>(reservationService.GetAccommodationsForRating(Guest));

            BackButton.Focus();
        }
        private void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            SelectedReservation = (AccommodationReservation)AccommodationsForRatingTable.SelectedItem;
            if (SelectedReservation != null)
            {
                NavigationService.Navigate(new AccommodationRatingView(SelectedReservation));
            }
        }

        private void Reservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedReservation = (AccommodationReservation)AccommodationsForRatingTable.SelectedItem;
        }

        private void Rate(object sender, KeyEventArgs e)
        {
            if (SelectedReservation != null)
            {
                if ((e.Key.Equals(Key.Enter)) || (e.Key.Equals(Key.Return)))
                {
                    NavigationService.Navigate(new AccommodationRatingView(SelectedReservation));
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            RatingStartView startView = (RatingStartView)Window.GetWindow(this);
            startView.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            RatingStartView startView = (RatingStartView)Window.GetWindow(this);
            startView.Close();
        }

        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in reservationService.GetAccommodationsForRating(Guest))
            {
                Reservations.Add(reservation);
            }
        }
    }
}
