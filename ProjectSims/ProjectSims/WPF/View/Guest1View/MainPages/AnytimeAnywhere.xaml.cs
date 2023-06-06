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
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for AnytimeAnywhere.xaml
    /// </summary>
    public partial class AnytimeAnywhere : Page
    {
        private bool isDark;
        AnywhereAnytimeViewModel viewModel;
        public AnytimeAnywhere(bool isDark)
        {
            InitializeComponent();
            this.isDark = isDark;
            viewModel = new AnywhereAnytimeViewModel();
            this.DataContext = viewModel;
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;

            if (isDark)
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                isDark = false;
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                isDark = true;
            }
        }

        private void FirstDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LastDatePicker.DisplayDateStart = FirstDatePicker.SelectedDate;
        }

        private void LastDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FirstDatePicker.DisplayDateEnd = LastDatePicker.SelectedDate;
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
