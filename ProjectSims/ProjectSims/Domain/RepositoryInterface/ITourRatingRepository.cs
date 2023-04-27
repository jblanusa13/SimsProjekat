using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Observer;
using ProjectSims.Repository;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface ITourRatingRepository : IGenericRepository<TourAndGuideRating, int>
    {
        public List<TourAndGuideRating> GetAllRatingsByTour(Tour tour);
        public int GetNextId();
    }
}
