using ProjectSims.Controller;
using ProjectSims.Model;
using ProjectSims.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tulpep.NotificationWindow;

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class OwnerView : Window, INotifyPropertyChanged, IObserver
    {
        private readonly GuestAccommodationController _guestAccommodationController;
        public ObservableCollection<GuestAccommodation> GuestAccommodations { get; set; }
        public GuestAccommodation SelectedGuestAccommodation { get; set; }
        public OwnerView()
        {
            InitializeComponent();
            DataContext = this;

            _guestAccommodationController = new GuestAccommodationController();
            _guestAccommodationController.Subscribe(this);
            GuestAccommodations = new ObservableCollection<GuestAccommodation>(_guestAccommodationController.GetAllGuestAccommodations());
        }
       
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       private void RateGuest_Click(object sender, RoutedEventArgs e)
        {
            SelectedGuestAccommodation = (GuestAccommodation)GuestAccommodationsTable.SelectedItem;

            if (SelectedGuestAccommodation != null && SelectedGuestAccommodation.Rated == false)
            {
                GuestRatingtView guestRatingtView = new GuestRatingtView(SelectedGuestAccommodation, _guestAccommodationController);
                guestRatingtView.Show();
                
            }
            else
            {
                String message = "Gost " + SelectedGuestAccommodation.FirstName + " " + SelectedGuestAccommodation.LastName + " je vec ocijenjen!";
                MessageBox.Show(message, "Ocjenjivanje gosta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void RegistrateAccommodation_Click(object sender, RoutedEventArgs e)
        {
                AccommodationRegistrationView accommodationRegistrationView = new AccommodationRegistrationView();
                accommodationRegistrationView.Show();
        }

        public void Update()
        {
            GuestAccommodations.Clear();
            foreach (GuestAccommodation guestAccommodation in _guestAccommodationController.GetAllGuestAccommodations()) 
            {
                GuestAccommodations.Add(guestAccommodation);
            }
        }
    }
}
