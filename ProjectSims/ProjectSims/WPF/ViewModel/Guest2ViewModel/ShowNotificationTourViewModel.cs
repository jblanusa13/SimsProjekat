using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest2View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class ShowNotificationTourViewModel : IObserver
    {
        private NotificationTourService notificationTourService;
        public ObservableCollection<NotificationTour> ListNotification { get; set; }
        public NotificationTour SelectedNotification { get; set; }
        public Guest2 guest2 { get; set; }

        public ShowNotificationTourViewModel(Guest2 g)
        {
            guest2 = g;
            notificationTourService = new NotificationTourService();
            notificationTourService.Subscribe(this);
            ListNotification = new ObservableCollection<NotificationTour>
                                        (notificationTourService.GetAllNotificationsByGuest2(guest2.Id));
        }

        public void OpenNotificationAboutNewTours_PreviewMouseDown(object sender)
        {
            if (SelectedNotification != null)
            {
                var openWindow = new NotificationNewTourView(SelectedNotification);
                openWindow.Show();
            }
        }

        private void UpdateNotificationList()
        {
            ListNotification.Clear();
            foreach (var n in notificationTourService.GetAllNotificationsByGuest2(guest2.Id))
            {
                ListNotification.Add(n);
            }
        }
        public void Update()
        {
            UpdateNotificationList();
        }
    }
}
