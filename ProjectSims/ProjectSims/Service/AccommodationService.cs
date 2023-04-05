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
    public class AccommodationService
    {
        private AccommodationRepository accommodations;

        public AccommodationService()
        {
            accommodations = new AccommodationRepository();
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
