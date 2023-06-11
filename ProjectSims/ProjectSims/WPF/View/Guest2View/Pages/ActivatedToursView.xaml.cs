using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.ViewModel.Guest2ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ActivatedToursView.xaml
    /// </summary>
    public partial class ActivatedToursView : Page
    {
        public ActivatedToursViewModel viewModel;
        public ActivatedToursView(ActivatedToursViewModel activatedToursViewModel)
        {
            InitializeComponent();
            this.DataContext = activatedToursViewModel;
            viewModel = activatedToursViewModel;
        }

        private void ButtonTrackingTour(object sender, RoutedEventArgs e)
        {
            viewModel.TrackingTour();
        }
        private void Home_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new StartView(viewModel.guest2));
        }
    }
}
