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
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for NotificationNewTourView.xaml
    /// </summary>
    public partial class NotificationNewTourView : Window
    {
        private NotificationTourService notificationTourService;
        public NotificationTour notificationTour { get; set; }
        public ObservableCollection<Tour> ListTour { get; set; }
        public Tour SelectedTour { get; set; }
        private TourService tourService;
        public NotificationNewTourView(NotificationTour notification)
        {
            InitializeComponent();
            DataContext = this;
            notificationTour = notification;
            tourService = new TourService();
            notificationTourService = new NotificationTourService();
            LoadTourFromListInt(notificationTour);
            if (!notification.Seen)
            {
                notification.Seen = true;
                notificationTourService.Update(notification);
            }
        }

        private void LoadTourFromListInt(NotificationTour notification)
        {
            List<Tour> tours = new List<Tour>();
            foreach(int id in notification.TourIds)
            {
                tours.Add(tourService.GetTourById(id));
            }
            ListTour = new ObservableCollection<Tour>(tours);
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SeeMoreDetailsButton(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                var see_more = new DetailsAndReservationTourView(SelectedTour,notificationTour.Guest2);
                see_more.Show();
            }
            else
            {
                MessageBox.Show("Morate selektovati turu da bi ste vidjeli vise detalja o njoj!");
            }
        }
    }
}
