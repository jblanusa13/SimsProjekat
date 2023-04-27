using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IKeyPointRepository : IGenericRepository<KeyPoint, int>
    {
        public int GetNextId();
        public List<KeyPoint> GetKeyPointsByStateAndIds(List<int> ids, bool state);
    }
}
