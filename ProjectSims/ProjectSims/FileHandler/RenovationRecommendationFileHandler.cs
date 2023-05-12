using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    public class RenovationRecommendationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/renovationRecommendation.csv";

        private Serializer<RenovationRecommendation> _serializer;

        public RenovationRecommendationFileHandler()
        {
            _serializer = new Serializer<RenovationRecommendation>();
        }

        public List<RenovationRecommendation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<RenovationRecommendation> recommendations)
        {
            _serializer.ToCSV(FilePath, recommendations);
        }
    }
}
