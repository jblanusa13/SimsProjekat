using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class OwnerService
    {
        private OwnerRepository owners;

        public OwnerService()
        {
            owners = new OwnerRepository();
        }

        public List<Owner> GetAllOwners()
        {
            return owners.GetAll();
        }

        public void Create(Owner owner)
        {
            owners.Add(owner);
        }

        public void Delete(Owner owner)
        {
            owners.Remove(owner);
        }

        public void Update(Owner owner)
        {
            owners.Update(owner);
        }

        public void Subscribe(IObserver observer)
        {
            owners.Subscribe(observer);
        }
    }
}
