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
        private IOwnerRepository ownerRepository;
        private IRequestRepository requestRepository;
        private IAccommodationReservationRepository reservationRepository;

        public OwnerService()
        {
            ownerRepository = Injector.CreateInstance<IOwnerRepository>();
            requestRepository = Injector.CreateInstance<IRequestRepository>();
            reservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();

            InitializeReservation();
        }

        private void InitializeReservation()
        {
            foreach(Request request in requestRepository.GetAll())
            {
                request.Reservation = reservationRepository.GetById(request.ReservationId);
            }
        }

        public Owner GetOwnerByUserId(int userId)
        {
            return ownerRepository.GetByUserId(userId);
        }

        public List<Owner> GetAllOwners()
        {
            return ownerRepository.GetAll();
        }

        public void AddAccommodation(Owner owner, int accommodationId)
        {
            ownerRepository.AddAccommodation(owner, accommodationId);
        }
        public void RemoveAccommodation(Owner owner, int accommodationId)
        {
            ownerRepository.RemoveAccommodation(owner, accommodationId);
        }

        public void Create(Owner owner)
        {
            ownerRepository.Create(owner);
        }

        public void Delete(Owner owner)
        {
            ownerRepository.Remove(owner);
        }

        public void Update(Owner owner)
        {
            ownerRepository.Update(owner);
        }

        public void Subscribe(IObserver observer)
        {
            ownerRepository.Subscribe(observer);
        }
    }
}
