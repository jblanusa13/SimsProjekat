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
using ProjectSims.WPF.ViewModel.Guest1ViewModel;
using ProjectSims.WPF.ViewModel.OwnerViewModel;

namespace ProjectSims.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for GuestRatingtView.xaml
    /// </summary>
    public partial class GuestRatingView : Page, INotifyPropertyChanged
    {
        public GuestRatingViewModel guestRatingViewModel { get; set; }
        public AccommodationReservation SelectedAccommodationReservation { get; set; }
        private Owner Owner { get; set; }

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

        private string _comment;
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
        public GuestRatingView(AccommodationReservation selectedAccommodationReservation, Owner o)
        {
            InitializeComponent();
            Owner = o;
            SelectedAccommodationReservation = selectedAccommodationReservation;
            guestRatingViewModel = new GuestRatingViewModel(SelectedAccommodationReservation, Owner);
            this.DataContext = guestRatingViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RateGuest_Click(object sender, RoutedEventArgs e)
        {
            if (CleanlinessComboBox.SelectedIndex > -1 && RespectingRulesComboBox.SelectedIndex > -1 && TidinessComboBox.SelectedIndex > -1 && CommunicationComboBox.SelectedIndex > -1) 
            {
                guestRatingViewModel.RateGuest(SelectedAccommodationReservation, Convert.ToInt32(CleanlinessComboBox.Text), Convert.ToInt32(RespectingRulesComboBox.Text), Convert.ToInt32(TidinessComboBox.Text), Convert.ToInt32(CommunicationComboBox.Text), ReadComment());
                this.NavigationService.Navigate(new AccommodationsDisplay(Owner));
            }
        }

        private string ReadComment() 
        {
            if (CommentTextBox.Text == "Dodatni komentar...")
            {
                return "";
            }
            return CommentTextBox.Text;
        }

        private void CancelRateGuest_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AccommodationsDisplay(Owner));
        }

        private void CommentTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            if (source != null)
            {
                source.Background = Brushes.MintCream;
                source.Foreground = Brushes.Black;
                if (source.Text == "Dodatni komentar...") 
                {
                    source.Clear();
                }
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