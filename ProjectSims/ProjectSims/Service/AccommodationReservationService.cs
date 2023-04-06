using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;

namespace ProjectSims.Service
{
    public class AccommodationReservationService
    {
        private AccommodationReservationRepository reservations;
        public AccommodationReservationService()
        {
            reservations = new AccommodationReservationRepository();
        }

        public Guest1 GetGuestByUsername(string username)
        {
            return reservations.GetGuestByUsername(username);
        }

        public User GetUser(int userId)
        {
            return reservations.GetUser(userId);
        }

        public List<AccommodationReservation> GetAllReservations()
        {
            return reservations.GetAll();
        }

        public List<DateRanges> FindAvailableDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
             return reservations.FindAvailableDates(firstDate, lastDate, daysNumber, accommodationId);
        }

        public void CreateReservation(int accommodationId, int guestId, DateOnly checkIn, DateOnly checkOut, int guestNumber)
        {
            reservations.Add(accommodationId, guestId, checkIn, checkOut, guestNumber);
        }

        public void Subscribe(IObserver observer)
        {
            reservations.Subscribe(observer);
        }
    }
}
