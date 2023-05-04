using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for AccommodationReservation.xaml
    /// </summary>
    public partial class AccommodationReservationView : Page, INotifyPropertyChanged
    {
        public Accommodation Accommodation { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _firstDate;
        public DateOnly FirstDate
        {
            get => _firstDate;
            set
            {
                if (value != _firstDate)
                {
                    _firstDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _lastDate;
        public DateOnly LastDate
        {
            get => _lastDate;
            set
            {
                if (value != _lastDate)
                {
                    _lastDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _guestNumber;
        public int GuestNumber
        {
            get => _guestNumber;
            set
            {
                if (value != _guestNumber)
                {
                    _guestNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _daysNumber;
        public int DaysNumber
        {
            get => _daysNumber;
            set
            {
                if (value != _daysNumber)
                {
                    _daysNumber = value;
                    OnPropertyChanged();
                }
            }
        }


        public ObservableCollection<DateRanges> AvailableDates { get; set; }

        public DateRanges SelectedDates;

        private AccommodationReservationService reservationService;

        public Guest1 Guest { get; set; }
        public AccommodationReservationView(Accommodation SelectedAccommodation, Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;

            Guest = guest;
            reservationService = new AccommodationReservationService();

            Accommodation = SelectedAccommodation;
            Username = guest.User.Username;
            AvailableDates = new ObservableCollection<DateRanges>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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


        private void FindDates_Click(object sender, RoutedEventArgs e)
        {
            DateRangesService dateRangesService = new DateRangesService();
            if (FirstDatePicker.SelectedDate != null && LastDatePicker.SelectedDate != null && !string.IsNullOrEmpty(TextboxDaysNumber.Text))
            {
                FirstDate = DateOnly.FromDateTime((DateTime)FirstDatePicker.SelectedDate);
                LastDate = DateOnly.FromDateTime((DateTime)LastDatePicker.SelectedDate);

                List<DateRanges> availableDates = new List<DateRanges>();
                availableDates = dateRangesService.FindAvailableDates(FirstDate, LastDate, DaysNumber, Accommodation.Id);

                UpdateDatesTable(availableDates);
            }
        }

        public void UpdateDatesTable(List<DateRanges> availableDates)
        {
            AvailableDates.Clear();
            foreach (DateRanges dateRange in availableDates)
            {
                AvailableDates.Add(dateRange);
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            SelectedDates = (DateRanges)DatesTable.SelectedItem;
            if (SelectedDates != null)
            {
                DateRanges dates = (DateRanges)DatesTable.SelectedItem;

                reservationService.CreateReservation(Accommodation.Id, Guest.Id, dates.CheckIn, dates.CheckOut, GuestNumber);
                NavigationService.GoBack();
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
