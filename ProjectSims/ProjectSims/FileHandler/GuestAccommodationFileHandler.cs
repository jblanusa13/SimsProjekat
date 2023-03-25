using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    class GuestAccommodationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/guestAccommodation.csv";

        private readonly Serializer<GuestAccommodation> _serializer;

        public GuestAccommodationFileHandler()
        {
            _serializer = new Serializer<GuestAccommodation>();
        }

        public List<GuestAccommodation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<GuestAccommodation> accommodations)
        {
            _serializer.ToCSV(FilePath, accommodations);
        }
    }
}
