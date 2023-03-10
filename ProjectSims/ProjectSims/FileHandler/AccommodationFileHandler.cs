using ProjectSims.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class AccommodationFileHandler
    {
        private const string FilePath = "../../Resources/Data/accommodation.csv";

        private readonly Serializer<Accommodation> _serializer;

        public AccommodationFileHandler()
        {
            _serializer = new Serializer<Accommodation>();
        }

        public List<Accommodation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Accommodation> accommodations)
        {
            _serializer.ToCSV(FilePath, accommodations);
        }
    }
}
