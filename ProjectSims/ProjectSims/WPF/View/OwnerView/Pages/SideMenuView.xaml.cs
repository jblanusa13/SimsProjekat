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
        public TextBlock TitleTextBlock { get; set; }

        public SideMenuView(Owner o, TextBlock titleTextBlock, NavigationService navService)
        {
            InitializeComponent();
            DataContext = this;
            Owner = o;
            NavService = navService;
            TitleTextBlock = titleTextBlock;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var login = new MainWindow();
            login.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new HomePageView(Owner, TitleTextBlock, NavService));
            TitleTextBlock.Text = "Početna stranica";
        }

        private void Accommodations_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new AccommodationsDisplayView(Owner, TitleTextBlock, NavService));
            TitleTextBlock.Text = "Smještaji";
        }

        private void Ratings_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new OwnerRatingsDisplayView(Owner, NavService));
            TitleTextBlock.Text = "Recenzije";
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new ProfileView(Owner, NavService));
            TitleTextBlock.Text = "Profil";
        }

        private void Renovations_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new RenovationsView(Owner, TitleTextBlock, NavService));
            TitleTextBlock.Text = "Renoviranja";
        }

        private void Forum_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new ForumsDisplayView(Owner, NavService));
            TitleTextBlock.Text = "Forumi"; 
        }
        
        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new TutorialView(Owner, NavService));
            TitleTextBlock.Text = "Tutorijal"; 
        }
    }
}
