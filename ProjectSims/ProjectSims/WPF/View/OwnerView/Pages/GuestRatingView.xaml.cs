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
using System.Text.RegularExpressions;
using ProjectSims.WPF.View.OwnerView;
using ProjectSims.WPF.View.OwnerView.Pages;

namespace ProjectSims.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for GuestRatingtView.xaml
    /// </summary>
    public partial class GuestRatingView : Page, INotifyPropertyChanged, IObserver
    {
        public GuestAccommodation SelectedGuestAccommodation { get; set; }

        public GuestAccommodationService _guestAccommodationService;
        public ObservableCollection<GuestAccommodation> GuestAccommodations { get; set; }

        public AccommodationReservationService _accommodationReservationService;

        public GuestRatingService _guestRatingService;
        private Owner owner;
        public GuestRatingView(GuestAccommodation selectedGuestAccommodation, GuestAccommodationService guestAccommodationService, Owner o)
        {
            InitializeComponent();
            DataContext = this;
            owner = o;

            FirstName = selectedGuestAccommodation.FirstName;
            LastName = selectedGuestAccommodation.LastName;
            AccommodationName = selectedGuestAccommodation.Name;
            Type = selectedGuestAccommodation.Type;
            CheckInDate = selectedGuestAccommodation.CheckInDate;
            CheckInDateTextBox.Text = selectedGuestAccommodation.CheckInDate.ToString();
            CheckOutDate = selectedGuestAccommodation.CheckOutDate;
            CheckOutDateTextBox.Text = selectedGuestAccommodation.CheckOutDate.ToString();
            Rated = selectedGuestAccommodation.Rated;
            SelectedGuestAccommodation = selectedGuestAccommodation;
 
            _guestAccommodationService = guestAccommodationService;
            _guestAccommodationService.Subscribe(this);
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationReservationService.Subscribe(this);
            _guestRatingService = new GuestRatingService();
            _guestRatingService.Subscribe(this);
;
            GuestAccommodations = new ObservableCollection<GuestAccommodation>(_guestAccommodationService.GetAllGuestAccommodations());
        }

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

        private int _cleanlinessRate;
        public int CleanlinessRate
        {
            get => _cleanlinessRate;
            set
            {
                if (value != _cleanlinessRate)
                {
                    _cleanlinessRate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _respectingRulesRate;
        public int RespectingRulesRate
        {
            get => _respectingRulesRate;
            set
            {
                if (value != _respectingRulesRate)
                {
                    _respectingRulesRate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _tidinessRate;
        public int TidinessRate
        {
            get => _tidinessRate;
            set
            {
                if (value != _tidinessRate)
                {
                    _tidinessRate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _communicationRate;
        public int CommunicationRate
        {
            get => _communicationRate;
            set
            {
                if (value != _communicationRate)
                {
                    _communicationRate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment = "Dodatni komentar...";
        public string Comment 
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            GuestAccommodations.Clear();
            foreach (GuestAccommodation guestAccommodation in _guestAccommodationService.GetAllGuestAccommodations())
            {
                GuestAccommodations.Add(guestAccommodation);
            }
        }

        private void RateGuest_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (IsValid && !string.IsNullOrWhiteSpace(CommentTextBox.Text))
            {
                if (clickedButton == RateButton)
                {
                    SelectedGuestAccommodation.Rated = true;
                    _guestAccommodationService.Update(SelectedGuestAccommodation);
                    AccommodationReservation accommodationReservation = _accommodationReservationService.GetReservation(SelectedGuestAccommodation.GuestId, SelectedGuestAccommodation.AccommodationId, SelectedGuestAccommodation.CheckInDate, SelectedGuestAccommodation.CheckOutDate);
                    _guestRatingService.Create(new GuestRating(-1, CleanlinessRate, RespectingRulesRate, TidinessRate, CommunicationRate, CommentTextBox.Text, accommodationReservation, DateOnly.FromDateTime(DateTime.Now), SelectedGuestAccommodation.GuestId));
                }
                OwnerStartingView ownerStartingView = (OwnerStartingView)Window.GetWindow(this);
                ownerStartingView.SelectedTab.Content = new AccommodationsDisplay(owner);
            }
            else if (clickedButton == CancelButton)
            {
                //Didn't rate guest
            }
            else if (clickedButton == null)
            {
                return;
            }
        }

        private void CancelRateGuest_Click(object sender, RoutedEventArgs e)
        {
            OwnerStartingView ownerStartingView = (OwnerStartingView)Window.GetWindow(this);
            ownerStartingView.SelectedTab.Content = new AccommodationsDisplay(owner);
        }

        private void NotifyIfGuestUnrated() 
        {
               
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;

                if (columnName == "CleanlinessRate")
                {
                    if (CleanlinessComboBox.SelectedIndex == -1)
                    {
                        return "Izaberite vrijednost!";
                    }
                }
                else if (columnName == "RespectingRulesRate")
                {
                    if (RespectingRulesComboBox.SelectedIndex == -1)
                    {
                        return "Izaberite vrijednost!";
                    }
                }
                else if (columnName == "TidinessRate")
                {
                    if (TidinessComboBox.SelectedIndex == -1)
                    {
                        return "Izaberite vrijednost!";
                    }
                }
                else if (columnName == "CommunicationRate")
                {
                    if (CommunicationComboBox.SelectedIndex == -1)
                    {
                        return "Izaberite vrijednost!";
                    }
                }
                else if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment) || CommentTextBox.Text == "Dodatni komentar...")
                    {
                        return "Unesite komentar!";
                    }
                }
                return null;
            }
        }

        private readonly string[] validatedProperties = { "CleanlinessRate", "RespectingRulesRate", "TidinessRate", "CommunicationRate", "Comment" };

        public bool IsValid
        {
            get
            {
                foreach (var property in validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }     
        }

        private void CommentTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            if (source != null)
            {
                source.Background = Brushes.MintCream;
                source.Foreground = Brushes.Black;
                source.Clear();
            }
        }

        private void CommentTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            if (source != null)
            {
                source.Background = Brushes.White;
                source.Foreground = Brushes.Black;
            }
        }


    }
}
