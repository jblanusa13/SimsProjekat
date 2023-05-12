using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    public class GuestRatingFileHandler
    {
        private const string FilePath = "../../../Resources/Data/guestRating.csv";

        private readonly Serializer<GuestRating> _serializer;

        public GuestRatingFileHandler()
        {
            _serializer = new Serializer<GuestRating>();
        }

        public List<GuestRating> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<GuestRating> guestRatings)
        {
            _serializer.ToCSV(FilePath, guestRatings);
        }
    }
}