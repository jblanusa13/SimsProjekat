using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.Observer;

namespace ProjectSims.ModelDAO
{
    public class AccommodationReservationDAO 
    {
        private AccommodationReservationFileHandler _reservationFileHandler;
        private List<AccommodationReservation> _reservations;

        public AccommodationReservationDAO()
        {
            _reservationFileHandler = new AccommodationReservationFileHandler();
            _reservations = _reservationFileHandler.Load();
        }
        public List<AccommodationReservation> GetAll()
        {
            return _reservations;
        }

    }
}
