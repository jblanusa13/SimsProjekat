using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Model;
using ProjectSims.ModelDAO;
using ProjectSims.Observer;

namespace ProjectSims.Controller
{
    public class AccommodationReservationController
    {
        private AccommodationReservationDAO _reservations;
        public AccommodationReservationController()
        {
            _reservations = new AccommodationReservationDAO();
        }

        public Guest1 GetGuestByUsername(string username)
        {
            return _reservations.GetGuestByUsername(username);
        }

        public User GetUser(int userId)
        {
            return _reservations.GetUser(userId);
        }

        public List<AccommodationReservation> GetAllReservations()
        {
            return _reservations.GetAll();
        }

        public List<DateRanges> FindAvailableDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
             return _reservations.FindAvailableDates(firstDate, lastDate, daysNumber, accommodationId);
        }

        public void CreateReservation(int accommodationId, int guestId, DateOnly checkIn, DateOnly checkOut, int guestNumber)
        {
            _reservations.Add(accommodationId, guestId, checkIn, checkOut, guestNumber);
        }

        public void Subscribe(IObserver observer)
        {
            _reservations.Subscribe(observer);
        }
    }
}
