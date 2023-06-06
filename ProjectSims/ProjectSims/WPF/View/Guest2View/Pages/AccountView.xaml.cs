using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.WPF.ViewModel.Guest2ViewModel;
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
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : Page
    {
        AccountViewModel viewModel;
        public AccountView(AccountViewModel accountViewModel)
        {
            InitializeComponent();
            DataContext = accountViewModel;
            viewModel = accountViewModel;
            
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            ShowNotificationTourViewModel showNotificationTourViewModel = new ShowNotificationTourViewModel(viewModel.guest2);
            this.NavigationService.Navigate(new ShowNotificationTourView(showNotificationTourViewModel));
            
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var startView = new MainWindow();
            startView.Show();
            var currentWindow = Window.GetWindow(this);
            currentWindow.Close();
        }

        private void Home_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new StartView(viewModel.guest2));
        }
    }
}
