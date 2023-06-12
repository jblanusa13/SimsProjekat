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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.NotifAndHelp;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Guest1 Guest { get; set; }
        private SuperGuestService superGuestService;
        public Profile(Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;
            Guest = guest;
            superGuestService = new SuperGuestService();
            CheckSuperGuest();
            BackButton.Focus();
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;

            if (App.IsDark)
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                App.IsDark = false;
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                App.IsDark = true;
            }
        }

        private void ButtonLanguage_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;

            if (App.CurrentLanguage == "sr-LATN")
            {
                app.ChangeLanguage("en-US");
                App.CurrentLanguage = "en-US";
            }
            else
            {
                app.ChangeLanguage("sr-LATN");
                App.CurrentLanguage = "sr-LATN";
            }
        }

        private void ButtonNotifications_Click(object sender, RoutedEventArgs e)
        {
            NotificationsView notificationsView = new NotificationsView(Guest);
            notificationsView.Show();
        }

        public void CheckSuperGuest()
        {
            if (Guest.SuperGuestId != -1)
            {
                SuperGuestView();
            }
            else
            {
                NotSuperGuestView();
            }

        }

        public void SuperGuestView()
        {
            CaptionFirst.Content = "Vi ste";
            CaptionSecond.Content = "Super gost!";
            ReservationTb.Text = superGuestService.GetReservationNumberForGuestInLastYear(Guest.Id).ToString();
            BonusPointsTb.Text = Guest.SuperGuest.BonusPoints.ToString();
        }

        public void NotSuperGuestView()
        {
            CaptionFirst.Content = "Koliko jos da biste postali";
            CaptionSecond.Content = "Super gost";
            ReservationTb.Text = superGuestService.GetReservationNumberForGuestInLastYear(Guest.Id).ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var login = new MainWindow();
            login.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}
