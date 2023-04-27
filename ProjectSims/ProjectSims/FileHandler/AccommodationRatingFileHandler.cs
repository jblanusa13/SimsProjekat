using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    class AccommodationRatingFileHandler
    {
        private const string FilePath = "../../../Resources/Data/accommodationRating.csv";

        private readonly Serializer<AccommodationAndOwnerRating> _serializer;

        public AccommodationRatingFileHandler()
        {
            _serializer = new Serializer<AccommodationAndOwnerRating>();
        }

        public List<AccommodationAndOwnerRating> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationAndOwnerRating> reservations)
        {
            _serializer.ToCSV(FilePath, reservations);
        }
    }
}
