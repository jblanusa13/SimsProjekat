using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class RenovationRecommendationService
    {
        private RenovationRecommendationRepository recommendationRepository;

        public RenovationRecommendationService()
        {
            recommendationRepository = new RenovationRecommendationRepository();
        }

        public RenovationRecommendation GetRecommendation(int id)
        {
            return recommendationRepository.Get(id);
        }

        public List<RenovationRecommendation> GetAllRecommendations()
        {
            return recommendationRepository.GetAll();
        }
        public int NextId()
        {
            return recommendationRepository.NextId();
        }

        public void CreateRecommendation(int renovationUrgency, string recommendations)
        {
            int id = NextId();
            RenovationRecommendation recommendation = new RenovationRecommendation(id, renovationUrgency, recommendations);
            recommendationRepository.Add(recommendation);
        }

        public RenovationRecommendation GetNewRecommendation(int renovationUrgency, string recommendations)
        {
            CreateRecommendation(renovationUrgency, recommendations);
            return GetAllRecommendations().LastOrDefault();
        }

    }
}
