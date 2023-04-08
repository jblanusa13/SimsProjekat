using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class Guest1Service
    {
        private Guest1Repository repository;
        public Guest1Service()
        {
            repository = new Guest1Repository();
        }

        public Guest1 GetGuest1(int id)
        {
            return repository.Get(id);
        }
    }
}
