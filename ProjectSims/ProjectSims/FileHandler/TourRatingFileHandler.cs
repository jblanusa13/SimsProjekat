using ProjectSims.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class TourRatingFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourrating.csv";


        private Serializer<TourAndGuideRating> serializer;

        public TourRatingFileHandler()
        {
            serializer = new Serializer<TourAndGuideRating>();
        }

        public List<TourAndGuideRating> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<TourAndGuideRating> toursRating)
        {
            serializer.ToCSV(FilePath, toursRating);
        }
    }
}
