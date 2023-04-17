using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;

namespace ProjectSims.Service
{
    public class ReservationTourService
    {
        private ReservationTourRepository reservations;
        private Guest2Service guestService;

        public ReservationTourService()
        {
            reservations = new ReservationTourRepository();
            guestService = new Guest2Service();
        }
        public List<ReservationTour> GetAllReservations()
        {
            return reservations.GetAll();
        }
        public List<ReservationTour> GetReservationsByTour(Tour tour)
        {
            return reservations.GetReservationsByTour(tour);
        }
        public List<ReservationTour> GetReservationsByTourAndState(Tour tour,Guest2State state)
        {
            return reservations.GetReservationsByTourAndState(tour, state);
        }
        public List<int> GetGuestIdsByTourAndState(Tour tour, Guest2State state)
        {
            return reservations.GetGuestIdsByTourAndState(tour, state);
        }
        public ReservationTour GetReservationByGuestAndTourId(int tourId, int guestId)
        {
            return reservations.GetReservationByGuestAndTourId(tourId, guestId);
        }
        public int GetNumberOfPresentGuests(Tour tour)
        {
            int numberOfPresentGuests = 0;
            List<ReservationTour> wantedReservations = GetReservationsByTourAndState(tour, Guest2State.Present);
            wantedReservations.ForEach(r => numberOfPresentGuests += r.NumberGuest);
            return numberOfPresentGuests;
        }
        public int GetNumberOfPresentGuestsWithVoucher(Tour tour)
        {
            int numberOfPresentGuestsWithVoucher = 0;
            List<ReservationTour> wantedReservations = GetReservationsByTourAndState(tour, Guest2State.Present);
            wantedReservations.ForEach(r => { if (r.UsedVoucher) numberOfPresentGuestsWithVoucher += r.NumberGuest; });
            return numberOfPresentGuestsWithVoucher;
        }
        public double GetPercentageOfPresentGuestsWithVoucher(Tour tour)
        {
            double numberOfPresentGuestsWithVoucher = (double)GetNumberOfPresentGuestsWithVoucher(tour);
            double numberOfPresentGuests = (double)GetNumberOfPresentGuests(tour);
            if (numberOfPresentGuestsWithVoucher != 0)
            {
                double percentage = (numberOfPresentGuestsWithVoucher /numberOfPresentGuests) *100;
                return Math.Round(percentage, 2);
            }
            else
                return 0;
        }
        public int GetNumberOfPresentGuestsByAgeLimit(Tour tour,int lowerLimit)
        {
            int numberOfPresentGuestsThatAge = 0;
            List<ReservationTour> wantedReservations = GetReservationsByTourAndState(tour, Guest2State.Present);
            if (lowerLimit == 0)
                wantedReservations.ForEach(r => { if (r.GuestAgeOnTour <18 ) numberOfPresentGuestsThatAge += r.NumberGuest; });
            else if(lowerLimit == 18)
                wantedReservations.ForEach(r => { if (r.GuestAgeOnTour >= 18 && r.GuestAgeOnTour <=50 ) numberOfPresentGuestsThatAge += r.NumberGuest; });
            else
                wantedReservations.ForEach(r => { if (r.GuestAgeOnTour > 50) numberOfPresentGuestsThatAge += r.NumberGuest; });
            return numberOfPresentGuestsThatAge;
        }
        public void Create(ReservationTour reservation)
        {
            reservations.Create(reservation);
        }
        public void Remove(ReservationTour reservation)
        {
            reservations.Remove(reservation);
        }  
        public void UpdateGuestsState(Tour tour,Guest2State state)
        {
            List<ReservationTour> tourReservations = GetReservationsByTour(tour);
            tourReservations.ForEach(r => r.State = state);
            tourReservations.ForEach(r => Update(r));
        }
        public void UpdateGuestState(Guest2 guest,Tour tour,Guest2State state)
        {
            ReservationTour reservation = GetReservationsByTour(tour).Find(r=>r.Guest2Id == guest.Id);
            reservation.State = state;
            if(state == Guest2State.Present)
                reservation.KeyPointWhereGuestArrivedId = tour.ActiveKeyPointId;
            Update(reservation);
        }
        public ReservationTour GetTourIdWhereGuestIsWaiting(Guest2 guest)
        {
            return reservations.GetTourIdWhereGuestIsWaiting(guest);
        }
        public List<int> FindTourIdsWhereGuestPresent(int guestId)
        {
            List<int> tourIds = new List<int>();
            foreach (ReservationTour reservation in GetAllReservations())
            {
                if(reservation.Guest2Id == guestId && reservation.State == Guest2State.Present && reservation.RatedTour != true)

                {
                    tourIds.Add(reservation.TourId);
                }
            }
            return tourIds;
        }
        public void Update(ReservationTour reserevation)
        {
            reservations.Update(reserevation);
        }
        public void Subscribe(IObserver observer)
        {
            reservations.Subscribe(observer);
        }
    }
}
