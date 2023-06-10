using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    public class SuperGuestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/superGuest.csv";


        private Serializer<SuperGuest> serializer;
        public SuperGuestFileHandler()
        {
            serializer = new Serializer<SuperGuest>();
        }
        public List<SuperGuest> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<SuperGuest> guests)
        {
            serializer.ToCSV(FilePath, guests);
        }
    }
}
