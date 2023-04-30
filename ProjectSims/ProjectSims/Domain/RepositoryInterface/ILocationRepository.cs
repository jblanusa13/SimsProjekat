using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface ILocationRepository : IGenericRepository<Location, int>
    {
        public int GetLocationId(string location);
        public int Add(string location);
        public bool Exist(string location);
    }
}
