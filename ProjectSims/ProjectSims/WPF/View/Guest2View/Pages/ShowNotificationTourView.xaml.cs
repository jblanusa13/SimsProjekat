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
    /// Interaction logic for ShowNotificationTourView.xaml
    /// </summary>
    public partial class ShowNotificationTourView : Page 
    {
        public ShowNotificationTourViewModel viewModel;
        public ShowNotificationTourView(ShowNotificationTourViewModel showNotificationTourViewModel)
        {
            InitializeComponent();
            DataContext = showNotificationTourViewModel;
            viewModel = showNotificationTourViewModel;
        }
        private void OpenNotificationAboutNewTours_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.OpenNotificationAboutNewTours();
        }
        private void Home_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new StartView(viewModel.guest2));
        }
    }
}
