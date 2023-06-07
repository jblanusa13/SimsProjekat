using ProjectSims.Domain.Model;
using ProjectSims.Observer;
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
    /// Interaction logic for ShowTourRequestsView.xaml
    /// </summary>
    public partial class ShowTourRequestsView : Page
    {
        public ShowTourRequestsViewModel viewModel;
        public ShowTourRequestsView(ShowTourRequestsViewModel showTourRequestsViewModel)
        {
            InitializeComponent();
            this.DataContext = showTourRequestsViewModel;
            viewModel = showTourRequestsViewModel;
        }
        private void ButtonCreateRequest(object sender, RoutedEventArgs e)
        {
            viewModel.ButtonCreateRequest(sender);
        }
        private void ImageAndLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.ImageAndLabel_MouseLeftButtonDown(sender);
        }
        private void Statistic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.Statistic_MouseLeftButtonDown(sender);
        }

        private void ButtonCreateRequestForComplexTour (object sender, RoutedEventArgs e)
        {
            viewModel.ButtonCreateRequestForComplexTour(sender);
        }

        private void DetailsAboutRequestClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedRequest != null)
            {
                this.NavigationService.Navigate(new DetailsAboutRequestView(viewModel.SelectedRequest));
            }
            else
            {
                MessageBox.Show("Morate selektovati zahtjev za koji zelite da vidite detalje.");
            }
        }

        private void DetailsAboutComplexRequestClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedComplexRequest != null)
            {
                this.NavigationService.Navigate(new DetailsAboutComplexRequestView(viewModel.SelectedComplexRequest));
            }
            else
            {
                MessageBox.Show("Morate selektovati zahtjev za koji zelite da vidite detalje.");
            }
        }

        /*private void Home_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Guest2StartingView(viewModel.guest2));
        }*/
    }
}
