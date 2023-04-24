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

namespace ProjectSims.WPF.View.Guest1View.RatingPages
{
    /// <summary>
    /// Interaction logic for AccommodationsForRatingView.xaml
    /// </summary>
    public partial class AccommodationsForRatingView : Page, IObserver
    {
        public ObservableCollection<AccommodationReservation> Accommodations { get; set; }
        public AccommodationReservation SelectedAccommodation { get; set; }
        public Guest1 Guest { get; set; }
        private Frame selectedTab;
        private AccommodationReservationService reservationService;
        public AccommodationsForRatingView(Guest1 guest, Frame selectedTab)
        {
            InitializeComponent();
            DataContext = this;

            reservationService = new AccommodationReservationService();
            this.selectedTab = selectedTab;

            Guest = guest;
            Accommodations = new ObservableCollection<AccommodationReservation>(reservationService.GetAccommodationsForRating(Guest));
        }
        private void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodation = (AccommodationReservation)AccommodationsForRatingTable.SelectedItem;
            if (SelectedAccommodation != null)
            {
                selectedTab.Content = new AccommodationRatingView(SelectedAccommodation, Guest, selectedTab);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //Close();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (AccommodationReservation reservation in reservationService.GetAccommodationsForRating(Guest))
            {
                Accommodations.Add(reservation);
            }
        }
    }
}
