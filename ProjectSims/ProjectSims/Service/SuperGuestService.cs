using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Service
{
    public class SuperGuestService
    {
        private ISuperGuestRepository superGuestRepository;
        private IAccommodationReservationRepository accommodationReservationRepository;
        private IGuest1Repository guest1Repository;
        
        public SuperGuestService()
        {
            superGuestRepository = Injector.CreateInstance<ISuperGuestRepository>();
            accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            guest1Repository = Injector.CreateInstance<IGuest1Repository>();
        }

        public bool HasTenReservations(int guestId)
        {
            int counter = 0;
            foreach(AccommodationReservation reservation in accommodationReservationRepository.GetInLastYear())
            {
                if(reservation.GuestId == guestId)
                {
                    counter++;
                    if(counter >= 10)
                        return true;
                }
            }
            return false;
        }

        public void CreateSuperGuest(Guest1 guest)
        {
            int id = superGuestRepository.NextId();
            SuperGuest superGuest = new SuperGuest(id, 5, DateOnly.FromDateTime(DateTime.Today));
            superGuestRepository.Create(superGuest);
            UpdateSuperGuestForGuest(guest, superGuest, superGuest.Id);
        }

        public void RemoveSuperGuest(Guest1 guest)
        {
            superGuestRepository.Remove(guest.SuperGuest);
            UpdateSuperGuestForGuest(guest, null, -1);
        }

        public void UpdateSuperGuestForGuest(Guest1 guest, SuperGuest superGuest, int superGuestId)
        {
            guest.SuperGuestId = superGuestId;
            guest.SuperGuest = superGuest;
            guest1Repository.Update(guest);
        }
    }
}
