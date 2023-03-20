using ProjectSims.Model;
using ProjectSims.ModelDAO;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Controller
{
    public class GuestAccommodationController
    {
        private GuestAccommodationDAO guestAccommodations;

        public GuestAccommodationController()
        {
            guestAccommodations = new GuestAccommodationDAO();
        }

        public List<GuestAccommodation> GetAllGuestAccommodations()
        {
            return guestAccommodations.GetAll();
        }

        public void Create(GuestAccommodation guestAccommodation)
        {
            guestAccommodations.Add(guestAccommodation);
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
