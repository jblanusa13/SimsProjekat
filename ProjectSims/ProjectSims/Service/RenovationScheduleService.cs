using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class RenovationScheduleService
    {
        private IRenovationScheduleRepository renovationScheduleRepository;
        private IAccommodationScheduleRepository accommodationScheduleRepository;
        private IAccommodationRepository accommodationRepository;

        public RenovationScheduleService()
        {
            renovationScheduleRepository = Injector.CreateInstance<IRenovationScheduleRepository>();
            accommodationScheduleRepository = Injector.CreateInstance<IAccommodationScheduleRepository>();
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }

        public RenovationSchedule GetRenovation(int id)
        {
            return renovationScheduleRepository.GetById(id);
        }

        public List<RenovationSchedule> GetAllRenovations()
        {
            return renovationScheduleRepository.GetAll();
        }

        public List<RenovationSchedule> GetPassedAndFutureRenovationsByOwner(int ownerId)
        {
            return renovationScheduleRepository.GetPassedAndFutureRenovationsByOwner(ownerId);  
        }

        public void CreateRenovation(DateRanges dateRange, string description, int accomodationId, Accommodation accommodation)
        {
            int id = renovationScheduleRepository.NextId();
            RenovationSchedule renovation = new RenovationSchedule(id, dateRange, description, accomodationId, accommodation, -1);
            renovationScheduleRepository.Create(renovation);
            AccommodationSchedule schedule = accommodationScheduleRepository.GetById(accommodation.ScheduleId);
            accommodationScheduleRepository.AddUnavailableDate(schedule, dateRange);
            accommodationScheduleRepository.Update(schedule);
        }

        public void UpdateIfRenovated(List<Accommodation> accommodations)
        {
            foreach (var accommodation in accommodations)
            {
                List<DateOnly> dates = renovationScheduleRepository.GetPassedRenovationDatesforAccommodation(accommodation.Id);
                DateOnly dateOfLastRenovation = renovationScheduleRepository.FindMaxDate(dates);

                if (IsRenovatedYearAgo(dateOfLastRenovation) || dateOfLastRenovation == new DateOnly(1, 1, 1))
                {
                    accommodation.Renovated = false;
                }
                else
                {
                    accommodation.Renovated = true;
                }
                accommodationRepository.Update(accommodation);  
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

        public int CalculateDurationForRenovation(RenovationSchedule renovation)
        {
            return renovation.DateRange.CheckOut.DayNumber - renovation.DateRange.CheckIn.DayNumber;
        }

        public bool CanQuitRenovation(RenovationSchedule renovation)
        {
            if (DateOnly.FromDateTime(DateTime.Today).AddDays(5) < renovation.DateRange.CheckIn)
            {
                return true;
            }
            return false;
        }

        public void RemoveRenovation(RenovationSchedule renovation)
        {
            renovationScheduleRepository.Remove(renovation);
            AccommodationSchedule schedule = accommodationScheduleRepository.GetById(renovation.Accommodation.ScheduleId);
            schedule.UnavailableDates.Remove(renovation.DateRange);
            accommodationScheduleRepository.Update(schedule);
        }

        public void Subscribe(IObserver observer)
        {
            accommodationRepository.Subscribe(observer);
        }
    }
}
