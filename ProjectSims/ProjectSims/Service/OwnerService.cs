using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Serializer;

namespace ProjectSims.Service
{
    public class OwnerService
    {
        private IOwnerRepository owners;
        private RequestService requestService;

        public OwnerService()
        {
            owners = Injector.CreateInstance<IOwnerRepository>();
            requestService = new RequestService();
        }

        public Owner GetOwnerByUserId(int userId)
        {
            return owners.GetByUserId(userId);
        }

        public List<Owner> GetAllOwners()
        {
            return owners.GetAll();
        }

        public bool HasWaitingRequests(int ownerId)
        {
            foreach(Request request in requestService.GetAllRequestByOwner(ownerId))
            {
                if (request.State.Equals(RequestState.Waiting))
                {
                    return true;
                }
            }
            return false;
        }
        public void AddAccommodation(Owner owner, int accommodationId)
        {
            owners.AddAccommodation(owner, accommodationId);
        }

        public void Create(Owner owner)
        {
            owners.Create(owner);
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
