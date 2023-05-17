using ProjectSims.Service;
using ProjectSims.Domain.Model;
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
using ProjectSims.WPF.View.Guest2View.Pages;
using System.Windows.Threading;
using ProjectSims.WPF.ViewModel.Guest2ViewModel;

namespace ProjectSims.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for Guest2StartingView.xaml
    /// </summary>
    public partial class Guest2StartingView : Window
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;
        public Guest2 guest2 { get; set; }
        public Guest2StartingView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            SetStatusBarClock();
            guest2 = g;
            UserBox.Text = guest2.Name + " " + guest2.Surname;
            SelectedTab.Content = new StartView(guest2);

            reservationTourService = new ReservationTourService();
            tourService = new TourService();

            if (reservationTourService.GetTourIdWhereGuestIsWaiting(guest2) != null)
            {
                int tourId = reservationTourService.GetTourIdWhereGuestIsWaiting(guest2).TourId;
                Tour tour = tourService.GetTourById(tourId);
                MessageBoxResult answer = MessageBox.Show("Da li ste prisutni na turi " + tour.Name + "?", "", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    reservationTourService.UpdateGuestState(guest2,tour,Guest2State.Present);
                }
            }
        }

        private void SetStatusBarClock()
        {
            //Tred za prikazivanje sata i datuma
            this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd.MM.yyyy.");

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd.MM.yyyy.");
            }, this.Dispatcher);
        }

        private void ButtonStartView(object sender, RoutedEventArgs e)
        {
            ChangeTab(0);
        }
        private void ButtonSearchTour(object sender, RoutedEventArgs e)
        {
            ChangeTab(1);
        }
        private void ButtonShowFinishedTours(object sender, RoutedEventArgs e)
        {
            ChangeTab(2);
        }

        private void ButtonShowActiveTour(object sender, RoutedEventArgs e)
        {
            ChangeTab(3);
        }

        private void ButtonShowTourRequests(object sender, RoutedEventArgs e)
        {
            ChangeTab(4);
        }

        private void ButtonShowVouchers(object sender, RoutedEventArgs e)
        {
            ChangeTab(5);
        }
        private void ButtonAccount(object sender, RoutedEventArgs e)
        {
            ChangeTab(6);
        }
        private void Notification_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeTab(7);
        }
        private void Account_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeTab(6);
        }

        public void ChangeTab(int tabNum)
        {
            switch (tabNum)
            {
                case 0:
                    {
                        SelectedTab.Content = new StartView(guest2);
                        break;
                    }
                case 1:
                    {
                        SelectedTab.Content = new SearchTourView(guest2);
                        break;
                    }
                case 2:
                    {
                        FinishedToursViewModel finishedToursViewModel = new FinishedToursViewModel(guest2);
                        SelectedTab.Content = new FinishedToursView(finishedToursViewModel);
                        break;
                    }
                case 3:
                    {
                        ActivatedToursViewModel activatedToursViewModel = new ActivatedToursViewModel(guest2);
                        SelectedTab.Content = new ActivatedToursView(activatedToursViewModel);
                        break;
                    }
                case 4:
                    {
                        ShowTourRequestsViewModel showTourRequestsViewModel = new ShowTourRequestsViewModel(guest2);
                        SelectedTab.Content = new ShowTourRequestsView(showTourRequestsViewModel);
                        break;
                    }
                case 5:
                    {
                        ShowVouchersViewModel vouchersViewModel = new ShowVouchersViewModel(guest2);
                        SelectedTab.Content = new ShowVouchersView(vouchersViewModel);
                        break;
                    }
                case 6:
                    {
                        AccountViewModel accountViewModel = new AccountViewModel(guest2);
                        SelectedTab.Content = new AccountView(accountViewModel);
                        break;
                    }
                case 7:
                    {
                        ShowNotificationTourViewModel showNotificViewModel = new ShowNotificationTourViewModel(guest2);
                        SelectedTab.Content = new ShowNotificationTourView(showNotificViewModel);
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
