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
using ProjectSims.View.Guest1View;

namespace ProjectSims.WPF.View.Guest1View.BarPages
{
    /// <summary>
    /// Interaction logic for WindowBar.xaml
    /// </summary>
    public partial class WindowBar : Page
    {
        public WindowBar()
        {
            InitializeComponent();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            Guest1StartView startView = (Guest1StartView)Window.GetWindow(this);
            startView.WindowState = WindowState.Minimized;
        }
        private void Size_Click(object sender, RoutedEventArgs e)
        {
            Guest1StartView startView = (Guest1StartView)Window.GetWindow(this);
            if(startView.WindowState == WindowState.Normal)
            {
                startView.WindowState = WindowState.Maximized;
                SizeButton.Content = FindResource("MainWindowRestoreIcon");
            }
            else
            {
                startView.WindowState = WindowState.Normal;
                SizeButton.Content = FindResource("MainWindowMaximizeIcon");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Guest1StartView startView = (Guest1StartView)Window.GetWindow(this);
            startView.Close();
        }
    }
}
