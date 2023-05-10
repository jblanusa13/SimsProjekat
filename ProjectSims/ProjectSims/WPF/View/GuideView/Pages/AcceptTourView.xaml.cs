using ProjectSims.Domain.Model;
using ProjectSims.WPF.ViewModel.GuideViewModel;
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

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for AcceptTourView.xaml
    /// </summary>
    public partial class AcceptTourView : Page
    {
        public TourRequest TourRequest { get; set; }
        public Guide Guide { get; set; }
        public AcceptTourView(TourRequest tourRequest,Guide guide)
        {
            InitializeComponent();
            this.DataContext = new AcceptTourViewModel(tourRequest);
            Guide = guide;
            TourRequest=tourRequest;
        }
       public void AcceptTour_Click(object sender, RoutedEventArgs e) 
        {
            this.NavigationService.Navigate(new CreateTourView(Guide, TourRequest));
        }
       public void Back_Click(object sender, RoutedEventArgs e) 
        { 
            this.NavigationService.GoBack();
        }
    }
}
