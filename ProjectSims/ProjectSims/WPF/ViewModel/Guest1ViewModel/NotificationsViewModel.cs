using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class NotificationsViewModel : IObserver
    {
        public ObservableCollection<NotificationOwnerGuest> Notifications { get; set; }
        public Guest1 Guest { get; set; }
        private NotificationOwnerGuestService notificationService;
        public NotificationsViewModel(Guest1 guest)
        {
            notificationService = new NotificationOwnerGuestService();
            notificationService.Subscribe(this);
            Guest = guest;
            Notifications = new ObservableCollection<NotificationOwnerGuest>(notificationService.GetAllForGuest(Guest.Id));
            
        }

        public void Update()
        {
            Notifications.Clear();
            foreach (NotificationOwnerGuest notif in notificationService.GetAllForGuest(Guest.Id))
            {
                Notifications.Add(notif);
            }
        }
    }
}
