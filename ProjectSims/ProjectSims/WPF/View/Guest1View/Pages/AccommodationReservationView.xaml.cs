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

namespace ProjectSims.WPF.View.Guest1View.Pages
{
    /// <summary>
    /// Interaction logic for AccommodationReservation.xaml
    /// </summary>
    public partial class AccommodationReservationView : Page, INotifyPropertyChanged, IObserver
    {
        private int accommodationId;

        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private AccommodationType _type;
        public AccommodationType Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _minDays;
        public int MinDays
        {
            get => _minDays;
            set
            {
                if (value != _minDays)
                {
                    _minDays = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private Guest1 guest;
        public AccommodationReservationView(Accommodation SelectedAccommodation, Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;

            this.guest = guest;

            reservationService = new AccommodationReservationService();
            reservationService.Subscribe(this);

            accommodationId = SelectedAccommodation.Id;
            AccommodationName = SelectedAccommodation.Name;
            City = SelectedAccommodation.Location.City;
            Country = SelectedAccommodation.Location.Country;
            Type = SelectedAccommodation.Type;
            MaxGuests = SelectedAccommodation.GuestsMaximum;
            MinDays = SelectedAccommodation.MinimumReservationDays;

            Username = guest.User.Username;

            AvailableDates = new ObservableCollection<DateRanges>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FindDates_Click(object sender, RoutedEventArgs e)
        {
            DateRangesService dateRangesService = new DateRangesService();
            FirstDate = DateOnly.FromDateTime((DateTime)FirstDatePicker.SelectedDate);
            LastDate = DateOnly.FromDateTime((DateTime)LastDatePicker.SelectedDate);

            if (FirstDate != null && LastDate != null && !string.IsNullOrEmpty(TextboxDaysNumber.Text))
            {
                List<DateRanges> availableDates = new List<DateRanges>();
                availableDates = dateRangesService.FindAvailableDates(FirstDate, LastDate, DaysNumber, accommodationId);

                AvailableDates.Clear();
                foreach (DateRanges dateRange in availableDates)
                {
                    AvailableDates.Add(dateRange);
                }
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            SelectedDates = (DateRanges)DatesTable.SelectedItem;
            if (SelectedDates != null)
            {
                DateRanges dates = (DateRanges)DatesTable.SelectedItem;
                int guestNumber = Convert.ToInt32(GuestNumber);

                reservationService.CreateReservation(accommodationId, guest.Id, dates.CheckIn, dates.CheckOut, guestNumber);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
