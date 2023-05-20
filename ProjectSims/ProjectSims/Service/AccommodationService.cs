using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;
using System.Collections.ObjectModel;

namespace ProjectSims.Service
{
    public class AccommodationService
    {
        private IAccommodationRepository accommodationRepository;
        private IAccommodationScheduleRepository accommodationScheduleRepository;
        private ILocationRepository locationRepository;

        public AccommodationService()
        {
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            accommodationScheduleRepository = Injector.CreateInstance<IAccommodationScheduleRepository>();
            locationRepository = Injector.CreateInstance<ILocationRepository>();
            InitializeSchedule();
            InitializeLocation();
        }

        private void InitializeSchedule()
        {
            foreach (Accommodation accommodation in GetAllAccommodations()) 
            {
                accommodation.Schedule = accommodationScheduleRepository.GetById(accommodation.ScheduleId);
            }
        }

        private void InitializeLocation()
        {
            foreach (Accommodation accommodation in GetAllAccommodations())
            {
                accommodation.Location = locationRepository.GetById(accommodation.IdLocation);
            }
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return accommodationRepository.GetAll();
        }

        public List<Accommodation> GetAllAccommodationsForGuestView()
        {
            return GetAllAccommodations();
        }

        public List<Accommodation> GetAccommodationsByOwner(int ownerId) 
        {
            return accommodationRepository.GetAllByOwner(ownerId);
        }

        public Accommodation GetAccommodation(int id)
        {
            return accommodationRepository.GetById(id);
        }

        public void Create(Accommodation accommodation)
        {
            accommodation.ScheduleId = accommodationScheduleRepository.NextId();
            accommodationRepository.Create(accommodation);
            accommodationScheduleRepository.Create(accommodation.Schedule);
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
