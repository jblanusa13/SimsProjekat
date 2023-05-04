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
        public ObservableCollection<GuestAccommodation> GuestAccommodations { get; set; }
        private GuestAccommodationService guestAccommodationService;
        private OwnerService ownerService;
        
        public AccommodationsDisplayViewModel(Owner o) {
            Owner = o;
            guestAccommodationService = new GuestAccommodationService();
            guestAccommodationService.Subscribe(this); 
            GuestAccommodations = new ObservableCollection<GuestAccommodation>(guestAccommodationService.GetAllGuestAccommodations());
            ownerService = new OwnerService();
        }
        public bool HasWaitingRequests(Owner owner)
        {
            return ownerService.HasWaitingRequests(owner.Id);
        }
        public bool IsNotRated(GuestAccommodation SelectedGuestAccommodation)
        {
            return SelectedGuestAccommodation.Rated == false;
        }

        public bool IsLessThan5Days(GuestAccommodation SelectedGuestAccommodation)
        {
            return SelectedGuestAccommodation.CheckOutDate.AddDays(5) >= DateOnly.FromDateTime(DateTime.Today);
        }

        public void Update()
        {
            GuestAccommodations.Clear();
            foreach (GuestAccommodation guestAccommodation in guestAccommodationService.GetAllGuestAccommodations())
            {
                GuestAccommodations.Add(guestAccommodation);
            }
        }
    }
}
