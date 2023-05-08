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
    public interface ITourRequestRepository : IGenericRepository<TourRequest, int>, ISubject
    {
        public List<TourRequest> GetByGuest2Id(int guest2Id);
        public List<TourRequest> GetWaitingRequests();
        public List<TourRequest> GetByLocation(string location);
        public List<TourRequest> GetByLanguage(string language);
        public List<TourRequest> GetByMaxNumberGuests(int maxNumberGuests);
        public List<TourRequest> GetRequestsInDateRange(DateOnly dateRangeStart, DateOnly dateRangeEnd);
    }
}