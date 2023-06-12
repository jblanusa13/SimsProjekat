using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.NotifAndHelp;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class NotificationsViewModel : IObserver
    {
        public ObservableCollection<NotificationOwnerGuest> Notifications { get; set; }
        public Guest1 Guest { get; set; }
        private NotificationOwnerGuestService notificationService;
        private RequestService requestService;
        private AccommodationReservationService reservationService;
        public MyICommand<NotificationsView> BackCommand { get; set; }
        public NotificationsViewModel(Guest1 guest)
        {
            notificationService = new NotificationOwnerGuestService();
            requestService = new RequestService();
            reservationService = new AccommodationReservationService();
            notificationService.Subscribe(this);
            Guest = guest;
            Notifications = new ObservableCollection<NotificationOwnerGuest>(notificationService.GetAllForGuest(Guest.Id));
            BackCommand = new MyICommand<NotificationsView>(OnBack);
        }

        public void Update()
        {
            Notifications.Clear();
            foreach (NotificationOwnerGuest notif in notificationService.GetAllForGuest(Guest.Id))
            {
                Notifications.Add(notif);
            }
        }

        private void OnBack(NotificationsView window)
        {
            window.Close();
        }
    }
}
