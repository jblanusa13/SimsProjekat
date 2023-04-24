using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;

namespace ProjectSims.Repository
{
    public class RenovationRecommendationRepository
    {
        private RenovationRecommendationFileHandler fileHandler;
        private List<RenovationRecommendation> recommendations;

        public RenovationRecommendationRepository()
        {
            fileHandler = new RenovationRecommendationFileHandler();
            recommendations = fileHandler.Load();
        }

        public List<RenovationRecommendation> GetAll()
        {
            return recommendations;
        }

        public RenovationRecommendation Get(int id)
        {
            return recommendations.Find(r => r.Id == id);
        }
        public int NextId()
        {
            if (recommendations.Count == 0)
            {
                return 0;
            }
            return recommendations.Max(r => r.Id) + 1;
        }

        public void Add(RenovationRecommendation recommendation)
        {
            recommendations.Add(recommendation);
            fileHandler.Save(recommendations);
        }
    }
}
