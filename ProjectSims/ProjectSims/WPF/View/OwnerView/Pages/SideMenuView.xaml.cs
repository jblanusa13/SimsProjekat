using ProjectSims.Domain.Model;
using ProjectSims.View.OwnerView.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class SideMenuView : Page
    {
        public Owner Owner { get; set; }
        public NavigationService NavService { get; set; }
        public OwnerStartingView Window { get; set; }

        public SideMenuView(Owner o, OwnerStartingView window, NavigationService navService)
        {
            InitializeComponent();
            DataContext = this;
            Owner = o;
            NavService = navService;
            Window = window;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var login = new MainWindow();
            login.Show();
            Window.Close();
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new HomePageView(Owner, NavService, Window));
            Window.PageTitle = "Početna stranica";
        }

        private void Accommodations_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new AccommodationsDisplayView(Owner, NavService, Window));
            Window.PageTitle = "Smještaji";
        }

        private void Ratings_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new OwnerRatingsDisplayView(Owner, NavService));
            Window.PageTitle = "Recenzije";
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new ProfileView(Owner, NavService));
            Window.PageTitle = "Profil";
        }

        private void Renovations_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new RenovationsView(Owner, Window, NavService));
            Window.PageTitle = "Renoviranja";
        }

        private void Forum_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new ForumsDisplayView(Owner, Window, NavService));
            Window.PageTitle = "Forumi"; 
        }
        
        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new TutorialView(Owner, NavService));
            Window.PageTitle = "Tutorijal"; 
        }
    }
}
