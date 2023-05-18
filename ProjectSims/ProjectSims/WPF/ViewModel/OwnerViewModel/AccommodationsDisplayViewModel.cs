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
        public ObservableCollection<Accommodation> AccommodationsForDisplay { get; set; }
        private AccommodationService accommodationService;
        private OwnerService ownerService;
        
        public AccommodationsDisplayViewModel(Owner o) {
            Owner = o;
            accommodationService = new AccommodationService();
            accommodationService.Subscribe(this); 
            AccommodationsForDisplay = new ObservableCollection<Accommodation>(accommodationService.GetAccommodationsByOwner(Owner.Id));
            ownerService = new OwnerService();
        }
        public bool HasWaitingRequests(Owner owner)
        {
            return ownerService.HasWaitingRequests(owner.Id);
        }

        public void UpdateAccommodationsIfRenovated()
        {
            accommodationService.UpdateIfRenovated(accommodationService.GetAccommodationsByOwner(Owner.Id));
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
