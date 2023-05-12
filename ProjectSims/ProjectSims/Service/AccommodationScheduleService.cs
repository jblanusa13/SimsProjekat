using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.WPF.View.OwnerView.Pages;

namespace ProjectSims.Service
{
   public class AccommodationScheduleService
    {
        private IAccommodationScheduleRepository scheduleRepository;
        private DateOnly firstDate;
        private DateOnly lastDate;
        
        public AccommodationScheduleService()
        {
            scheduleRepository = Injector.CreateInstance<IAccommodationScheduleRepository>();
        }

        public List<DateRanges> FindDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
            List<DateRanges> availableDates = new List<DateRanges>();
            this.firstDate = firstDate;
            this.lastDate = lastDate;

            bool isFirstBoundaryCase, isInRangeCase, isLastBoundaryCase, isDateToCheck;
            foreach(var unavailableDate in scheduleRepository.GetUnavailableDates(accommodationId))
            {
                // Date range for search: 15.03-22.03, unavailableCheckIn = 14.03, unavailableCheckOut = 17.03
                isFirstBoundaryCase = !IsInRange(unavailableDate.CheckIn) && IsInRange(unavailableDate.CheckOut);

                // Date range for search: 15.03-22.03, unavailableCheckIn = 15.03, unavailableCheckOut = 17.03
                isInRangeCase = IsInRange(unavailableDate.CheckIn) && IsInRange(unavailableDate.CheckOut);

                // Date range for search: 15.03-22.03, unavailableCheckIn = 20.03, unavailableCheckOut = 25.03
                isLastBoundaryCase = IsInRange(unavailableDate.CheckIn) && !IsInRange(unavailableDate.CheckOut);
                isDateToCheck = isFirstBoundaryCase || isInRangeCase || isLastBoundaryCase;

                if (isDateToCheck)
                {
                    foreach(var dates in CheckAvailableDates(unavailableDate, daysNumber))
                    {
                        availableDates.Add(dates);
                    }
                }
            }

            DateOnly checkIn;
            if(this.firstDate != lastDate)
            {
                while (this.firstDate.AddDays(daysNumber) <= lastDate)
                {
                    checkIn = this.firstDate;
                    availableDates.Add(new DateRanges(this.firstDate, checkIn.AddDays(daysNumber)));
                    this.firstDate = this.firstDate.AddDays(1);
                }

            }
            
            return availableDates;
        }

        public bool IsInRange(DateOnly date)
        {
            return date >= firstDate && date <= lastDate;
        }

        public List<DateRanges> CheckAvailableDates(DateRanges unavailableDate, int daysNumber)
        {
            bool isAvailable;
            List<DateRanges> availableDates = new List<DateRanges>();

            while(firstDate != unavailableDate.CheckOut)
            {
                isAvailable = true;
                for (int i = 0; i < daysNumber; i++)
                {
                    if (firstDate.AddDays(i) >= unavailableDate.CheckIn && firstDate.AddDays(i) <= unavailableDate.CheckOut)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable)
                {
                    availableDates.Add(new DateRanges(firstDate, firstDate.AddDays(daysNumber)));
                }

                firstDate = firstDate.AddDays(1);
            }

            return availableDates;
        }

        public List<DateRanges> FindAlternativeDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
            List<DateRanges> alternativeDates = new List<DateRanges>();

            DateOnly startDateBefore = firstDate.AddDays(-1);
            DateOnly endDateBefore = startDateBefore.AddDays(daysNumber);

            DateOnly endDateAfter = lastDate.AddDays(1);
            DateOnly startDateAfter = endDateAfter.AddDays(-daysNumber);

            while (alternativeDates.Count() < 4)
            {
                if (IsAlternativeDateAvailable(startDateBefore, endDateBefore, accommodationId))
                {
                    alternativeDates.Add(new DateRanges(startDateBefore, endDateBefore));
                }
                startDateBefore = startDateBefore.AddDays(-1);
                endDateBefore = endDateBefore.AddDays(-1);

                if (IsAlternativeDateAvailable(startDateAfter, endDateAfter, accommodationId))
                {
                    alternativeDates.Add(new DateRanges(startDateAfter, endDateAfter));
                }
                startDateAfter = startDateAfter.AddDays(1);
                endDateAfter = endDateAfter.AddDays(1);
            }

            return alternativeDates;
        }

        public bool IsAlternativeDateAvailable(DateOnly checkIn, DateOnly checkOut, int accommodationId)
        {
            bool checkInIsInRange, checkOutIsInRange, containsUnavailableDates;
            foreach (DateRanges unavailableDate in scheduleRepository.GetUnavailableDates(accommodationId))
            {
                checkInIsInRange = unavailableDate.CheckIn >= checkIn && unavailableDate.CheckIn < checkOut;
                checkOutIsInRange = unavailableDate.CheckOut > checkIn && unavailableDate.CheckOut <= checkOut;
                containsUnavailableDates = checkIn > unavailableDate.CheckIn && checkOut < unavailableDate.CheckOut;
                if (checkInIsInRange || checkOutIsInRange || containsUnavailableDates)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
