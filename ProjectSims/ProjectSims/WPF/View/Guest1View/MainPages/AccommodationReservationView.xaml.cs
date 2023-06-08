using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
using ProjectSims.Validation;
using ProjectSims.WPF.View.Guest1View;
using ProjectSims.WPF.View.Guest1View.RatingPages;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for AccommodationReservation.xaml
    /// </summary>
    public partial class AccommodationReservationView : Page, INotifyPropertyChanged, IDataErrorInfo
    {
        public Accommodation Accommodation { get; set; }

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

        private AccommodationReservationService reservationService;
        private bool isDark;


        public Guest1 Guest { get; set; }
        public AccommodationReservationView(Accommodation SelectedAccommodation, Guest1 guest, bool isDark)
        {
            InitializeComponent();
            DataContext = this;

            Guest = guest;
            reservationService = new AccommodationReservationService();


            Accommodation = SelectedAccommodation;
            Username = guest.User.Username;
            AvailableDates = new ObservableCollection<DateRanges>();
            LoadImages(SelectedAccommodation.Images);

            BackButton.Focus();

            this.isDark = isDark;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;

            if (isDark)
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                isDark = false;
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                isDark = true;
            }
        }

        private void LoadImages(List<string> pathList)
        {
            foreach(string path in pathList)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();

                Image image = new Image();
                image.Source = bitmapImage;
                image.Height = 150;
                image.Width = 220;
                ImageList.Items.Add(image);
            }
        }

        private void FirstDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LastDatePicker.DisplayDateStart = FirstDatePicker.SelectedDate;                   
        }

        private void LastDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FirstDatePicker.DisplayDateEnd = LastDatePicker.SelectedDate;
        }

        private void FindDates_Click(object sender, RoutedEventArgs e)
        {
            AccommodationScheduleService scheduleService = new AccommodationScheduleService();
            if (IsValid)
            {
               // FirstDate = DateOnly.FromDateTime((DateTime)FirstDatePicker.SelectedDate);
                //LastDate = DateOnly.FromDateTime((DateTime)LastDatePicker.SelectedDate);

                List<DateRanges> availableDates = new List<DateRanges>();


                if (scheduleService.FindDates(DateOnly.Parse(FirstDate), DateOnly.Parse(LastDate), Convert.ToInt32(DaysNumber), Accommodation.Id).Count == 0)
                {
                    availableDates = scheduleService.FindAlternativeDates(DateOnly.Parse(FirstDate), DateOnly.Parse(LastDate), Convert.ToInt32(DaysNumber), Accommodation.Id);
                }
                else
                {
                    availableDates = scheduleService.FindDates(DateOnly.Parse(FirstDate), DateOnly.Parse(LastDate), Convert.ToInt32(DaysNumber), Accommodation.Id);
                }
                

                UpdateDatesTable(availableDates);
            }
        }


        public void UpdateDatesTable(List<DateRanges> availableDates)
        {
            AvailableDates.Clear();
            foreach (DateRanges dateRange in availableDates)
            {
                AvailableDates.Add(dateRange);
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            SelectedDates = (DateRanges)DatesTable.SelectedItem;
            if (SelectedDates != null)
            {
                DateRanges dates = (DateRanges)DatesTable.SelectedItem;

                reservationService.CreateReservation(Accommodation.Id, Guest.Id, dates.CheckIn, dates.CheckOut, Convert.ToInt32(GuestNumber));
                NavigationService.GoBack();
            }
        }

        private void Dates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDates = (DateRanges)DatesTable.SelectedItem;
        }

        private void Reserve(object sender, KeyEventArgs e)
        {
            if (SelectedDates != null)
            {
                if ((e.Key.Equals(Key.Enter)) || (e.Key.Equals(Key.Return)))
                {
                    DateRanges dates = (DateRanges)DatesTable.SelectedItem;

                    reservationService.CreateReservation(Accommodation.Id, Guest.Id, dates.CheckIn, dates.CheckOut, Convert.ToInt32(GuestNumber));
                    NavigationService.GoBack();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public String Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "FirstDate")
                {
                    if (string.IsNullOrEmpty(FirstDate))
                        return "Unesite pocetni datum!";

                    DateOnly firstDate;
                    if (!DateOnly.TryParse(FirstDate, out firstDate))
                        return "Datum je u formatu: dd.MM.yyyy";

                    if (!string.IsNullOrEmpty(LastDate))
                    {
                        if (firstDate > DateOnly.Parse(LastDate))
                            return "Unesite datum manji od krajnjeg!";
                    }

                    if (firstDate < DateOnly.FromDateTime(DateTime.Now))
                        return "Unesite datum u buducnosti";
                }
                else if (columnName == "LastDate")
                {
                    if (string.IsNullOrEmpty(LastDate))
                        return "Unesite krajnji datum!";

                    DateOnly lastDate;
                    if (!DateOnly.TryParse(LastDate, out lastDate))
                        return "Datum je u formatu: dd.MM.yyyy";

                    if (string.IsNullOrEmpty(FirstDate))
                    {
                        if (lastDate < DateOnly.FromDateTime(DateTime.Now))
                            return "Unesite datum u buducnosti";
                    }
                    else
                    {
                        if (lastDate < DateOnly.Parse(FirstDate))
                            return "Unesite datum veci od pocetnog!";
                    }                
                }
                else if (columnName == "DaysNumber")
                {
                    if (string.IsNullOrEmpty(DaysNumber))
                        return "Unesite broj dana!";

                    int daysNumber;
                    if (!int.TryParse(DaysNumber, out daysNumber))
                        return "Unesite ceo broj!";

                    if (daysNumber < Accommodation.MinimumReservationDays)
                        return "Mora biti veci od min broja dana!";

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

                    if (guestNumber > Accommodation.GuestsMaximum)
                        return "Mora biti manji od maks broja gostiju!";

                    if (guestNumber <= 0)
                        return "Unesite broj veci od 0!";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "FirstDate", "LastDate", "GuestNumber", "DaysNumber" };
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
