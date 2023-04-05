using ProjectSims.Domain.Model;
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


        private Serializer<TourAndGuideRating> _serializer;

        public TourRatingFileHandler()
        {
            _serializer = new Serializer<TourAndGuideRating>();
        }

        public List<TourAndGuideRating> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourAndGuideRating> toursRating)
        {
            _serializer.ToCSV(FilePath, toursRating);
        }
    }
}
