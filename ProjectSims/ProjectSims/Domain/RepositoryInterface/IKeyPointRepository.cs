using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IKeyPointRepository : IGenericRepository<KeyPoint, int>, ISubject
    {
        public List<KeyPoint> GetKeyPointsByStateAndIds(List<int> ids, bool state);
    }
}
