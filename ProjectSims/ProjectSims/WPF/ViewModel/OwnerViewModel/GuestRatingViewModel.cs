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
        public AccommodationReservation SelectedAccommodationReservation { get; set; }
        public AccommodationReservationService accommodationReservationService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        public GuestRatingService guestRatingService;

        public DateRangesService dateRangesService;
        public GuestRatingViewModel(AccommodationReservation selectedAccommodationReservation, Owner o) 
        {
            Owner = o;
            SelectedAccommodationReservation = selectedAccommodationReservation;
            accommodationReservationService = new AccommodationReservationService();
            accommodationReservationService.Subscribe(this);
            guestRatingService = new GuestRatingService();
            guestRatingService.Subscribe(this);
            dateRangesService = new DateRangesService();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(accommodationReservationService.GetAllReservations());
        }

        public void RateGuest(AccommodationReservation SelectedAccommodationReservation, int cleanlinessRate, int respectingRulesRate, int tidinessRate, int communicationRate, string comment)
        {
            SelectedAccommodationReservation.RatedGuest = true;
            accommodationReservationService.Update(SelectedAccommodationReservation);
            guestRatingService.Create(new GuestRating(-1, cleanlinessRate, respectingRulesRate, tidinessRate, communicationRate, comment, SelectedAccommodationReservation.Id, SelectedAccommodationReservation, DateOnly.FromDateTime(DateTime.Now), SelectedAccommodationReservation.GuestId));
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
