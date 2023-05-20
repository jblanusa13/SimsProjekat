using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class Guest2FileHandler
    {
        private const string FilePath = "../../../Resources/Data/guest2.csv";


        private Serializer<Guest2> _serializer;
        public Guest2FileHandler()
        {
            _serializer = new Serializer<Guest2>();
        }
        public List<Guest2> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guest2> guests)
        {
            _serializer.ToCSV(FilePath, guests);
        }
    }
}
