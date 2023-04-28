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
    }
}
