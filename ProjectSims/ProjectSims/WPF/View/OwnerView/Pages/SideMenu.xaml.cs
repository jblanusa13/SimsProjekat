using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class SideMenu : Page
    {
        public Owner owner { get; set; }
        public SideMenu(Owner o)
        {
            InitializeComponent();
            DataContext = this;
            owner = o;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
            var startView = new MainWindow();
            startView.Show();
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            OwnerStartingView ownerStartingView = (OwnerStartingView)Window.GetWindow(this);
            ownerStartingView.ChangeTab(3); 
        }

        private void Accommodations_Click(object sender, RoutedEventArgs e)
        {
            OwnerStartingView ownerStartingView = (OwnerStartingView)Window.GetWindow(this);
            ownerStartingView.ChangeTab(4);
        }
    }
}
