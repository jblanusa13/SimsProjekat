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
    /// Interaction logic for AnytimeAnywhere.xaml
    /// </summary>
    public partial class AnytimeAnywhere : Page
    {
        public AnytimeAnywhere()
        {
            InitializeComponent();
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;

            if (ButtonTheme.Content == FindResource("SunIcon"))
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                ButtonTheme.Content = FindResource("MoonIcon");
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                ButtonTheme.Content = FindResource("SunIcon");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
