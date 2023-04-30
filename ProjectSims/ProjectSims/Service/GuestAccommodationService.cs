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
    public class GuestAccommodationService
    {
        private GuestAccommodationRepository guestAccommodations;

        public GuestAccommodationService()
        {
            guestAccommodations = new GuestAccommodationRepository();
        }

        public List<GuestAccommodation> GetAllGuestAccommodations()
        {
            return guestAccommodations.GetAll();
        }

        public void Create(GuestAccommodation guestAccommodation)
        {
            guestAccommodations.Create(guestAccommodation);
        }

        public void Delete(GuestAccommodation guestAccommodation)
        {
            guestAccommodations.Remove(guestAccommodation);
        }

        public void Update(GuestAccommodation guestAccommodation)
        {
            guestAccommodations.Update(guestAccommodation);
        }

        public void Subscribe(IObserver observer)
        {
            guestAccommodations.Subscribe(observer);
        }
    }
}
