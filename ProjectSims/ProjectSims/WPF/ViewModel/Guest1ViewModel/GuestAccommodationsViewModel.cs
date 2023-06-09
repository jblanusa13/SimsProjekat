using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System.Windows.Controls;
using ProjectSims.Observer;
using System.Windows.Controls.Primitives;
using ProjectSims.FileHandler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Navigation;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.View.Guest1View;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public partial class GuestAccommodationsViewModel : IObserver, INotifyPropertyChanged
    {
        private AccommodationService accommodationService;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Guest1 Guest { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        private string city;
        public string City
        {
            get => city;
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }
        private string country;
        public string Country
        {
            get => country;
            set
            {
                if (value != country)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }
        private string type;
        public string Type
        {
            get => type;
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }
        private string guestNumber;
        public string GuestNumber
        {
            get => guestNumber;
            set
            {
                if (value != guestNumber)
                {
                    guestNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string daysNumber;
        public string DaysNumber
        {
            get => daysNumber;
            set
            {
                if (value != daysNumber)
                {
                    daysNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public MyICommand SearchCommand { get; set; }
        public MyICommand ThemeCommand { get; set; }
        public MyICommand AnywhereCommand { get; set; }
        public MyICommand MyReservationsCommand { get; set; }
        public MyICommand ShowRatingsCommand { get; set; }
        public MyICommand RateAccommodationCommand { get; set; }
        public MyICommand ForumCommand { get; set; }
        public MyICommand ProfileCommand { get; set; }
        public MyICommand<GuestAccommodationsView> LogOutCommand { get; set; }
        public NavigationService NavService { get; set; }
        public GuestAccommodationsViewModel(Guest1 guest, NavigationService navigation)
        {
            accommodationService = new AccommodationService();

            accommodationService.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAllAccommodationsForGuestView());

            Guest = guest;

            SearchCommand = new MyICommand(OnSearch);
            ThemeCommand = new MyICommand(OnTheme);
            AnywhereCommand = new MyICommand(OnAnywhere);
            MyReservationsCommand = new MyICommand(OnMyReservations);
            ShowRatingsCommand = new MyICommand(OnShowRatings);
            RateAccommodationCommand = new MyICommand(OnRate);
            ForumCommand = new MyICommand(OnForum);
            ProfileCommand = new MyICommand(OnProfile);
            LogOutCommand = new MyICommand<GuestAccommodationsView>(OnLogOut);

            NavService = navigation;
        }
        public void OnAnywhere()
        {
            NavService.Navigate(new AnytimeAnywhere(Guest, NavService));
        }
        public void OnMyReservations()
        {
            NavService.Navigate(new MyReservations(Guest, NavService));
        }
        public void OnShowRatings()
        {
            NavService.Navigate(new RatingsView(Guest));
        }
        public void OnRate()
        {
            RatingStartView accommodationForRating = new RatingStartView(Guest);
            accommodationForRating.Show();
        }
        public void OnForum()
        {
            NavService.Navigate(new View.Guest1View.MainPages.Forum(Guest, NavService));
        }
        public void OnProfile()
        {
            NavService.Navigate(new Profile(Guest));
        }
        private void OnLogOut(GuestAccommodationsView page)
        {
            var login = new MainWindow();
            login.Show();
            Window parentWindow = Window.GetWindow(page);
            parentWindow.Close();
        }
        private void OnTheme()
        {
            App app = (App)Application.Current;

            if (App.IsDark)
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                App.IsDark = false;
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                App.IsDark = true;
            }
        }

        public void OnSearch()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in accommodationService.GetAllAccommodations())
            {
                if (CheckSearchConditions(accommodation))
                {
                    Accommodations.Add(accommodation);
                }
            }
        }

        public bool CheckSearchConditions(Accommodation accommodation)
        {
            bool ContainsName, ContainsCity, ContainsCountry, ContainsType, GuestsNumberIsLower, DaysNumberIsGreater;

            ContainsName = string.IsNullOrEmpty(Name) ? true : accommodation.Name.ToLower().Contains(Name.ToLower());
            ContainsCity = string.IsNullOrEmpty(City) ? true : accommodation.Location.City.ToLower().Contains(City.ToLower());
            ContainsCountry = string.IsNullOrEmpty(Country) ? true : accommodation.Location.Country.ToLower().Contains(Country.ToLower());
            ContainsType = string.IsNullOrEmpty(Type) ? true : accommodation.Type.ToString().ToLower().Contains(Type.ToLower());
            GuestsNumberIsLower = string.IsNullOrEmpty(GuestNumber) ? true : Convert.ToInt32(GuestNumber) <= accommodation.GuestsMaximum && Convert.ToInt32(GuestNumber) >= 0;
            DaysNumberIsGreater = string.IsNullOrEmpty(DaysNumber) ? true : Convert.ToInt32(DaysNumber) >= accommodation.DismissalDays;


            return ContainsName && ContainsCity && ContainsCountry && ContainsType && GuestsNumberIsLower && DaysNumberIsGreater;
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach(Accommodation accommodation in accommodationService.GetAllAccommodations())
            {
                Accommodations.Add(accommodation);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
