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
        static Timer timer;
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

            //NotifyOwner();
        }
        /*
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        private int DateDifference(DateOnly dateOnly1, DateOnly dateOnly2)
        {
            return (new DateTime(dateOnly1.Year, dateOnly1.Month, dateOnly1.Day) - new DateTime(dateOnly2.Year, dateOnly2.Month, dateOnly2.Day)).Days;
        }

        private void NotifyOwner() 
        {
            foreach (var item in GuestAccommodations)
            {
                if (item.Rated == false) 
                {
                    if (today.CompareTo(item.CheckOutDate) > 0)    //Guest left 
                    {
                        int interval = DateDifference(today, item.CheckOutDate);
                        if (interval >= 0 && interval <= 5)
                        { 
                            DateTime nowTime = DateTime.Now;    //Timer started
                            DateTime scheduledTime = nowTime.AddDays(5-interval); //Specify your scheduled time HH,MM,SS
                            
                            var totalMilliSecondsPerDay = (5-interval)*TimeSpan.FromDays(1).TotalMilliseconds;
                            var timer = new Timer(totalMilliSecondsPerDay);
                            // double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
                            // timer = new Timer(tickTime);
                            timer.Start();
                            timer.Elapsed += new ElapsedEventHandler(Notification_Click) ;
                        } 
                    }
                }   
            }
        }

        private void Notification_Click(object? sender, EventArgs e)
        {
            PopupNotifier notification = new PopupNotifier();
            notification.AnimationDuration = 10;
            notification.ContentText = "Niste ocijenili ";// + _guestAccommodationController.GetAllGuestAccommodations().Find().FirstName + " " + item.LastName + "!";
            notification.TitleText = "Ocjenjivanje gosta";
            notification.Click += Notification_Click;
            notification.Popup();
        }
        */
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
