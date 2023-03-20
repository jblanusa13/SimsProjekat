using ProjectSims.Model;
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

        private List<Guest2> guests2;

        public Guest2FileHandler()
        {
            _serializer = new Serializer<Guest2>();
            guests2 = _serializer.FromCSV(FilePath);
        }

        public Guest2 GetByUserId(int id)
        {
            guests2 = _serializer.FromCSV(FilePath);
            return guests2.FirstOrDefault(g => g.UserId == id);
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
