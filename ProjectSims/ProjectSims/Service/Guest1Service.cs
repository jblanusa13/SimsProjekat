using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class Guest1Service
    {
        private IGuest1Repository repository;
        public Guest1Service()
        {
            repository = Injector.CreateInstance<IGuest1Repository>();
        }

        public Guest1 GetGuest1(int id)
        {
            return repository.GetById(id);
        }
    }
}
