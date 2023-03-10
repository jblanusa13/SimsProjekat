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
    public class AccommodationController
    {
        private AccommodationDAO accommodations;

        public AccommodationController()
        {
            accommodations = new AccommodationDAO();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return accommodations.GetAll();
        }

        public void Create(Accommodation accommodation)
        {
            accommodations.Add(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            accommodations.Remove(accommodation);
        }

        public void Update(Accommodation accommodation)
        {
            accommodations.Update(accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            accommodations.Subscribe(observer);
        }
    }
}
