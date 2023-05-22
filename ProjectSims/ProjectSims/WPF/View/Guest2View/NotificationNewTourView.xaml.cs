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
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for NotificationNewTourView.xaml
    /// </summary>
    public partial class NotificationNewTourView : Window
    {
        public NotificationNewTourViewModel viewModel;
        public NotificationNewTourView(NotificationNewTourViewModel notificationNewTourViewModel)
        {
            InitializeComponent();
            DataContext = notificationNewTourViewModel;
            viewModel = notificationNewTourViewModel;
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SeeMoreDetailsButton(object sender, RoutedEventArgs e)
        {
            viewModel.SeeMoreDetailsButton(sender);
            
        }
    }
}
