using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class RenovationRecommendationService
    {
        private IRenovationRecommendationRepository recommendationRepository;
        private IAccommodationRatingRepository accommodationRatingRepository;


        public RenovationRecommendationService()
        {
            recommendationRepository = Injector.CreateInstance<IRenovationRecommendationRepository>();
            accommodationRatingRepository = Injector.CreateInstance<IAccommodationRatingRepository>();
        }

        public RenovationRecommendation GetRecommendation(int id)
        {
            return recommendationRepository.GetById(id);
        }

        public void CreateRecommendation(int renovationUrgency, string recommendations)
        {
            int id = recommendationRepository.NextId();
            RenovationRecommendation recommendation = new RenovationRecommendation(id, renovationUrgency, recommendations);
            recommendationRepository.Create(recommendation);
        }

        public RenovationRecommendation GetNewRecommendation(int renovationUrgency, string recommendations)
        {
            CreateRecommendation(renovationUrgency, recommendations);
            return recommendationRepository.GetAll().LastOrDefault();
        }
    }
}
