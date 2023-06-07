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
        public DetailsAboutRequestView(TourRequest r)
        {
            InitializeComponent();
            this.DataContext = this;
            request = r;
            DateStartbox.Text = request.DateRangeStart.ToString();
            DateEndbox.Text = request.DateRangeEnd.ToString();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Zahtjevi_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
