using ProjectSims.Domain.Model;
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

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for DetailsAboutRequestView.xaml
    /// </summary>
    public partial class DetailsAboutRequestView : Page
    {
        public TourRequest request { get; set; }
        public Guest2 guest2 { get; set; }
        public DetailsAboutRequestView(TourRequest r,Guest2 g)
        {
            InitializeComponent();
            this.DataContext = this;
            request = r;
            guest2 = g;
            DateStartbox.Text = request.DateRangeStart.ToString("dd.MM.yyyy");
            DateEndbox.Text = request.DateRangeEnd.ToString("dd.MM.yyyy");
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Zahtjevi_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Home_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new StartView(guest2));
        }
    }
}
