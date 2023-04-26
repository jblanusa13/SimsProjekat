using ProjectSims.Service;
using ProjectSims.Domain.Model;
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
using ProjectSims.WPF.View.OwnerView;

namespace ProjectSims.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class AccommodationsDisplay : Page, INotifyPropertyChanged, IObserver
    {
        private readonly GuestAccommodationService guestAccommodationService;
        private Owner owner;
        private OwnerService ownerService;
        public ObservableCollection<GuestAccommodation> GuestAccommodations { get; set; }
        public GuestAccommodation SelectedGuestAccommodation { get; set; }
        
        public AccommodationsDisplay(Owner owner)
        {
            InitializeComponent();
            DataContext = this;

            guestAccommodationService = new GuestAccommodationService();
            guestAccommodationService.Subscribe(this);
            GuestAccommodations = new ObservableCollection<GuestAccommodation>(guestAccommodationService.GetAllGuestAccommodations());

            this.owner = owner;
            ownerService = new OwnerService();
            if (ownerService.HasWaitingRequests(owner.Id))
            {
                MessageBox.Show("Imate zahteve na cekanju!");
            }
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
                OwnerStartingView ownerStartingView = (OwnerStartingView)Window.GetWindow(this);
                ownerStartingView.SelectedTab.Content = new GuestRatingView(SelectedGuestAccommodation, guestAccommodationService, owner);
            }
            else if(SelectedGuestAccommodation == null)
            {
                //Do nothing
            }
            else
            {
               //Guest is rated
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
            foreach (GuestAccommodation guestAccommodation in guestAccommodationService.GetAllGuestAccommodations()) 
            {
                GuestAccommodations.Add(guestAccommodation);
            }
        }
    }
}
