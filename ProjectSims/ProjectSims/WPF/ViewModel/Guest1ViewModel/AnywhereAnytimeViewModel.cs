using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        private string _firstDate;
        public string FirstDate
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

        private string _lastDate;
        public string LastDate
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
        public Accommodation SelectedAccommodation { get; set; }

        public MyICommand SearchCommand { get; set; }
        public MyICommand ShowDatesCommand { get; set; }
        public AnywhereAnytimeViewModel()
        {
            SearchCommand = new MyICommand(OnSearch, CanSearch);
            ShowDatesCommand = new MyICommand(OnShow, CanShow);
            AvailableAccommodations = new ObservableCollection<Accommodation>();
            AvailableDates = new ObservableCollection<DateRanges>();
            scheduleService = new AccommodationScheduleService();
        }

        private bool CanShow()
        {
            return SelectedAccommodation != null;
        }
        public void OnShow()
        {
            List<DateRanges> availableDates = new List<DateRanges>();
            availableDates = scheduleService.FindDates(DateOnly.Parse(FirstDate), DateOnly.Parse(LastDate), Convert.ToInt32(DaysNumber), SelectedAccommodation.Id);
            UpdateDatesTable(availableDates);
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
            bool firstDateSelected = string.IsNullOrEmpty(FirstDate);
            bool lastDateSelected = string.IsNullOrEmpty(LastDate);

            return IsValid && ((firstDateSelected && !lastDateSelected || !firstDateSelected && lastDateSelected) ? false : true);
        }

        private void OnSearch()
        {
            List<Accommodation> availableAccommodations = new List<Accommodation>();

            if(string.IsNullOrEmpty(FirstDate) && string.IsNullOrEmpty(LastDate))
            {
                availableAccommodations = scheduleService.FindAvailableAccommodations(DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today).AddDays(365), Convert.ToInt32(DaysNumber), Convert.ToInt32(GuestNumber));
            }
            else
            {
                availableAccommodations = scheduleService.FindAvailableAccommodations(DateOnly.Parse(FirstDate), DateOnly.Parse(LastDate), Convert.ToInt32(DaysNumber), Convert.ToInt32(GuestNumber));
            }

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
