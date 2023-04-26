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
using ProjectSims.WPF.View.Guest1View.RatingPages;
using ProjectSims.WPF.View.Guest1View;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for AccommodationReservationView.xaml
    /// </summary>
    public partial class GuestAccommodationsView : Page
    {
        public GuestAccommodationsViewModel ViewModel { get; set; }
        public Guest1 Guest { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public GuestAccommodationsView(Guest1 guest)
        {
            InitializeComponent();

            ViewModel = new GuestAccommodationsViewModel(guest);
            this.DataContext = ViewModel;

            Guest = guest;
        }
        private void MyReservations_Click(object sender, RoutedEventArgs e)
        {
            ChangeTab(2);
        }
        private void ShowRatings_Click(object sender, RoutedEventArgs e)
        {
            ChangeTab(4);
        }

        public void Reservation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodation = (Accommodation)AccommodationsTable.SelectedItem;
            if (SelectedAccommodation != null)
            {
                ChangeTab(6);
            }
        }

        private void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            RatingStartView accommodationForRating = new RatingStartView(Guest);
            accommodationForRating.Show();    
        }

        public void ChangeTab(int tabNum)
        {
            switch (tabNum)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        NavigationService.Navigate(new MyReservations(Guest));
                        break;
                    }
                case 4:
                    {
                        NavigationService.Navigate(new RatingsView());
                        break;
                    }
                case 5:
                    {
                        break;
                    }
                case 6:
                    {
                        NavigationService.Navigate(new AccommodationReservationView(SelectedAccommodation, Guest));
                        break;
                    }
            }
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
           ViewModel.Search(TextboxName.Text, TextboxCity.Text, TextboxCountry.Text, TextboxType.Text, TextboxGuests.Text, TextboxDays.Text);
        }
    }

}

