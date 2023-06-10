using ProjectSims.Domain.Model;
using ProjectSims.View.OwnerView.Pages;
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

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePageView : Page
    {
        public Owner Owner { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        public NavigationService NavService { get; set; }

        public HomePageView(Owner o, TextBlock titleTextBlock, NavigationService navService)
        {
            InitializeComponent();
            DataContext = this;
            Owner = o;
            TitleTextBlock = titleTextBlock;
            NavService = navService;
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
    }
}
