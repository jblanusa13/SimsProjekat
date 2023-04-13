using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class ReservationTourService
    {
        private ReservationTourRepository reservations;
        private TourService tourService;
        private Guest2Service guestService;

        public ReservationTourService()
        {
            reservations = new ReservationTourRepository();
            tourService = new TourService();
            guestService = new Guest2Service();
        }
        public List<ReservationTour> GetAllReservations()
        {
            return reservations.GetAll();
        }
        public List<ReservationTour> GetReservationsByTourId(Tour tour)
        {
            return reservations.GetReservationsByTourId(tour);
        }
        public List<int> GetGuestIdsByStateAndTourId(Tour tour, Guest2State state)
        {
            return reservations.GetGuestIdsByStateAndTourId(tour, state);
        }
        public ReservationTour GetReservationByGuestAndTour(Tour tour, Guest2 guest2)
        {
            return reservations.GetReservationByGuestAndTour(tour, guest2);
        }
        public int  GetNumberOfGuestsWhoUsedVoucher(Tour tour)
        {
            return reservations.GetNumberOfGuestsWhoUsedVoucher(tour);
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
            List<ReservationTour> tourReservations = GetReservationsByTourId(tour);
            tourReservations.ForEach(r => r.State = state);
            tourReservations.ForEach(r => Update(r));
        }
        public void UpdateGuestState(Guest2 guest,Tour tour,Guest2State state)
        {
            ReservationTour reservation = GetReservationsByTourId(tour).Find(r=>r.Guest2Id == guest.Id);
            reservation.State = state;
            if(state == Guest2State.Present)
            {
                reservation.KeyPointWhereGuestArrivedId = tour.ActiveKeyPointId;
                tourService.UpdateNumberOfGuests(guestService.GetAge(guest),tour);
            }
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
