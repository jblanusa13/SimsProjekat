using ProjectSims.Model;
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

        private List<Guest1> guests1;

        public Guest1FileHandler()
        {
            _serializer = new Serializer<Guest1>();
            guests1 = _serializer.FromCSV(FilePath);
        }

        public Guest1 GetByUserId(int id)
        {
            guests1 = _serializer.FromCSV(FilePath);
            return guests1.FirstOrDefault(g => g.UserId == id);
        }
    }
}
