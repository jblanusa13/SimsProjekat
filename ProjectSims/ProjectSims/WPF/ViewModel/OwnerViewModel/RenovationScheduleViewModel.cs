using ceTe.DynamicPDF;
using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.View.OwnerView;
using ProjectSims.WPF.View.OwnerView.Pages;
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
    public class RenovationScheduleViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public Owner Owner { get; set; }
        public OwnerStartingView Window { get; set; }
        public RenovationScheduleView View { get; set; }
        public RelayCommand ConfirmCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand MouseDownCommand { get; set; }
        public RelayCommand LostKeyboardFocusCommand { get; set; }
        public RelayCommand DurationChangedCommand { get; set; }
        public RelayCommand FirstDateChangedCommand { get; set; }
        public RelayCommand FirstChangedCommand { get; set; }
        public RelayCommand LastChangedCommand { get; set; }
        public RelayCommand SecondDateChangedCommand { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<DateRanges> DateRanges { get; set; }
        public DateOnly FirstDate { get; set; }
        public DateOnly SecondDate { get; set; }
        public int Duration { get; set; }
        private RenovationScheduleService renovationService;
        private AccommodationScheduleService accommodationScheduleService;
        public NavigationService NavService { get; set; }

        private string _description="Unesite opis...";
        public string Description
        {
            get => _description;

            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public RenovationScheduleViewModel(OwnerStartingView window, RenovationScheduleView view, Accommodation selectedAccommodation, Owner owner, NavigationService navService)
        {
            Owner = owner;
            Window = window;
            View = view;
            SelectedAccommodation = selectedAccommodation;
            NavService = navService;
            renovationService = new RenovationScheduleService();
            accommodationScheduleService = new AccommodationScheduleService();
            DateRanges = new ObservableCollection<DateRanges>();
            CancelCommand = new RelayCommand(Execute_CancelCommand);
            ConfirmCommand = new RelayCommand(Execute_ConfirmCommand, CanExecute_ConfirmCommand);
            DurationChangedCommand = new RelayCommand(Execute_DurationCommand, CanExecute_DurationCommand);
            FirstDateChangedCommand = new RelayCommand(Execute_FirstDateChangedCommand, CanExecute_FirstDateChangedCommand);
            SecondDateChangedCommand = new RelayCommand(Execute_SecondDateChangedCommand, CanExecute_SecondDateChangedCommand);
            MouseDownCommand = new RelayCommand(Execute_MouseDownCommand);
            LostKeyboardFocusCommand = new RelayCommand(Execute_LostKeyboardFocus);
            FirstChangedCommand = new RelayCommand(Execute_FirstChangedCommand);
            LastChangedCommand = new RelayCommand(Execute_LastChangedCommand);
            ValidateFirst();
            ValidateLast();
        }

        public void Execute_FirstChangedCommand(object obj)
        {
            View.SecondDatePicker.DisplayDateStart = View.FirstDatePicker.SelectedDate;
            ValidateFirst();
        }
        public void Execute_LastChangedCommand(object obj)
        {
            View.FirstDatePicker.DisplayDateEnd = View.SecondDatePicker.SelectedDate;
            ValidateLast();
        }

        public void ValidateFirst()
        {
            if (View.FirstDatePicker.SelectedDate == null)
            {
                View.ValidateFirst.Content = "Unesite pocetni datum!";
            }
            else
            {
                View.ValidateFirst.Content = "";
            }
        }

        public void ValidateLast()
        {
            if (View.SecondDatePicker.SelectedDate == null)
            {
                View.ValidateLast.Content = "Unesite krajnji datum!";
            }
            else
            {
                View.ValidateLast.Content = "";
            }
        }

        private void Execute_LostKeyboardFocus(object obj)
        {
            View.DescriptionTextBox.Background = Brushes.White;
            View.DescriptionTextBox.Foreground = Brushes.Black;
            if (Description == "") 
            {
                Description = "Unesite opis...";
                View.DescriptionTextBox.Foreground = Brushes.Gray;
            }
        }

        private void Execute_MouseDownCommand(object obj)
        {
            View.DescriptionTextBox.Background = Brushes.MintCream;
            View.DescriptionTextBox.Foreground = Brushes.Black;
            if (Description == "Unesite opis...")
            {
                View.DescriptionTextBox.Clear();
            }
        }

        private void Execute_DurationCommand(object obj)
        {
            FindAvailableDates(DateOnly.FromDateTime((DateTime)View.FirstDatePicker.SelectedDate), DateOnly.FromDateTime((DateTime)View.SecondDatePicker.SelectedDate), Duration);      
        }

        private bool CanExecute_DurationCommand(object obj)
        {
            return View.SecondDatePicker.SelectedDate != null
                && View.FirstDatePicker.SelectedDate != null
                && !string.IsNullOrWhiteSpace(Duration.ToString());
        }

        private void Execute_SecondDateChangedCommand(object obj)
        {
            View.FirstDatePicker.DisplayDateEnd = View.SecondDatePicker.SelectedDate;
            ValidateLast();
            if(View.FirstDatePicker.SelectedDate != null && !string.IsNullOrEmpty(Duration.ToString()))
                FindAvailableDates(DateOnly.FromDateTime((DateTime)View.FirstDatePicker.SelectedDate), DateOnly.FromDateTime((DateTime)View.SecondDatePicker.SelectedDate), Duration);
        }

        private bool CanExecute_SecondDateChangedCommand(object obj)
        {
            return true;
        }
        private void Execute_FirstDateChangedCommand(object obj)
        {
            View.SecondDatePicker.DisplayDateStart = View.FirstDatePicker.SelectedDate;
            ValidateFirst();
            if(View.SecondDatePicker.SelectedDate != null && !string.IsNullOrEmpty(Duration.ToString()))
                FindAvailableDates(DateOnly.FromDateTime((DateTime)View.FirstDatePicker.SelectedDate), DateOnly.FromDateTime((DateTime)View.SecondDatePicker.SelectedDate), Duration);
        }

        private bool CanExecute_FirstDateChangedCommand(object obj)
        {
            return true;
        }

        private void Execute_CancelCommand(object obj)
        {
            NavService.Navigate(new AccommodationsDisplayView(Owner, NavService, Window));
            Window.PageTitle = "Smještaji";
        }

        private void Execute_ConfirmCommand(object obj)
        {
            CreateRenovation((DateRanges)View.DateRangesDataGrid.SelectedItem, Description);
            NavService.Navigate(new AccommodationsDisplayView(Owner,NavService, Window));
            Window.PageTitle = "Smještaji";
        }

        private bool CanExecute_ConfirmCommand(object obj)
        {
            return (DateRanges)View.DateRangesDataGrid.SelectedItem != null && IsValid; 
        }

        public void CreateRenovation(DateRanges dateRange, string description)
        {
            renovationService.CreateRenovation(dateRange, description, SelectedAccommodation.Id, SelectedAccommodation);
        }

        public void FindAvailableDates(DateOnly firstDate, DateOnly secondDate, int duration)
        {
            FirstDate = firstDate;
            SecondDate = secondDate;
            Duration = duration;
            List<DateRanges> availableDates = new List<DateRanges>();
            availableDates = accommodationScheduleService.FindDates(firstDate, secondDate, duration, SelectedAccommodation.Id);
            Update(availableDates);
        }
        public void Update(List<DateRanges> availableDates)
        {
            DateRanges.Clear();
            foreach (DateRanges date in availableDates)
            {
                DateRanges.Add(date);
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
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Unesite opis!";
                 
                    if (Description == "Unesite opis...")
                        return "Unesite opis!";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Description" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null || View.FirstDatePicker.SelectedDate == null ||
                        View.SecondDatePicker.SelectedDate == null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
