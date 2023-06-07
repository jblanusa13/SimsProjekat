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

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for Forum.xaml
    /// </summary>
    public partial class Forum : Page
    {
        public Forum()
        {
            InitializeComponent();
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;

            if (App.IsDark)
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                App.IsDark = false;
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                App.IsDark = true;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        public void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var login = new MainWindow();
            login.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}
