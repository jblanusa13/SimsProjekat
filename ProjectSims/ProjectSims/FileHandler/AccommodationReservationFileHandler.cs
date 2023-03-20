using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    class AccommodationReservationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/accommodationReservation.csv";

        private readonly Serializer<AccommodationReservation> _serializer;

        public AccommodationReservationFileHandler()
        {
            _serializer = new Serializer<AccommodationReservation>();
        }

        public List<AccommodationReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationReservation> reservations)
        {
            _serializer.ToCSV(FilePath, reservations);
        }
    }
}
