using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using ProjectSims.FileHandler;

namespace ProjectSims.Service
{
    public class AccommodationReservationService
    {
        private AccommodationReservationRepository reservationRepository;
        private List<AccommodationReservation> reservations;

        public AccommodationReservationService()
        {
            reservationRepository = new AccommodationReservationRepository();
            reservations = reservationRepository.GetAll();
        }

        public AccommodationReservation GetReservation(int id)
        {
            return reservationRepository.Get(id);
        }

        public List<AccommodationReservation> GetReservationByGuest(int guestId)
        {
            return reservationRepository.GetByGuest(guestId);
        }
 
        public int NextId()
        {
            return reservations.Max(r => r.Id) + 1;
        }
        public void CreateReservation(int accommodationId, int guestId, DateOnly checkIn, DateOnly checkOut, int guestNumber)
        {
            int id = NextId();
            AccommodationReservation reservation = new AccommodationReservation(id, accommodationId, guestId, checkIn, checkOut, guestNumber, ReservationState.Active);
            reservationRepository.Add(reservation);
        }

        public void RemoveReservation(AccommodationReservation reservation)
        {
            reservation.State = ReservationState.Canceled;
            reservationRepository.Update(reservation);
        }
        public bool CanCancel(AccommodationReservation reservation)
        {
            int dismissalDays = reservation.Accommodation.DismissalDays;
            if(DateOnly.FromDateTime(DateTime.Today) <= reservation.CheckInDate.AddDays(-dismissalDays))
            {
                return true;
            }
            return false;
        }

        public void Subscribe(IObserver observer)
        {
            reservationRepository.Subscribe(observer);
        }
    }
}
