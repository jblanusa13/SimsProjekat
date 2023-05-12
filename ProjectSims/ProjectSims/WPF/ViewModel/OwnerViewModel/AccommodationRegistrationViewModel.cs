using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Repository;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class AccommodationRegistrationViewModel : Page
    {
        private AccommodationService accommodationService;
        private LocationService locationService;
        private OwnerService ownerService;
        public Owner owner { get; set; }
        public AccommodationRegistrationViewModel(Owner o)
        {
            accommodationService = new AccommodationService();
            locationService = new LocationService();
            ownerService = new OwnerService();
            owner = o;
        }

        public void RegisterAccommodation(string Location, List<string> Pics, string AccommodationName, AccommodationType Type, int GuestsMaximum, int MinimumReservationDays, int DismissalDays)
        {
            locationService.Add(Location);
            int IdLocation = locationService.GetIdByLocation(Location);
            Location location = new Location(IdLocation, Location.ToString().Split(",")[0], Location.ToString().Split(",")[1]);
            Accommodation accommodation = new Accommodation(-1, AccommodationName, IdLocation, location, Type, GuestsMaximum, MinimumReservationDays, DismissalDays, Pics, owner.Id, -1);
            accommodationService.Create(accommodation);
            ownerService.AddAccommodation(owner, accommodation.Id);
            ownerService.Update(owner);
        }
    }
}
