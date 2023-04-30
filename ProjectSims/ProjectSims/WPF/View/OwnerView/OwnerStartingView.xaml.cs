using ProjectSims.Domain.Model;
using ProjectSims.View.OwnerView;
using ProjectSims.WPF.View.Guest2View.Pages;
using ProjectSims.WPF.View.OwnerView.Pages;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using System.IO;
using ProjectSims.Repository;

namespace ProjectSims.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerStartingView.xaml
    /// </summary>
    public partial class OwnerStartingView : Window
    {
        public Owner owner { get; set; }
        private AccommodationReservationRepository accommodationReservationRepository;
        private GuestAccommodationRepository guestAccommodationRepository;
        public OwnerStartingView(Owner o)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTab.Content = new HomePage(owner);
            owner = o;
            accommodationReservationRepository = new AccommodationReservationRepository();
            guestAccommodationRepository = new GuestAccommodationRepository();
        }
        private Boolean IsAnyGuestRatable() 
        {
            List<AccommodationReservation> reservations = accommodationReservationRepository.GetAll();
            foreach (var item in reservations)
            { 
                if (DateOnly.FromDateTime(DateTime.Today).CompareTo(item.CheckOutDate) > 0)
                {
                    if (guestAccommodationRepository.GetById(item.Id).Rated == false) 
                    {
                        return true;   
                    }
                }
            }
            return false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string fileName = "../../../Resources/Data/lastShown.csv";

            try
            {
                string lastShownText = File.ReadAllText(fileName);
                DateOnly lastShownDate = DateOnly.Parse(lastShownText);

                if (lastShownDate < DateOnly.FromDateTime(DateTime.Today)) 
                {
                    if (IsAnyGuestRatable())
                    {
                        MessageBox.Show("Imate neocijenjenih gostiju!", "Ocjenjivanje gostiju", MessageBoxButton.OK);
                        File.WriteAllText(fileName, DateOnly.FromDateTime(DateTime.Today).ToString());
                    }
                }
            }
            catch (Exception)
            {
              
            }
        }

        private void ButtonMenu(object sender, RoutedEventArgs e)
        {
            ChangeTab(0);
        }

        private void ButtonRequests(object sender, RoutedEventArgs e)
        {
            ChangeTab(1);
        }

        private void ButtonNotifications(object sender, RoutedEventArgs e)
        {
            ChangeTab(2);
        }

        public void ChangeTab(int tabNum)
        {
            switch (tabNum)
            {
                case 0:
                    {
                        SelectedTab.Content = new SideMenu(owner);
                        break;
                    }
                case 1:
                    {
                        SelectedTab.Content = new Requests(owner);
                        break;
                    }
                case 2:
                    {
                        //SelectedTab.Content = new SideMenu(owner);
                        break;
                    }
                case 3:
                    {
                        SelectedTab.Content = new HomePage(owner);
                        break;
                    }
                case 4:
                    {
                        SelectedTab.Content = new AccommodationsDisplay(owner);
                        break;
                    }
                case 5:
                    {
                        //SelectedTab.Content = new GuestRatingView(owner);
                        break;
                    }
            }
        }
    }
}
