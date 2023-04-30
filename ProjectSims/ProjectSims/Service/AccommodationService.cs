using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Service
{
    public class AccommodationService
    {
        private IAccommodationRepository accommodationRepository;

        public AccommodationService()
        {
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return accommodationRepository.GetAll();
        }

        public List<Accommodation> GetAllAccommodationsForGuestView()
        {
            return GetAllAccommodations();
        }

        public Accommodation GetAccommodation(int id)
        {
            return accommodationRepository.GetById(id);
        }

        public void Create(Accommodation accommodation)
        {
            accommodationRepository.Create(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            accommodationRepository.Remove(accommodation);
        }

        public void Update(Accommodation accommodation)
        {
            accommodationRepository.Update(accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            accommodationRepository.Subscribe(observer);
        }
    }
}
