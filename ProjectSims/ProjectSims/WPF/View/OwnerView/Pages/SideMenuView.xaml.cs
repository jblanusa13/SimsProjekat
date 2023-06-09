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
        public TextBlock TitleTextBlock { get; set; }
        public SideMenuView(Owner o, TextBlock titleTextBlock)
        {
            InitializeComponent();
            DataContext = this;
            Owner = o;
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
            Window parentWindow = Window.GetWindow(this);
            this.NavigationService.Navigate(new HomePageView(Owner, TitleTextBlock));
            TitleTextBlock.Text = "Početna stranica";
        }

        private void Accommodations_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AccommodationsDisplayView(Owner, TitleTextBlock));
            TitleTextBlock.Text = "Smještaji";
        }

        private void Ratings_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OwnerRatingsDisplayView(Owner));
            TitleTextBlock.Text = "Recenzije";
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProfileView(Owner));
            TitleTextBlock.Text = "Profil";
        }

        private void Renovations_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RenovationsView(Owner, TitleTextBlock));
            TitleTextBlock.Text = "Renoviranja";
        }
    }
}
