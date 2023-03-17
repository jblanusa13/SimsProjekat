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
    }
}
