using ProjectSims.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class ReservationTourFileHandler
    {
        private const string FilePath = "../../../Resources/Data/reservationtour.csv";

        private Serializer<ReservationTour> _serializer;

        public ReservationTourFileHandler()
        {
            _serializer = new Serializer<ReservationTour>();
        }

        public List<ReservationTour> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<ReservationTour> reservations)
        {
            _serializer.ToCSV(FilePath, reservations);
        }
    }
}
