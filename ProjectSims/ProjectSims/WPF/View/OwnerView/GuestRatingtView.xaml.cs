using Microsoft.VisualBasic;
using ProjectSims.Service;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

namespace ProjectSims.View.OwnerView
{
    /// <summary>
    /// Interaction logic for GuestRatingtView.xaml
    /// </summary>
    public partial class GuestRatingtView : Window, INotifyPropertyChanged, IObserver
    {
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
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
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

        private DateOnly _checkInDate;
        public DateOnly CheckInDate
        {
            get => _checkInDate;
            set
            {
                if (value != _checkInDate)
                {
                    _checkInDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _checkOutDate;
        public DateOnly CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                if (value != _checkOutDate)
                {
                    _checkOutDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _rated;
        public bool Rated
        {
            get => _rated;
            set
            {
                if (value != _rated)
                {
                    _rated = value;
                    OnPropertyChanged();
                }
            }
        }
        public GuestAccommodation SelectedGuestAccommodation { get; set; }

        public  GuestAccommodationService _guestAccommodationController;
        public ObservableCollection<GuestAccommodation> GuestAccommodations { get; set; }

        public GuestRatingtView(GuestAccommodation selectedGuestAccommodation, GuestAccommodationService guestAccommodationController)
        {
            InitializeComponent();
            DataContext = this;

            FirstName = selectedGuestAccommodation.FirstName;
            LastName = selectedGuestAccommodation.LastName;
            AccommodationName = selectedGuestAccommodation.Name;
            Type = selectedGuestAccommodation.Type;
            CheckInDate = selectedGuestAccommodation.CheckInDate;
            CheckInDateTextBox.Text = selectedGuestAccommodation.CheckInDate.ToString();
            CheckOutDate = selectedGuestAccommodation.CheckOutDate;
            CheckOutDateTextBox.Text = selectedGuestAccommodation.CheckOutDate.ToString();

            _guestAccommodationController = guestAccommodationController;
            _guestAccommodationController.Subscribe(this);
            SelectedGuestAccommodation = selectedGuestAccommodation;
        
            Rated = selectedGuestAccommodation.Rated;

            GuestAccommodations = new ObservableCollection<GuestAccommodation>(_guestAccommodationController.GetAllGuestAccommodations());
        
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            GuestAccommodations.Clear();
            foreach (GuestAccommodation guestAccommodation in _guestAccommodationController.GetAllGuestAccommodations())
            {
                GuestAccommodations.Add(guestAccommodation);
            }
        }

        private void RateGuest_Click(object sender, RoutedEventArgs e)
        {
            SelectedGuestAccommodation.Rated = true;
            UpdateLayout();
            this.Close();
        }

        private void CancelRateGuest_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
