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
using ProjectSims.WPF.View.Guest1View.NotifAndHelp;

namespace ProjectSims.WPF.View.Guest1View.BarPages
{
    /// <summary>
    /// Interaction logic for NotifWindowBar.xaml
    /// </summary>
    public partial class NotifWindowBar : Page
    {
        public NotifWindowBar()
        {
            InitializeComponent();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            NotificationsView startView = (NotificationsView)Window.GetWindow(this);
            startView.Close();
        }
    }
}
