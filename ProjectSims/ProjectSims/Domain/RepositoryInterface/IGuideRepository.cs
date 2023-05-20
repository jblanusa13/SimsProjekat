using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IGuideRepository : IGenericRepository<Guide, int>, ISubject
    {
        public Guide GetByUserId(int userId);
    }
}
