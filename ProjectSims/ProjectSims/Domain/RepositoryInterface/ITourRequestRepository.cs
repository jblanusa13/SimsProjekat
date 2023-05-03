using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface ITourRequestRepository : IGenericRepository<TourRequest, int>
    {
        public int GetNextId();
        public List<TourRequest> GetWaitingRequests();
        public List<TourRequest> GetByLocation(string location);
        public List<TourRequest> GetByLanguage(string language);
        public List<TourRequest> GetByMaxNumberGuests(int maxNumberGuests);
        public List<TourRequest> GetRequestsInDateRange(DateOnly dateRangeStart, DateOnly dateRangeEnd);
    }
}
