using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.View.OwnerView;
using ProjectSims.WPF.View.OwnerView.Pages;
using Syncfusion.Windows.Tools;
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
using System.Windows.Media;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class GuestRatingViewModel : IObserver, INotifyPropertyChanged
    {
        Owner Owner { get; set; }
        public AccommodationReservation SelectedAccommodationReservation { get; set; }
        public AccommodationReservationService accommodationReservationService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public RelayCommand RateGuestCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand GotFocusCommand { get; set; }
        public RelayCommand LostFocusCommand { get; set; }
        public GuestRatingService guestRatingService;

        public DateRangesService dateRangesService;
        private NavigationService NavService { get; set; }
        private OwnerStartingView Window { get; set; }
        private GuestRatingView View { get; set; }

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

        private string _cleanlinessRate;
        public string CleanlinessRate
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

        private string _respectingRulesRate;
        public string RespectingRulesRate
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

        private string _tidinessRate;
        public string TidinessRate
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

        private string _communicationRate;
        public string CommunicationRate
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

        private string _comment="Dodatni komentar...";
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

        public GuestRatingViewModel(AccommodationReservation selectedAccommodationReservation, GuestRatingView view, OwnerStartingView window, Owner o, NavigationService navService) 
        {
            Owner = o;
            Window = window;
            View = view;
            SelectedAccommodationReservation = selectedAccommodationReservation;
            NavService = navService;
            SelectedAccommodationReservation = selectedAccommodationReservation;
            accommodationReservationService = new AccommodationReservationService();
            accommodationReservationService.Subscribe(this);
            guestRatingService = new GuestRatingService();
            guestRatingService.Subscribe(this);
            dateRangesService = new DateRangesService();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(accommodationReservationService.GetAllReservations());
            RateGuestCommand = new RelayCommand(Execute_RateCommand, CanExecute_RateCommand);
            CancelCommand = new RelayCommand(Execute_CancelCommand);
            GotFocusCommand = new RelayCommand(Execute_PreviewGotFocusCommand);
            LostFocusCommand = new RelayCommand(Execute_LostFocusCommand);
        }

        private void Execute_PreviewGotFocusCommand(object obj)
        {
            View.CommentTextBox.Foreground = Brushes.Black;
            if (Comment == "Dodatni komentar...")
            {
                View.CommentTextBox.Clear();
            }
        }

        private void Execute_LostFocusCommand(object obj)
        {
            View.CommentTextBox.Background = Brushes.White;
            View.CommentTextBox.Foreground = Brushes.Gray;
        }

        private void Execute_RateCommand(object obj)
        {
            RateGuest(SelectedAccommodationReservation, Convert.ToInt32(CleanlinessRate), Convert.ToInt32(RespectingRulesRate), Convert.ToInt32(TidinessRate), Convert.ToInt32(CommunicationRate), ReadComment());
            NavService.Navigate(new AccommodationsDisplayView(Owner, NavService, Window));
            Window.PageTitle = "Smještaji";
        }

        private bool CanExecute_RateCommand(object obj)
        {
            return View.CleanlinessComboBox.SelectedIndex > -1 && View.RespectingRulesComboBox.SelectedIndex > -1 
                    && View.TidinessComboBox.SelectedIndex > -1 && View.CommunicationComboBox.SelectedIndex > -1;
        }

        private void Execute_CancelCommand(object obj)
        {
            NavService.Navigate(new NotificationsView(Owner, Window, NavService));
            Window.PageTitle = "Obavještenja";
        }

        public void RateGuest(AccommodationReservation SelectedAccommodationReservation, int cleanlinessRate, int respectingRulesRate, int tidinessRate, int communicationRate, string comment)
        {
            SelectedAccommodationReservation.RatedGuest = true;
            accommodationReservationService.Update(SelectedAccommodationReservation);
            guestRatingService.Create(new GuestRating(-1, cleanlinessRate, respectingRulesRate, tidinessRate, communicationRate, comment, SelectedAccommodationReservation.Id, SelectedAccommodationReservation, DateOnly.FromDateTime(DateTime.Now), SelectedAccommodationReservation.GuestId));
        }

        private string ReadComment()
        {
            if (Comment == "Dodatni komentar...")
            {
                return "";
            }
            return Comment;
        }

        public void Update()
        {
            AccommodationReservations.Clear();
            foreach (AccommodationReservation guestAccommodation in accommodationReservationService.GetAllReservations())
            {
                AccommodationReservations.Add(guestAccommodation);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
