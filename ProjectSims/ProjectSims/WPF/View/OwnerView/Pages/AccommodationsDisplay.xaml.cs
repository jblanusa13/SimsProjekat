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
using ProjectSims.WPF.ViewModel.OwnerViewModel;
using ProjectSims.WPF.View.GuideView.Pages;

namespace ProjectSims.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class AccommodationsDisplay : Page
    {
        public AccommodationsDisplayViewModel accommodationsDisplayViewModel;
        public Owner Owner { get; set; }
        public AccommodationReservation SelectedAccommodationReservation { get; set; }
        
        public AccommodationsDisplay(Owner o)
        {
            InitializeComponent();
            Owner = o;
            accommodationsDisplayViewModel = new AccommodationsDisplayViewModel(Owner);
            this.DataContext = accommodationsDisplayViewModel;
            NotifyAboutRequest();
        }

        public void NotifyAboutRequest()
        {
            if (accommodationsDisplayViewModel.HasWaitingRequests(Owner))
            {
                MessageBox.Show("Imate zahteve na cekanju!");
            }
        }

        private void RateGuest_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)AccommodationReservationsTable.SelectedItem;

            if (SelectedAccommodationReservation != null 
                && accommodationsDisplayViewModel.IsNotRated(SelectedAccommodationReservation) 
                && accommodationsDisplayViewModel.IsLessThan5Days(SelectedAccommodationReservation))
            {
                this.NavigationService.Navigate(new GuestRatingView(SelectedAccommodationReservation, Owner));
            }
            else if(SelectedAccommodationReservation == null)
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
            this.NavigationService.Navigate(new AccommodationRegistrationView(Owner));
        }
    }
}
