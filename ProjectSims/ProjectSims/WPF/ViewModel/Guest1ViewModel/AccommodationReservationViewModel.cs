using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.Commands;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class AccommodationReservationViewModel : INotifyPropertyChanged, IDataErrorInfo
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
        private string _guestNumber;
        public string GuestNumber
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

        private string _daysNumber;
        public string DaysNumber
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

        public DateRanges SelectedDates { get; set; }

        private AccommodationReservationService reservationService;
        public Guest1 Guest { get; set; }
        public RelayCommand FindDatesCommand { get; set; }
        public RelayCommand ThemeCommand { get; set; }
        public RelayCommand ConfirmCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand FirstChangedCommand { get; set; }
        public RelayCommand LastChangedCommand { get; set; }
        public NavigationService NavService { get; set; }
        public AccommodationReservationView ReservationView { get; set; }

        public AccommodationReservationViewModel(Guest1 guest, Accommodation SelectedAccommodation, AccommodationReservationView reservationView, NavigationService navigation)

        {
            Guest = guest;
            Accommodation = SelectedAccommodation;
            ReservationView = reservationView;
            NavService = navigation;
            Username = guest.User.Username;
            AvailableDates = new ObservableCollection<DateRanges>();
            reservationService = new AccommodationReservationService();

            FindDatesCommand = new RelayCommand(Execute_FindDatesCommand, CanExecute_FindDatesCommand);
            ConfirmCommand = new RelayCommand(Execute_ConfirmCommand, CanExecute_ConfirmCommand);
            ThemeCommand = new RelayCommand(Execute_ThemeCommand);
            CancelCommand = new RelayCommand(Execute_CancelCommand);
            FirstChangedCommand = new RelayCommand(Execute_FirstChangedCommand);
            LastChangedCommand = new RelayCommand(Execute_LastChangedCommand);
            ValidateFirst();
            ValidateLast();
        }

        public void Execute_CancelCommand(object obj)
        {
            NavService.GoBack();
        }

        public bool CanExecute_FindDatesCommand(object obj)
        {
            return IsValid && ReservationView.FirstDatePicker.SelectedDate != null &&
                        ReservationView.LastDatePicker.SelectedDate != null;
        }

        public void Execute_FindDatesCommand(object obj)
        {
            AccommodationScheduleService scheduleService = new AccommodationScheduleService();
            List<DateRanges> availableDates = new List<DateRanges>();
            DateOnly firstDate = DateOnly.FromDateTime((DateTime)ReservationView.FirstDatePicker.SelectedDate);
            DateOnly lastDate = DateOnly.FromDateTime((DateTime)ReservationView.LastDatePicker.SelectedDate);

            if (scheduleService.FindDates(firstDate, lastDate, Convert.ToInt32(DaysNumber), Accommodation.Id).Count == 0)
            {
                availableDates = scheduleService.FindAlternativeDates(firstDate, lastDate, Convert.ToInt32(DaysNumber), Accommodation.Id);
            }
            else
            {
                availableDates = scheduleService.FindDates(firstDate, lastDate, Convert.ToInt32(DaysNumber), Accommodation.Id);
            }

            UpdateDatesTable(availableDates);
        }

        public bool CanExecute_ConfirmCommand(object obj)
        {
            return SelectedDates != null;
        }

        public void Execute_ConfirmCommand(object obj)
        {
            Reserve();
            NavService.GoBack();
        }
        public void Reserve()
        {
            reservationService.CreateReservation(Accommodation.Id, Guest.Id, SelectedDates.CheckIn, SelectedDates.CheckOut, Convert.ToInt32(GuestNumber));
            NavService.GoBack();
        }
        public void UpdateDatesTable(List<DateRanges> availableDates)
        {
            AvailableDates.Clear();
            foreach (DateRanges dateRange in availableDates)
            {
                AvailableDates.Add(dateRange);
            }
        }
        private void Execute_ThemeCommand(object obj)
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
        public void Execute_FirstChangedCommand(object obj)
        {
            ReservationView.LastDatePicker.DisplayDateStart = ReservationView.FirstDatePicker.SelectedDate;
            ValidateFirst();
        }
        public void Execute_LastChangedCommand(object obj)
        {
            ReservationView.FirstDatePicker.DisplayDateEnd = ReservationView.LastDatePicker.SelectedDate;
            ValidateLast();
        }

        public void ValidateFirst()
        {
            if (ReservationView.FirstDatePicker.SelectedDate == null)
            {
                ReservationView.ValidateFirst.Content = "Unesite pocetni datum!";
            }
            else
            {
                ReservationView.ValidateFirst.Content = "";
            }
        }

        public void ValidateLast()
        {
            if (ReservationView.LastDatePicker.SelectedDate == null)
            {
                ReservationView.ValidateLast.Content = "Unesite krajnji datum!";
            }
            else
            {
                ReservationView.ValidateLast.Content = "";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public String Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "DaysNumber")
                {
                    if (string.IsNullOrEmpty(DaysNumber))
                        return "Unesite broj dana!";

                    int daysNumber;
                    if (!int.TryParse(DaysNumber, out daysNumber))
                        return "Unesite ceo broj!";

                    if (daysNumber < Accommodation.MinimumReservationDays)
                        return "Mora biti veci od min broja dana!";

                    if (daysNumber <= 0)
                        return "Unesite broj veci od 0!";
                }
                else if (columnName == "GuestNumber")
                {
                    if (string.IsNullOrEmpty(GuestNumber))
                        return "Unesite broj gostiju!";

                    int guestNumber;
                    if (!int.TryParse(GuestNumber, out guestNumber))
                        return "Unesite ceo broj!";

                    if (guestNumber > Accommodation.GuestsMaximum)
                        return "Mora biti manji od maks broja gostiju!";

                    if (guestNumber <= 0)
                        return "Unesite broj veci od 0!";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "GuestNumber", "DaysNumber" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null || ReservationView.FirstDatePicker.SelectedDate == null ||
                        ReservationView.LastDatePicker.SelectedDate == null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
