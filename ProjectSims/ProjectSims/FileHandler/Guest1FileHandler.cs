using ProjectSims.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<<<< Temporary merge branch 1
using ProjectSims.Model;
using ProjectSims.Serializer;
<<<<<<<<< Temporary merge branch 1
using ProjectSims.Model;
using ProjectSims.Serializer;

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

        public void Save(List<Guest1> guests1)
        {
            _serializer.ToCSV(FilePath, guests1);
        }
        public Guest1 GetByUserId(int id)
        {
            guests1 = _serializer.FromCSV(FilePath);
            return guests1.FirstOrDefault(g => g.UserId == id);
        }
    }
}
