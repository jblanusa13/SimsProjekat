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
        private IAccommodationScheduleRepository scheduleRepository;

        public AccommodationService()
        {
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            scheduleRepository = Injector.CreateInstance<IAccommodationScheduleRepository>();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return accommodationRepository.GetAll();
        }

        public List<Accommodation> GetAllAccommodationsForGuestView()
        {
            return GetAllAccommodations();
        }

        public List<Accommodation> GetAllByOwnerId(int ownerId) 
        {
            List<Accommodation> accommodations = new List<Accommodation>();
            foreach (var item in GetAllAccommodations())
            {
                if (item.IdOwner == ownerId)
                {
                    accommodations.Add(item);
                }
            }
            return accommodations;
        }

        public Accommodation GetAccommodation(int id)
        {
            return accommodationRepository.GetById(id);
        }

        public void Create(Accommodation accommodation)
        {
            accommodation.ScheduleId = scheduleRepository.NextId();
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
