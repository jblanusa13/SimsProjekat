using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    class LocationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/location.csv";

        private readonly Serializer<Location> _serializer;

        public LocationFileHandler()
        {
            _serializer = new Serializer<Location>();
        }

        public List<Location> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Location> locations)
        {
            _serializer.ToCSV(FilePath, locations);
        }
    }
}

