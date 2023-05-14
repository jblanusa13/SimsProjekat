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
        private IAccommodationScheduleRepository scheduleRepository;
        private IRenovationRepository renovationRepository;

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

        public void UpdateIfRenovated(List<Accommodation> accommodations)
        {
            foreach (var item in accommodations)
            {
                foreach (var range in item.Schedule.UnavailableDates)
                {
                    if (range.CheckOut < DateOnly.FromDateTime(DateTime.Today) && !IsRenovatedYearAgo(range.CheckOut))
                    {
                        item.Renovated = true;
                    }
                    else
                    {
                        item.Renovated = false;
                    }       
                    accommodationRepository.Update(item);
                }
            }
        }

        public bool IsRenovatedYearAgo(DateOnly checkOut)
        {
            if (checkOut.AddYears(1) < DateOnly.FromDateTime(DateTime.Today))
            {
                return true;
            }
            return false;
        }

        public void Subscribe(IObserver observer)
        {
            accommodationRepository.Subscribe(observer);
        }
    }
}
