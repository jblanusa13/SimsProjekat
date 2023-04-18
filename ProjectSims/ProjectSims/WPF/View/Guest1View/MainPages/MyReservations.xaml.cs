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
using ProjectSims.View.Guest1View;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Page, IObserver
    {
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        private Guest1 guest;
        private AccommodationReservationService service;
        private Frame selectedTab;

        public MyReservations(Guest1 guest, Frame selectedTab)
        {
            InitializeComponent();
            DataContext = this;

            this.guest = guest;

            service = new AccommodationReservationService();
            service.Subscribe(this);

            Reservations = new ObservableCollection<AccommodationReservation>(service.GetReservationByGuest(guest.Id));

            this.selectedTab = selectedTab;
        }

        private void DateChange_Click(object sender, RoutedEventArgs e)
        {
            //1SelectedReservation = (AccommodationReservation)ReservationsTable.SelectedItem;

            if (SelectedReservation != null)
            {
                DateChangeRequest request = new DateChangeRequest(SelectedReservation);
                request.Show();
            }
        }
        private void MyRequests_Click(object sender, RoutedEventArgs e)
        {
            MyRequests myRequests = new MyRequests(guest);
            myRequests.Show();
        }

        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in service.GetReservationByGuest(guest.Id))
            {
                if (reservation.State == ReservationState.Active)
                {
                    Reservations.Add(reservation);
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            selectedTab.Content = new GuestAccommodationsView(guest, selectedTab);
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            SelectedReservation = (AccommodationReservation)ReservationsTable.SelectedItem;
            if(SelectedReservation != null)
            {
                if (service.CanCancel(SelectedReservation))
                {
                    service.RemoveReservation(SelectedReservation);
                    MessageBox.Show("Uspesno ste otkazali rezervaciju");
                }
                else
                {
                    MessageBox.Show("Rok za otkazivanje je prosao, ne mozete otkazati rezervaciju");
                }
            }
        }
    }
}
