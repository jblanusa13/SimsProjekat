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

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for ShowNotificationTourView.xaml
    /// </summary>
    public partial class ShowNotificationTourView : Page
    {
        private NotificationTourService notificationTourService;
        public ObservableCollection<NotificationTour> ListNotification { get; set; }
        public NotificationTour SelectedNotification { get; set; }
        public Guest2 guest2 { get; set; }
        public ShowNotificationTourView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            guest2 = g;
            notificationTourService = new NotificationTourService();
            ListNotification = new ObservableCollection<NotificationTour>
                                        (notificationTourService.GetAllNotificationsByGuest2(guest2.Id));
        }
        
        private void OpenNotificationAboutNewTours_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var openWindow = new NotificationNewTourView(SelectedNotification);
            openWindow.Show();          
        }
    }
}
