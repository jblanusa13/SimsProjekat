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
using ProjectSims.Commands;
using ProjectSims.WPF.View.Guest1View.NotifAndHelp;
using ProjectSims.WPF.View.Guest1View.HelpPages;
using ProjectSims.WPF.View.Guest1View;

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

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ReserveCommand { get; set; }
        public RelayCommand ThemeCommand { get; set; }
        public RelayCommand NotifCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand LanguageCommand { get; set; }
        public RelayCommand HelpCommand { get; set; }
        public RelayCommand FirstChangedCommand { get; set; }
        public RelayCommand LastChangedCommand { get; set; }
        public AnytimeAnywhere AnytimeAnywhere { get; set; }
        private AccommodationReservationService reservationService;

        private Guest1 guest;

        public NavigationService NavService { get; set; }
        public AnywhereAnytimeViewModel(Guest1 guest, NavigationService navigation, AnytimeAnywhere anywhereAnytimeView)
        {
            SearchCommand = new RelayCommand(Execute_SearchCommand, CanExecute_SearchCommand);
            ReserveCommand = new RelayCommand(Execute_ReserveCommand, CanExecute_ReserveCommand);
            ThemeCommand = new RelayCommand(Execute_ThemeCommand);
            NotifCommand = new RelayCommand(Execute_NotifCommand);
            CancelCommand = new RelayCommand(Execute_CancelCommand);
            HelpCommand = new RelayCommand(Execute_HelpCommand);
            LanguageCommand = new RelayCommand(Execute_LanguageCommand);
            FirstChangedCommand = new RelayCommand(Execute_FirstChangedCommand);
            LastChangedCommand = new RelayCommand(Execute_LastChangedCommand);
            AvailableAccommodations = new ObservableCollection<Accommodation>();
            AvailableDates = new ObservableCollection<DateRanges>();
            scheduleService = new AccommodationScheduleService();
            this.AnytimeAnywhere = anywhereAnytimeView;

            reservationService = new AccommodationReservationService();
            NavService = navigation;

            this.guest = guest;
        }

        public void Execute_FirstChangedCommand(object obj)
        {
            AnytimeAnywhere.LastDatePicker.DisplayDateStart = AnytimeAnywhere.FirstDatePicker.SelectedDate;
        }
        public void Execute_LastChangedCommand(object obj)
        {
            AnytimeAnywhere.FirstDatePicker.DisplayDateEnd = AnytimeAnywhere.LastDatePicker.SelectedDate;
        }

        private void Execute_HelpCommand(object obj)
        {
            HelpStartView helpStart = new HelpStartView();
            helpStart.SelectedTab.Content = new AnywhereHelpView();
            helpStart.Show();
        }
        public void ShowDates()
        {
            if(SelectedAccommodation != null)
            {
                List<DateRanges> availableDates = new List<DateRanges>();
                availableDates = scheduleService.FindDates(FirstDate, LastDate, Convert.ToInt32(DaysNumber), SelectedAccommodation.Id);
                UpdateDatesTable(availableDates);
                AnytimeAnywhere.DatesTable.Focus();
            }
        }
        public void Execute_CancelCommand(object obj)
        {
            NavService.GoBack();
        }
        public void Execute_ThemeCommand(object obj)
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

        private void Execute_LanguageCommand(object obj)
        {
            App app = (App)Application.Current;

            if (App.CurrentLanguage == "sr-LATN")
            {
                app.ChangeLanguage("en-US");
                App.CurrentLanguage = "en-US";
            }
            else
            {
                app.ChangeLanguage("sr-LATN");
                App.CurrentLanguage = "sr-LATN";
            }

        }
        private void Execute_NotifCommand(object obj)
        {
            NotificationsView notificationsView = new NotificationsView(guest);
            notificationsView.Show();
        }

        public bool CanExecute_ReserveCommand(object obj)
        {
            return SelectedDate != null;
        }
        public void Execute_ReserveCommand(object obj)
        {
            reservationService.CreateReservation(SelectedAccommodation.Id, guest.Id, SelectedDate.CheckIn, SelectedDate.CheckOut, Convert.ToInt32(GuestNumber));
            MessageBox.Show("Uspesno ste izvrsili rezervaciju!", "Travel&Tour");
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
        private bool CanExecute_SearchCommand(object obj)
        {
            bool firstDateSelected = AnytimeAnywhere.FirstDatePicker.SelectedDate == null;
            bool lastDateSelected = AnytimeAnywhere.LastDatePicker.SelectedDate == null;

            return IsValid && ((firstDateSelected && !lastDateSelected || !firstDateSelected && lastDateSelected) ? false : true);
        }

        private void Execute_SearchCommand(object obj)
        {
            List<Accommodation> availableAccommodations = new List<Accommodation>();

            
            if (AnytimeAnywhere.FirstDatePicker.SelectedDate == null && AnytimeAnywhere.LastDatePicker.SelectedDate == null)
            {
                FirstDate = DateOnly.FromDateTime(DateTime.Today);
                LastDate = DateOnly.FromDateTime(DateTime.Today).AddDays(365);
                
            }
            else
            {
                FirstDate = DateOnly.FromDateTime((DateTime)AnytimeAnywhere.FirstDatePicker.SelectedDate);
                LastDate = DateOnly.FromDateTime((DateTime)AnytimeAnywhere.LastDatePicker.SelectedDate);    
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
