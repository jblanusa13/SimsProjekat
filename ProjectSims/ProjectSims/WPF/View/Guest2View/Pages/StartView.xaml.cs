using ProjectSims.Domain.Model;
using ProjectSims.Service;
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
using Syncfusion.UI.Xaml.Scheduler;

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : Page
    {
        public Guest2 guest2 { get; set; }
        private TourService tourService;
        private ReservationTourService reservationTourService;
        public List<ReservationTour> reservations { get; set; }
        public Tour tour { get; set; }
        public Image image { get; set; }
        public StartView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            guest2 = g;
            tour = tourService.GetMostVisitedTourLastMonth();
            reservations = reservationTourService.GetReservationsForGuest(guest2.Id);
            GuestNameLabel.Content = guest2.Name + "!";

            foreach (var fullFilePath in tour.Images)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                if (Uri.IsWellFormedUriString(fullFilePath, UriKind.RelativeOrAbsolute))
                {
                    bitmap.UriSource = new Uri(fullFilePath, UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();

                    image = new Image();

                    image.Source = bitmap;

                    image.Width = 370;
                    image.Height = 190;

                    ImageList.Items.Add(image);
                }
                else
                {
                    ImageList.Items.Add("Nije moguce utvriditi format URL adrese.");
                }
               
                ScheduleAppointmentCollection appointmentCollection = new ScheduleAppointmentCollection();

                foreach (ReservationTour reservation in reservations)
                {
                    ScheduleAppointment guest2Appoinment = new ScheduleAppointment();
                    guest2Appoinment.StartTime = reservation.Tour.StartOfTheTour;
                    guest2Appoinment.EndTime = reservation.Tour.StartOfTheTour.AddHours(reservation.Tour.Duration);
                    guest2Appoinment.Subject = reservation.Tour.Name;
                    if(reservation.Tour.State == TourState.Cancelled)
                    {
                        guest2Appoinment.AppointmentBackground = new SolidColorBrush(Colors.Red);
                    }else if(reservation.Tour.State == TourState.Active)
                    {
                        guest2Appoinment.AppointmentBackground = new SolidColorBrush(Colors.Green);
                    }
                    appointmentCollection.Add(guest2Appoinment);
                }
                Schedule.ItemsSource = appointmentCollection;

            }
        }

        private void Schedule_Loaded(object sender, RoutedEventArgs e)
        {
            Schedule.DisplayDate = DateTime.Now;
        }
    }
}
