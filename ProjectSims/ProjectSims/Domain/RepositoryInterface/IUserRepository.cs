using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        public User GetByUsername(string username);
    }
}
