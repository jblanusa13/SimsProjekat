using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IGuest1Repository : IGenericRepository<Guest1, int>, ISubject
    {
        public Guest1 GetByUserId(int userId);
    }
}
