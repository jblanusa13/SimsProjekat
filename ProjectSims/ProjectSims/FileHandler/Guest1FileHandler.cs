using ProjectSims.Model;
using ProjectSims.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace ProjectSims.FileHandler
{
    class Guest1FileHandler
    {
        private const string FilePath = "../../../Resources/Data/guest1.csv";

        private Serializer<Guest1> _serializer;

        private List<Guest1> guests1;

        public Guest1FileHandler()
        {
            _serializer = new Serializer<Guest1>();
            guests1 = _serializer.FromCSV(FilePath);
        }
        public List<Guest1> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guest1> guests)
        {
            _serializer.ToCSV(FilePath, guests);
        }

        public Guest1 GetByUserId(int id)
        {
            guests1 = _serializer.FromCSV(FilePath);
            return guests1.FirstOrDefault(g => g.UserId == id);
        }
    }
}
