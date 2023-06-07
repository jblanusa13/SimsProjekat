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
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for AnytimeAnywhere.xaml
    /// </summary>
    public partial class AnytimeAnywhere : Page
    {
        AnywhereAnytimeViewModel viewModel;

        public AnytimeAnywhere(Guest1 guest)
        {
            InitializeComponent();
            viewModel = new AnywhereAnytimeViewModel(FirstDatePicker, LastDatePicker, DatesTable, guest);
            this.DataContext = viewModel;
        }

        private void ShowDates(object sender, KeyEventArgs e)
        {
            if ((e.Key.Equals(Key.Enter)) || (e.Key.Equals(Key.Return)))
            {
                viewModel.ShowDates();
            }
        }

        private void Reserve(object sender, KeyEventArgs e)
        {
            if ((e.Key.Equals(Key.Enter)) || (e.Key.Equals(Key.Return)))
            {
                viewModel.Reserve();
                NavigationService.GoBack();
            }
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
