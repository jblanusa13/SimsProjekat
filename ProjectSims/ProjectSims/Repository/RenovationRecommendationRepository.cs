using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.Repository
{
    public class RenovationRecommendationRepository : IRenovationRecommendationRepository
    {
        private RenovationRecommendationFileHandler recommendationFileHandler;
        private List<RenovationRecommendation> recommendations;

        public RenovationRecommendationRepository()
        {
            recommendationFileHandler = new RenovationRecommendationFileHandler();
            recommendations = recommendationFileHandler.Load();
        }

        public List<RenovationRecommendation> GetAll()
        {
            return recommendations;
        }

        public int NextId()
        {
            if (recommendations.Count == 0)
            {
                return 0;
            }
            return recommendations.Max(r => r.Id) + 1;
        }

        public void Create(RenovationRecommendation entity)
        {
            recommendations.Add(entity);
            recommendationFileHandler.Save(recommendations);
        }

        public void Update(RenovationRecommendation entity)
        {
            int index = recommendations.FindIndex(a => entity.Id == a.Id);
            if (index != -1)
            {
                recommendations[index] = entity;
            }
            recommendationFileHandler.Save(recommendations);
        }

        public void Remove(RenovationRecommendation entity)
        {
            recommendations.Remove(entity);
            recommendationFileHandler.Save(recommendations);
        }

        public RenovationRecommendation GetById(int key)
        {
            return recommendations.Find(r => r.Id == key);
        }
    }
}
