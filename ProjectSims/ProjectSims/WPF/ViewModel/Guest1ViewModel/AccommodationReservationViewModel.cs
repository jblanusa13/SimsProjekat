using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using ProjectSims.Domain.Model;
using ProjectSims.Service;

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

        public DateRanges SelectedDates;

        private AccommodationReservationService reservationService;
        public Guest1 Guest { get; set; }
        public MyICommand FindDatesCommand { get; set; }
        public MyICommand ConfirmCommand { get; set; }

        public AccommodationReservationViewModel(Guest1 guest, Accommodation SelectedAccommodation, NavigationService navService)

        {
            Guest = guest;
            Accommodation = SelectedAccommodation;
            Username = guest.User.Username;
            AvailableDates = new ObservableCollection<DateRanges>();
            reservationService = new AccommodationReservationService();

            FindDatesCommand = new MyICommand(OnFind, CanFind);
            ConfirmCommand = new MyICommand(OnConfirm, CanConfirm);
        }

        public bool CanFind()
        {
            return IsValid;
        }

        public void OnFind()
        {
            AccommodationScheduleService scheduleService = new AccommodationScheduleService();
            List<DateRanges> availableDates = new List<DateRanges>();

            if (scheduleService.FindDates(FirstDate, LastDate, Convert.ToInt32(DaysNumber), Accommodation.Id).Count == 0)
            {
                availableDates = scheduleService.FindAlternativeDates(FirstDate, LastDate, Convert.ToInt32(DaysNumber), Accommodation.Id);
            }
            else
            {
                availableDates = scheduleService.FindDates(FirstDate, LastDate, Convert.ToInt32(DaysNumber), Accommodation.Id);
            }

            UpdateDatesTable(availableDates);
        }

        public bool CanConfirm()
        {
            return SelectedDates != null;
        }

        public void OnConfirm()
        {
            reservationService.CreateReservation(Accommodation.Id, Guest.Id, SelectedDates.CheckIn, SelectedDates.CheckOut, Convert.ToInt32(GuestNumber));
        }
        public void UpdateDatesTable(List<DateRanges> availableDates)
        {
            AvailableDates.Clear();
            foreach (DateRanges dateRange in availableDates)
            {
                AvailableDates.Add(dateRange);
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
                if (columnName == "FirstDate")
                {
                    if (string.IsNullOrEmpty(FirstDate))
                        return "Unesite pocetni datum!";

                    DateOnly firstDate;
                    if (!DateOnly.TryParse(FirstDate, out firstDate))
                        return "Datum je u formatu: dd.MM.yyyy";

                    if (!string.IsNullOrEmpty(LastDate))
                    {
                        if (firstDate > DateOnly.Parse(LastDate))
                            return "Unesite datum manji od krajnjeg!";
                    }

                    if (firstDate < DateOnly.FromDateTime(DateTime.Now))
                        return "Unesite datum u buducnosti";
                }
                else if (columnName == "LastDate")
                {
                    if (string.IsNullOrEmpty(LastDate))
                        return "Unesite krajnji datum!";

                    DateOnly lastDate;
                    if (!DateOnly.TryParse(LastDate, out lastDate))
                        return "Datum je u formatu: dd.MM.yyyy";

                    if (string.IsNullOrEmpty(FirstDate))
                    {
                        if (lastDate < DateOnly.FromDateTime(DateTime.Now))
                            return "Unesite datum u buducnosti";
                    }
                    else
                    {
                        if (lastDate < DateOnly.Parse(FirstDate))
                            return "Unesite datum veci od pocetnog!";
                    }
                }
                else if (columnName == "DaysNumber")
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

        private readonly string[] _validatedProperties = { "FirstDate", "LastDate", "GuestNumber", "DaysNumber" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
    }
}
