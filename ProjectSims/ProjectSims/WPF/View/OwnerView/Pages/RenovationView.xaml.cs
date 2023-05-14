using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
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

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for Renovation.xaml
    /// </summary>
    public partial class RenovationView : Page
    {
        public Owner Owner { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public RenovationViewModel renovationViewModel { get; set; }

        public RenovationView(Owner owner, TextBlock titleTextBlock, Accommodation selectedAccommodation)
        {
            InitializeComponent();
            Owner = owner;
            TitleTextBlock = titleTextBlock;
            SelectedAccommodation = selectedAccommodation;
            renovationViewModel = new RenovationViewModel(SelectedAccommodation, Owner);
            this.DataContext = renovationViewModel;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if ((DateRanges)DateRangesDataGrid.SelectedItem != null && !string.IsNullOrWhiteSpace(DescriptionTextBox.Text)) 
            {
                renovationViewModel.CreateRenovation((DateRanges)DateRangesDataGrid.SelectedItem, DescriptionTextBox.Text);
                this.NavigationService.Navigate(new AccommodationsDisplay(Owner, TitleTextBlock));
                TitleTextBlock.Text = "Smještaji";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AccommodationsDisplay(Owner, TitleTextBlock));
            TitleTextBlock.Text = "Smještaji";
        }
        
        private void FirstDatePickerDateChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (SecondDatePicker.SelectedDate != null && !string.IsNullOrEmpty(DurationTextBox.Text))
            {
                renovationViewModel.FindAvailableDates(DateOnly.FromDateTime((DateTime)FirstDatePicker.SelectedDate), DateOnly.FromDateTime((DateTime)SecondDatePicker.SelectedDate), Convert.ToInt32(DurationTextBox.Text));
            }
        }

        private void SecondDatePickerDateChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (FirstDatePicker.SelectedDate != null && !string.IsNullOrEmpty(DurationTextBox.Text))
            {
                renovationViewModel.FindAvailableDates(DateOnly.FromDateTime((DateTime)FirstDatePicker.SelectedDate), DateOnly.FromDateTime((DateTime)SecondDatePicker.SelectedDate), Convert.ToInt32(DurationTextBox.Text));
            }
        }

        private void DurationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SecondDatePicker.SelectedDate != null && FirstDatePicker.SelectedDate != null && !string.IsNullOrWhiteSpace(DurationTextBox.Text))
            {
                renovationViewModel.FindAvailableDates(DateOnly.FromDateTime((DateTime)FirstDatePicker.SelectedDate), DateOnly.FromDateTime((DateTime)SecondDatePicker.SelectedDate), Convert.ToInt32(DurationTextBox.Text));
            }
        }
        
        private void DescriptonTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            if (source != null)
            {
                source.Background = Brushes.MintCream;
                source.Foreground = Brushes.Black;
                if (source.Text == "Unesite opis...")
                {
                    source.Clear();
                }
            }
        }

        private void DescriptonTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            if (source != null)
            {
                source.Background = Brushes.White;
                source.Foreground = Brushes.Black;
            }
        }
    }
}
