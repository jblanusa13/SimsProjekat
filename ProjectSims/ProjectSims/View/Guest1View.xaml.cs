using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectSims.Controller;
using ProjectSims.Model;
using ProjectSims.Observer;
using ProjectSims.View;

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for Guest1View.xaml
    /// </summary>
    public partial class Guest1View : Window, IObserver
    {
        private readonly AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public Guest1 Guest { get; set; }
        public string NameSearch { get; set; }
        //public string CitySearch { get; set; }
        //public string CountrySearch { get; set; }
        public string LocationSearch { get; set; }
        public string TypeSearch { get; set; }
        public string GuestsNumberSearch { get; set; }
        public string DaysNumberSearch { get; set; }
        
        
        public Guest1View(Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationController = new AccommodationController();
            _accommodationController.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllAccommodations());

            Guest = guest;
        }

        public void TextboxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameSearch = TextboxName.Text;
        }

        //public void TextboxCity_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //   CitySearch = TextboxCity.Text;
        //}

        //ublic void TextboxCountry_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //  CountrySearch = TextboxCountry.Text;
        //}
        public void TextboxLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            LocationSearch = TextboxLocation.Text;
        }

        public void TextboxType_TextChanged(object sender, TextChangedEventArgs e)
        {
            TypeSearch = TextboxType.Text;
        }

        public void TextboxGuests_TextChanged(object sender, TextChangedEventArgs e)
        {
            GuestsNumberSearch = TextboxGuests.Text;
        }

        public void TextboxDays_TextChanged(object sender, TextChangedEventArgs e)
        {
            DaysNumberSearch = TextboxDays.Text;
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            Accommodations.Clear();


            foreach (Accommodation accommodation in _accommodationController.GetAllAccommodations())
            {
                if (CheckSearchConditions(accommodation))
                {
                    Accommodations.Add(accommodation);
                }
            }
        }

        public bool CheckSearchConditions(Accommodation accommodation)
        {
            bool ContainsName, ContainsLocation, ContainsType, GuestsNumberIsLower, DaysNumberIsGreater;

            ContainsName = string.IsNullOrEmpty(NameSearch) ? true : accommodation.Name.ToLower().Contains(NameSearch.ToLower());
            ContainsLocation = true;// string.IsNullOrEmpty(LocationSearch) ? true : accommodation.Location.ToLower().Contains(LocationSearch.ToLower());
            //ContainsCity = string.IsNullOrEmpty(CitySearch) ? true : _accomodationController.FindLocation(accommodation.LocationId).City.ToLower().Contains(CitySearch.ToLower());
            //ContainsCountry = string.IsNullOrEmpty(CountrySearch) ? true : _accomodationController.FindLocation(accommodation.LocationId).Country.ToLower().Contains(CountrySearch.ToLower());
            ContainsType = string.IsNullOrEmpty(TypeSearch) ? true : accommodation.Type.ToString().ToLower().Contains(TypeSearch.ToLower());
            GuestsNumberIsLower = string.IsNullOrEmpty(GuestsNumberSearch) ? true : Convert.ToInt32(GuestsNumberSearch) <= accommodation.GuestsMaximum && Convert.ToInt32(GuestsNumberSearch) >= 0;
            DaysNumberIsGreater = string.IsNullOrEmpty(DaysNumberSearch) ? true : Convert.ToInt32(DaysNumberSearch) >= accommodation.DismissalDays;


            return ContainsName && ContainsLocation && ContainsType && GuestsNumberIsLower && DaysNumberIsGreater;
        }

        public void Reservation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodation = (Accommodation)AccommodationsTable.SelectedItem;
            if (SelectedAccommodation != null)
            {
                AccommodationReservationView reservation = new AccommodationReservationView(SelectedAccommodation, Guest);
                reservation.Show();
            }
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach(var accommodation in _accommodationController.GetAllAccommodations())
            {
                Accommodations.Add(accommodation);
            }
        }
    }
}