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
    public interface ITourRepository : IGenericRepository<Tour, int>, ISubject
    {
        public List<Tour> GetToursByStateAndGuideId(TourState state, int guideId);
        public Tour GetTourByStateAndGuideId(TourState state, int guideId);
        public List<Tour> GetToursByDateAndGuideId(DateTime date,int guideId);
        public List<Tour> GetToursByLanguageAndGuideId(string language, int guideId);

    }
}
