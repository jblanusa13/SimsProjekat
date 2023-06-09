using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class Guest1FileHandler
    {
        private const string FilePath = "../../../Resources/Data/guest1.csv";


        private Serializer<Guest1> _serializer;
        public Guest1FileHandler()
        {
            _serializer = new Serializer<Guest1>();
        }
        public List<Guest1> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guest1> guests1)
        {
            _serializer.ToCSV(FilePath, guests1);
        }
    }
}
