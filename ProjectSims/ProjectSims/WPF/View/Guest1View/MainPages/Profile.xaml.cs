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

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Guest1 Guest { get; set; }
        private SuperGuestService superGuestService;
        private bool isDark;
        public Profile(Guest1 guest, bool isDark)
        {
            InitializeComponent();
            DataContext = this;
            Guest = guest;
            superGuestService = new SuperGuestService();
            CheckSuperGuest();

            this.isDark = isDark;
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;

            if (isDark)
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                isDark = false;
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                isDark = true;
            }
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
