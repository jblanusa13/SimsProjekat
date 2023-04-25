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
using ProjectSims.WPF.View.Guest1View.Requests;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Page, IObserver
    {
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public Guest1 Guest { get; set; }
        private AccommodationReservationService service;

        public MyReservations(Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;

            Guest = guest;

            service = new AccommodationReservationService();
            service.Subscribe(this);

            Reservations = new ObservableCollection<AccommodationReservation>(service.GetReservationByGuest(guest.Id));
        }

        private void DateChange_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                DateChangeRequest request = new DateChangeRequest(SelectedReservation);
                request.Show();
            }
        }
        private void MyRequests_Click(object sender, RoutedEventArgs e)
        {
            MyRequests myRequests = new MyRequests(Guest);
            myRequests.Show();
        }

        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in service.GetReservationByGuest(Guest.Id))
            {
                Reservations.Add(reservation);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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
