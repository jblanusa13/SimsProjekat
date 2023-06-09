using ProjectSims.Domain.Model;
using ProjectSims.Repository;
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
    /// Interaction logic for ShowVouchersView.xaml
    /// </summary>
    public partial class ShowVouchersView : Page
    {
        ShowVouchersViewModel viewModel;
        public ShowVouchersView(ShowVouchersViewModel vouchersViewModel)
        {
            InitializeComponent();
            this.DataContext = vouchersViewModel;
            viewModel = vouchersViewModel;
        }
        private void Home_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new StartView(viewModel.guest2));
        }

        private void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
