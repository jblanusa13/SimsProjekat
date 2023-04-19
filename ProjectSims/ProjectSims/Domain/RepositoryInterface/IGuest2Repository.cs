using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IGuest2Repository
    {
        public void Create(Guest2 guest);
        public void Remove(Guest2 guest);
        public void Update(Guest2 guest);
        public List<Guest2> GetAll();
        public Guest2 GetGuestById(int id);
        public int NextId();

    }
}
