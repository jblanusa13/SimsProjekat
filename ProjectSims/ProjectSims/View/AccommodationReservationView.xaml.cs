using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectSims.Controller;
using ProjectSims.Model;
using ProjectSims.Observer;

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationView.xaml
    /// </summary>
    public partial class AccommodationReservationView : Window, INotifyPropertyChanged, IObserver, IDataErrorInfo
    {
        private int _accommodationId;

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
        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _type;
        public string Type
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

        private string _maxGuests;
        public string MaxGuests
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

        private string _minDays;
        public string MinDays
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

        private AccommodationReservationController _reservationController;

        private Guest1 _guest;

        public AccommodationReservationView(Accommodation SelectedAccommodation, Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;

            _guest = guest;

            _reservationController = new AccommodationReservationController();
            _reservationController.Subscribe(this);

            _accommodationId = SelectedAccommodation.Id;
            AccommodationName = SelectedAccommodation.Name;
            Location = SelectedAccommodation.Location;
            Type = SelectedAccommodation.Type.ToString();
            MaxGuests = SelectedAccommodation.GuestsMaximum.ToString();
            MinDays = SelectedAccommodation.MinimumReservationDays.ToString();

            User user = _reservationController.GetUser(_guest.UserId);
            Username = user.Username;

            AvailableDates = new ObservableCollection<DateRanges>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FindDates_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(TexboxFirstDate.Text) && !string.IsNullOrEmpty(TexboxLastDate.Text) && !string.IsNullOrEmpty(TexboxDaysNumber.Text))
            {
                List<DateRanges> availableDates = new List<DateRanges>();
                availableDates = _reservationController.FindAvailableDates(DateOnly.Parse(TexboxFirstDate.Text), DateOnly.Parse(TexboxLastDate.Text), Convert.ToInt32(TexboxDaysNumber.Text), _accommodationId);

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
            if (IsValid && SelectedDates != null)
            {
                DateRanges dates = (DateRanges)DatesTable.SelectedItem;
                int guestNumber = Convert.ToInt32(GuestNumber);

                _reservationController.CreateReservation(_accommodationId, _guest.Id, dates.CheckIn, dates.CheckOut, guestNumber);
                Close();
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        // validation
        private Regex _DateRegex = new Regex("(0?[1-9]|[12][0-9]|3[01])\\.(0?[1-9]|1[012])\\.[1-2][0-9]{3}\\.?$");
        private Regex _NumberRegex = new Regex("[1-9]+");

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if(columnName == "FirstDate")
                {
                    if (string.IsNullOrEmpty(FirstDate))
                        return "Obavezno polje";

                    Match match = _DateRegex.Match(FirstDate);
                    if (!match.Success)
                        return "Datum je u formatu: DD.MM.YYYY";
                }
                else if (columnName == "LastDate")
                {
                    if (string.IsNullOrEmpty(LastDate))
                        return "Obavezno polje";

                    Match match = _DateRegex.Match(LastDate);
                    if (!match.Success)
                        return "Datum je u formatu: DD.MM.YYYY";


                    if (DateOnly.Parse(LastDate) <= DateOnly.Parse(FirstDate))
                    {
                        return "Mora biti veci od pocetnog datuma!";
                    }

                }
                else if(columnName == "DaysNumber")
                {
                    if (string.IsNullOrEmpty(DaysNumber))
                        return "Obavezno polje";

                    Match match = _NumberRegex.Match(DaysNumber);
                    if (!match.Success)
                        return "Broj dana je prirodan broj";

                    if(Convert.ToInt32(DaysNumber) < Convert.ToInt32(MinDays))
                    {
                        return "Mora biti veci od min dozvoljenog broja dana";
                    }
                }
                else if(columnName == "GuestNumber")
                {
                    if (string.IsNullOrEmpty(GuestNumber))
                        return "Obavezno polje";

                    Match match = _NumberRegex.Match(GuestNumber);
                    if (!match.Success)
                        return "Broj gostiju je prirodan broj";

                    if(Convert.ToInt32(GuestNumber) > Convert.ToInt32(MaxGuests))
                    {
                        return "Mora biti manji od maks dozvoljenog broja gostiju";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validiranaObelezja = { "FirstDate" , "LastDate", "DaysNumber", "GuestNumber" };


        public bool IsValid
        {
            get
            {
                foreach (var obelezje in _validiranaObelezja)
                {
                    if (this[obelezje] != null)
                        return false;
                }
                return true;
            }
        }
    }
}
