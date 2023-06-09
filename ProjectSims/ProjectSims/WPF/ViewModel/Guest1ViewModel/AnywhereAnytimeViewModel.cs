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
using System.Windows.Input;
using System.Windows.Navigation;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class AnywhereAnytimeViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
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
                    SearchCommand.RaiseCanExecuteChanged();
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
                    SearchCommand.RaiseCanExecuteChanged();
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
                    SearchCommand.RaiseCanExecuteChanged();
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
                    SearchCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private AccommodationScheduleService scheduleService;

        public ObservableCollection<Accommodation> AvailableAccommodations { get; set; }
        public ObservableCollection<DateRanges> AvailableDates { get; set; }
        private Accommodation selectedAccommodation;
        public Accommodation SelectedAccommodation 
        {
            get => selectedAccommodation;
            set
            {
                if (value != selectedAccommodation)
                {
                    selectedAccommodation = value;
                    OnPropertyChanged();
                    AvailableDates.Clear();
                }
            }          
        }

        private DateRanges selectedDate;
        public DateRanges SelectedDate
        {
            get => selectedDate;
            set
            {
                if (value != selectedDate)
                {
                    selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public MyICommand SearchCommand { get; set; }
        public MyICommand ReserveCommand { get; set; }
        public MyICommand ThemeCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        public DatePicker First { get; set; }
        public DatePicker Last { get; set; }
        public DataGrid DatesTable { get; set; }
        private AccommodationReservationService reservationService;

        private Guest1 guest;

        public NavigationService NavService { get; set; }
        public AnywhereAnytimeViewModel(DatePicker first, DatePicker last, DataGrid datesTable, Guest1 guest, NavigationService navigation)
        {
            SearchCommand = new MyICommand(OnSearch, CanSearch);
            ReserveCommand = new MyICommand(OnReserve);
            ThemeCommand = new MyICommand(OnTheme);
            CancelCommand = new MyICommand(OnCancel);
            AvailableAccommodations = new ObservableCollection<Accommodation>();
            AvailableDates = new ObservableCollection<DateRanges>();
            scheduleService = new AccommodationScheduleService();
            First = first;
            Last = last;
            DatesTable = datesTable;

            reservationService = new AccommodationReservationService();
            NavService = navigation;

            this.guest = guest;
        }

        public void ShowDates()
        {
            if(SelectedAccommodation != null)
            {
                List<DateRanges> availableDates = new List<DateRanges>();
                availableDates = scheduleService.FindDates(FirstDate, LastDate, Convert.ToInt32(DaysNumber), SelectedAccommodation.Id);
                UpdateDatesTable(availableDates);
                DatesTable.Focus();
            }
        }
        public void OnCancel()
        {
            NavService.GoBack();
        }
        public void OnTheme()
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
        public void OnReserve()
        {
            Reserve();
        }

        public void Reserve()
        {
            reservationService.CreateReservation(SelectedAccommodation.Id, guest.Id, SelectedDate.CheckIn, SelectedDate.CheckOut, Convert.ToInt32(GuestNumber));
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
        private bool CanSearch()
        {
            bool firstDateSelected = First.SelectedDate == null;
            bool lastDateSelected = Last.SelectedDate == null;

            return IsValid && ((firstDateSelected && !lastDateSelected || !firstDateSelected && lastDateSelected) ? false : true);
        }

        private void OnSearch()
        {
            List<Accommodation> availableAccommodations = new List<Accommodation>();

            
            if (First.SelectedDate == null && Last.SelectedDate == null)
            {
                FirstDate = DateOnly.FromDateTime(DateTime.Today);
                LastDate = DateOnly.FromDateTime(DateTime.Today).AddDays(365);
                
            }
            else
            {
                FirstDate = DateOnly.FromDateTime((DateTime)First.SelectedDate);
                LastDate = DateOnly.FromDateTime((DateTime)Last.SelectedDate);    
            }
            availableAccommodations = scheduleService.FindAvailableAccommodations(FirstDate, LastDate, Convert.ToInt32(DaysNumber), Convert.ToInt32(GuestNumber));

            AvailableDates.Clear();
            UpdateAccommodationsTable(availableAccommodations);
        }

        public void UpdateAccommodationsTable(List<Accommodation> availableAccommodations)
        {
            AvailableAccommodations.Clear();
            foreach (Accommodation accommodation in availableAccommodations)
            {
                AvailableAccommodations.Add(accommodation);
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
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
    }
}
