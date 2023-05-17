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
using ProjectSims.Domain.Model;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : Page
    {
        public AccountView(Guide g)
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var startView = new MainWindow();
            startView.Show();
            Window.GetWindow(this).Close();
        }
    }
}
