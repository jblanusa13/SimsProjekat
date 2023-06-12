using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.ViewModel.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Interaction logic for FinishedToursRatingsView.xaml
    /// </summary>
    public partial class FinishedToursRatingsView : Page
    {
        public Tour SelectedTour { get; set; }
        public Guide Guide { get; set; }
        public NavigationService navigationService { get; set; }
        public FinishedToursRatingsView(Guide guide,NavigationService ns)
        {
            InitializeComponent();
            Guide = guide;
            this.DataContext = new FinishedToursRatingsViewModel(guide);
            navigationService = ns;
        }
        private void TourInfo_Click(object sender, RoutedEventArgs e)
        {
            SelectedTour = ((FrameworkElement)sender).DataContext as Tour;
            if (SelectedTour != null)
            {
               this.NavigationService.Navigate(new TourDetailsAndRatingsView(SelectedTour,navigationService,Guide));
            }
        }
    }
}

