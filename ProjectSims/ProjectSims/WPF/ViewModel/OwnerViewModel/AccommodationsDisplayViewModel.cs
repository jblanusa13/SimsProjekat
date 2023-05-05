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

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class AccommodationsDisplayViewModel : IObserver
    {
        public Owner Owner { get; set; }
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        private AccommodationReservationService accommodationReservationService;
        private OwnerService ownerService;
        
        public AccommodationsDisplayViewModel(Owner o) {
            Owner = o;
            accommodationReservationService = new AccommodationReservationService();
            accommodationReservationService.Subscribe(this); 
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(accommodationReservationService.GetAllReservations());
            ownerService = new OwnerService();
        }
        public bool HasWaitingRequests(Owner owner)
        {
            return ownerService.HasWaitingRequests(owner.Id);
        }
        public bool IsNotRated(AccommodationReservation SelectedAccommodationReservation)
        {
            return SelectedAccommodationReservation.RatedGuest == false;
        }

        public bool IsLessThan5Days(AccommodationReservation SelectedAccommodationReservation)
        {
            return SelectedAccommodationReservation.CheckOutDate.AddDays(5) >= DateOnly.FromDateTime(DateTime.Today);
        }

        public void Update()
        {
            AccommodationReservations.Clear();
            foreach (AccommodationReservation guestAccommodation in accommodationReservationService.GetAllReservations())
            {
                AccommodationReservations.Add(guestAccommodation);
            }
        }
    }
}
