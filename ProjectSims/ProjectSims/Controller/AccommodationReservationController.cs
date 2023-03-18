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
        public List<AccommodationReservation> GetAllReservations()
        {
            return _reservations.GetAll();
        }

        public List<DateRanges> FindAvailableDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
             return _reservations.FindAvailableDates(firstDate, lastDate, daysNumber, accommodationId);
        }

        public List<DateRanges> FindDates(DateOnly firstDate, DateOnly lastDate, int accommodationId)
        {
            return _reservations.FindDates(firstDate,lastDate, accommodationId);
        }

        public void Subscribe(IObserver observer)
        {
            _reservations.Subscribe(observer);
        }
    }
}
