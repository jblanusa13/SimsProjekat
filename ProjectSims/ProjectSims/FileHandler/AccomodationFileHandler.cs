using ProjectSims.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class AccomodationFileHandler
    {
        private const string FilePath = "../../Resources/Data/accomodation.csv";

        private readonly Serializer<Accomodation> _serializer;

        public AccomodationFileHandler()
        {
            _serializer = new Serializer<Accomodation>();
        }

        public List<Accomodation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Accomodation> accomodations)
        {
            _serializer.ToCSV(FilePath, accomodations);
        }
    }
}
