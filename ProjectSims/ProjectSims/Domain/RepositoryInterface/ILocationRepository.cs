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
        public int GetIdByLocation(string location);
        public void Add(string location);
        public bool Exist(string location);
    }
}
