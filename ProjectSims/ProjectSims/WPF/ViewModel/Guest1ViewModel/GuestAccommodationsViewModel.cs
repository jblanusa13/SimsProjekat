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

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public partial class GuestAccommodationsViewModel : IObserver
    {
        private readonly AccommodationService accommodationService;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Guest1 Guest { get; set; }

        public string NameSearch { get; set; }
        public string CitySearch { get; set; }
        public string CountrySearch { get; set; }
        public string TypeSearch { get; set; }
        public string GuestsNumberSearch { get; set; }
        public string DaysNumberSearch { get; set; }
        public GuestAccommodationsViewModel(Guest1 guest)
        {
            accommodationService = new AccommodationService();
            accommodationService.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAllAccommodations());

            Guest = guest;
        }
        public void Search(string name, string city, string country, string type, string guestsNumber, string daysNumber)
        {
            NameSearch = name;
            CitySearch = city;
            CountrySearch = country;
            TypeSearch = type;
            GuestsNumberSearch = guestsNumber;
            DaysNumberSearch = daysNumber;

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

            ContainsName = string.IsNullOrEmpty(NameSearch) ? true : accommodation.Name.ToLower().Contains(NameSearch.ToLower());
            ContainsCity = string.IsNullOrEmpty(CitySearch) ? true : accommodation.Location.City.ToLower().Contains(CitySearch.ToLower());
            ContainsCountry = string.IsNullOrEmpty(CountrySearch) ? true : accommodation.Location.Country.ToLower().Contains(CountrySearch.ToLower());
            ContainsType = string.IsNullOrEmpty(TypeSearch) ? true : accommodation.Type.ToString().ToLower().Contains(TypeSearch.ToLower());
            GuestsNumberIsLower = string.IsNullOrEmpty(GuestsNumberSearch) ? true : Convert.ToInt32(GuestsNumberSearch) <= accommodation.GuestsMaximum && Convert.ToInt32(GuestsNumberSearch) >= 0;
            DaysNumberIsGreater = string.IsNullOrEmpty(DaysNumberSearch) ? true : Convert.ToInt32(DaysNumberSearch) >= accommodation.DismissalDays;


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
    }
}
