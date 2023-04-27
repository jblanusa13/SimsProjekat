using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface ITourRepository : IGenericRepository<Tour, int>
    {
        public List<Tour> GetToursByStateAndGuideId(TourState state, int guideId);
        public Tour GetTourByStateAndGuideId(TourState state, int guideId);
        public List<Tour> GetTodayTours(int guideId);
        public int NextId();

    }
}
