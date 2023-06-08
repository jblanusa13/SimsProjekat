using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Service
{
    public class AnywhereAnytimeService
    {
        private IAccommodationReservationRepository reservationRepository;
        private IAccommodationScheduleRepository accommodationScheduleRepository;
        private IAccommodationRepository accommodationRepository;

        private AccommodationScheduleService scheduleService;
        public AnywhereAnytimeService()
        {
            reservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            accommodationScheduleRepository = Injector.CreateInstance<IAccommodationScheduleRepository>();
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();

            scheduleService = new AccommodationScheduleService();
        }

        private List<Accommodation> FindAvailableAccommodations(DateOnly firstDate, DateOnly lastDate, int daysNumber, int guestNumber)
        {
            List<Accommodation> availableAccommodations = null;
            foreach(Accommodation accommodation in accommodationRepository.GetAll())
            {
                if(daysNumber < accommodation.MinimumReservationDays || guestNumber > accommodation.GuestsMaximum)
                {
                    break;
                }

                if(scheduleService.FindDates(firstDate, lastDate, daysNumber, accommodation.Id).Count == 0)
                {
                    break;
                }

                availableAccommodations.Add(accommodation);
            }

            return availableAccommodations;
        }

        
    }
}
