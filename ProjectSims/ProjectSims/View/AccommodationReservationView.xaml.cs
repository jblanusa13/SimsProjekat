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
    public partial class AccommodationReservationView : Window, INotifyPropertyChanged, IObserver
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

        private AccommodationReservationController _reservationController;

        public AccommodationReservationView(Accommodation SelectedAccommodation)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationId = SelectedAccommodation.Id;
            AccommodationName = SelectedAccommodation.Name;
            Location = SelectedAccommodation.Location;
            Type = SelectedAccommodation.Type.ToString();
            MaxGuests = SelectedAccommodation.GuestMaximum.ToString();
            MinDays = SelectedAccommodation.MinimumReservationDays.ToString();

            _reservationController = new AccommodationReservationController();
            _reservationController.Subscribe(this);

           // AvailableDates = new ObservableCollection<DateRanges>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FindDates_Click(object sender, RoutedEventArgs e)
        {
            TexboxDaysNumber.Text = "0";
            if (!string.IsNullOrEmpty(TexboxFirstDate.Text) && !string.IsNullOrEmpty(TexboxLastDate.Text) && !string.IsNullOrEmpty(TexboxDaysNumber.Text))
            {
                // TexboxDaysNumber.Text = "0";
                //List<DateRanges> availableDates = new List<DateRanges>();
                //availableDates = _reservationController.FindAvailableDates(DateOnly.Parse(FirstDate), DateOnly.Parse(LastDate), Convert.ToInt32(DaysNumber), _accommodationId);
                AvailableDates = new ObservableCollection<DateRanges>(_reservationController.FindAvailableDates(DateOnly.Parse(FirstDate), DateOnly.Parse(LastDate), Convert.ToInt32(DaysNumber), _accommodationId));
                //foreach(DateRanges dateRange in availableDates)
                //{
                 //  AvailableDates.Add(dateRange);
                //}
            }
        }

        public void Update()
        {
            
        }
    }
}
