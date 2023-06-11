using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest2View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class NotificationNewTourViewModel
    {
        private NotificationTourService notificationTourService;
        public NotificationTour notificationTour { get; set; }
        public ObservableCollection<Tour> ListTour { get; set; }
        public Tour SelectedTour { get; set; }

        private TourService tourService;

        public NotificationNewTourViewModel(NotificationTour notification)
        {
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
            foreach (int id in notification.TourIds)
            {
                tours.Add(tourService.GetTourById(id));
            }
            ListTour = new ObservableCollection<Tour>(tours);
        }

        public void SeeMoreDetails()
        {
            if (SelectedTour != null)
            {
                var see_more = new DetailsAndReservationTourView(SelectedTour, notificationTour.Guest2);
                see_more.Show();
            }
            else
            {
                MessageBox.Show("Morate selektovati turu da bi ste vidjeli vise detalja o njoj!");
            }
        }
    }
}
