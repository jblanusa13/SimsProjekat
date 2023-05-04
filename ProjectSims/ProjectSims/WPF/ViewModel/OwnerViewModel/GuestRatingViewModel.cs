using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class GuestRatingViewModel : IObserver
    {
        Owner Owner { get; set; }
        public GuestAccommodation SelectedGuestAccommodation { get; set; }
        public GuestAccommodationService guestAccommodationService;
        public ObservableCollection<GuestAccommodation> GuestAccommodations { get; set; }

        public AccommodationReservationService accommodationReservationService;

        public GuestRatingService guestRatingService;

        public DateRangesService dateRangesService;
        public GuestRatingViewModel(GuestAccommodation selectedGuestAccommodation, Owner o) 
        {
            Owner = o;
            SelectedGuestAccommodation = selectedGuestAccommodation;
            guestAccommodationService = new GuestAccommodationService();
            guestAccommodationService.Subscribe(this);
            accommodationReservationService = new AccommodationReservationService();
            accommodationReservationService.Subscribe(this);
            guestRatingService = new GuestRatingService();
            guestRatingService.Subscribe(this);
            dateRangesService = new DateRangesService();
            GuestAccommodations = new ObservableCollection<GuestAccommodation>(guestAccommodationService.GetAllGuestAccommodations());
        }

        public void RateGuest(GuestAccommodation SelectedGuestAccommodation, int cleanlinessRate, int respectingRulesRate, int tidinessRate, int communicationRate, string comment)
        {
            SelectedGuestAccommodation.Rated = true;
            guestAccommodationService.Update(SelectedGuestAccommodation);
            AccommodationReservation accommodationReservation = accommodationReservationService.GetReservation(SelectedGuestAccommodation.GuestId, SelectedGuestAccommodation.AccommodationId, SelectedGuestAccommodation.CheckInDate, SelectedGuestAccommodation.CheckOutDate);
            guestRatingService.Create(new GuestRating(-1, cleanlinessRate, respectingRulesRate, tidinessRate, communicationRate, comment, accommodationReservation, DateOnly.FromDateTime(DateTime.Now), SelectedGuestAccommodation.GuestId));
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
