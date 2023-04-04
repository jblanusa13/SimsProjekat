using ProjectSims.Controller;
using ProjectSims.Model;
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

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for Guest2StartingView.xaml
    /// </summary>
    public partial class Guest2StartingView : Window
    {
        private TourController tourController;
        private ReservationTourController reservationTourController;

        public Guest2 guest2 { get; set; }
        public Guest2StartingView(Guest2 g)
        {
            InitializeComponent();
            guest2 = g;
            UserBox.Text = "Korisnik : " + guest2.Name + " " + guest2.Surname;

            reservationTourController = new ReservationTourController();
            tourController = new TourController();

            if (reservationTourController.IsWaiting(guest2.Id))
            {
                Tour tour = tourController.FindStartedTour();
                MessageBoxResult answer = MessageBox.Show("Da li ste prisutni na turi " + tour.Name + "?", "", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    reservationTourController.ConfirmPresence(guest2.Id, tour);
                }
            }
        }

        private void ButtonSearchTour(object sender, RoutedEventArgs e)
        {
            ChangeTab(0);
        }

        private void ButtonShowFinishedTours(object sender, RoutedEventArgs e)
        {
            ChangeTab(1);
        }

        public void ChangeTab(int tabNum)
        {
            switch (tabNum)
            {
                case 0:
                    {
                        SelectedTab.Content = new SearchTourView(guest2);
                        break;
                    }
                case 1:
                    {
                        SelectedTab.Content = new FinishedToursView(guest2);
                        break;
                    }
            }
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            var startView = new MainWindow();
            startView.Show();
            Close();
        }
    }
}
