using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class RenovationService
    {
        private IRenovationRepository renovationRepository;
        private IAccommodationScheduleRepository accommodationScheduleRepository;

        public RenovationService()
        {
            renovationRepository = Injector.CreateInstance<IRenovationRepository>();
            accommodationScheduleRepository = Injector.CreateInstance<IAccommodationScheduleRepository>();
        }

        public Renovation GetRenovation(int id)
        {
            return renovationRepository.GetById(id);
        }

        public List<Renovation> GetAllRenovations()
        {
            return renovationRepository.GetAll();
        }
        public void CreateRenovation(DateRanges dateRange, string description, int accomodationId, Accommodation accommodation)
        {
            int id = renovationRepository.NextId();
            Renovation renovation = new Renovation(id, dateRange, description, accomodationId, accommodation);
            renovationRepository.Create(renovation);
            List<DateRanges> dateRanges = accommodationScheduleRepository.GetUnavailableDates(accomodationId);
            dateRanges.Add(dateRange);
            AccommodationSchedule schedule = accommodationScheduleRepository.GetById(accommodation.ScheduleId);
            //
            accommodationScheduleRepository.Update(schedule);
        }
    }
}
