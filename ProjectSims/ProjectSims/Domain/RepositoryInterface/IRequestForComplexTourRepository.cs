using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IRequestForComplexTourRepository : IGenericRepository<RequestForComplexTour, int>, ISubject
    {
        public List<RequestForComplexTour> GetByGuest2Id(int guest2Id);
    }
}
