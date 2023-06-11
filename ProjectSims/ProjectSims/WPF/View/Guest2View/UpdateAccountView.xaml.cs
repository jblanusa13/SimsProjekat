using ProjectSims.Domain.Model;
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
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for UpdateAccountView.xaml
    /// </summary>
    public partial class UpdateAccountView : Window
    {
        private UpdateAccountViewModel viewModel;
        public UpdateAccountView(Guest2 guest2)
        {
            InitializeComponent();
            viewModel =  new UpdateAccountViewModel(guest2);
            this.DataContext = viewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateButton_Clcik(object sender, RoutedEventArgs e)
        {
            if (viewModel.UpdateAccountData())
            {
                Close();
            }
        }

        private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
