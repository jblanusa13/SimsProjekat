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
using ProjectSims.Domain.Model;
using ProjectSims.WPF.View.Guest1View.BarPages;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.NotifAndHelp
{
    /// <summary>
    /// Interaction logic for NotificationsView.xaml
    /// </summary>
    public partial class NotificationsView : Window
    {
        public NotificationsView(Guest1 guest)
        {
            InitializeComponent();
            StatusBarFrame.Content = new NotifStatusBar();
            WindowBarFrame.Content = new NotifWindowBar();
            DataContext = new NotificationsViewModel(guest);
        }
    }
}
