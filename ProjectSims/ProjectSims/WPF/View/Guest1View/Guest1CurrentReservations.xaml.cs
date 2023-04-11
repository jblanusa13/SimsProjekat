using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View;

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for Guest1CurrentReservations.xaml
    /// </summary>
    public partial class Guest1CurrentReservations : Window, IObserver
    {
        public ObservableCollection<AccommodationReservation> MyReservations { get; set; }   
        public AccommodationReservation SelectedReservation { get; set; }
        private Guest1 guest;
        private AccommodationReservationService service;
        public Guest1CurrentReservations(Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;

            this.guest = guest;

            service = new AccommodationReservationService();
            service.Subscribe(this);

            MyReservations = new ObservableCollection<AccommodationReservation>(service.GetReservationByGuest(guest.Id));
        }

        private void DateChange_Click(object sender, RoutedEventArgs e)
        {
            SelectedReservation = (AccommodationReservation)ReservationsTable.SelectedItem;

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
            MyReservations.Clear();
            foreach(AccommodationReservation reservation in service.GetReservationByGuest(guest.Id))
            {
                MyReservations.Add(reservation);
            }
        }
    }
}
