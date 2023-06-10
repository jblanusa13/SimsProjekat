using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class AccommodationsDisplayViewModel : IObserver
    {
        public Owner Owner { get; set; }
        public ObservableCollection<Accommodation> AccommodationsForDisplay { get; set; }

        private AccommodationService accommodationService;
        private RequestService requestService;
        private AccommodationReservationService accommodationReservationService;
        private RenovationScheduleService renovationScheduleService;
        public NavigationService NavService { get; set; }

        public AccommodationsDisplayViewModel(Owner o, NavigationService navService) {
            Owner = o;
            NavService = navService;
            accommodationService = new AccommodationService();
            accommodationService.Subscribe(this);
            accommodationReservationService = new AccommodationReservationService();
            AccommodationsForDisplay = new ObservableCollection<Accommodation>(accommodationService.GetAccommodationsByOwner(Owner.Id));
            requestService = new RequestService();
            renovationScheduleService = new RenovationScheduleService();
        }
        public bool HasWaitingRequests(Owner owner)
        {
            return requestService.HasWaitingRequests(owner.Id);
        }

        public void UpdateAccommodationsIfRenovated()
        {
            renovationScheduleService.UpdateIfRenovated(accommodationService.GetAccommodationsByOwner(Owner.Id));
        }

        public void Update()
        {
            AccommodationsForDisplay.Clear();
            foreach (Accommodation accommodation in accommodationService.GetAccommodationsByOwner(Owner.Id))
            {
                AccommodationsForDisplay.Add(accommodation);
            }
        }
    }
}
