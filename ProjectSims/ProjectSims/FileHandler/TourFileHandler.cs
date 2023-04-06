using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class TourFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tour.csv";


        private Serializer<Tour> serializer;

        public TourFileHandler()
        {
            serializer = new Serializer<Tour>();
        }

        public List<Tour> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<Tour> tours)
        {
            serializer.ToCSV(FilePath, tours);
        }
    }
}
