using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class DateRangesService
    {
        private List<DateRanges> availableDates;
        private List<DateRanges> unavailableDates;
        private List<DateRanges> candidatesForDeletion;

        private DateOnly firstDate;
        private DateOnly lastDate;
        
        public DateRangesService()
        {
            availableDates = new List<DateRanges>();
            unavailableDates = new List<DateRanges>();
            candidatesForDeletion = new List<DateRanges>();

            firstDate = new DateOnly();
            lastDate = new DateOnly(); 
        }

        // finds available dates for chosen accommodation in given date range
        public List<DateRanges> FindAvailableDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
            availableDates.Clear();

            this.firstDate = firstDate;
            this.lastDate = lastDate;

            FindAllDates(daysNumber);
            FindUnavailableDates(accommodationId);

            bool isFirstBoundaryCase, isInRangeCase, isLastBoundaryCase;
            foreach (DateRanges unavailableDate in unavailableDates)
            {
                // Date range for search: 15.03-22.03, checkIn = 14.03, checkOut = 17.03
                isFirstBoundaryCase = !IsInRange(unavailableDate.CheckIn, firstDate, lastDate) && IsInRange(unavailableDate.CheckOut, firstDate, lastDate);

                // Date range for search: 15.03-22.03, checkIn = 15.03, checkOut = 17.03
                isInRangeCase = IsInRange(unavailableDate.CheckIn, firstDate, lastDate) && IsInRange(unavailableDate.CheckOut, firstDate, lastDate);

                // Date range for search: 15.03-22.03, checkIn = 20.03, checkOut = 25.03
                isLastBoundaryCase = IsInRange(unavailableDate.CheckIn, firstDate, lastDate) && !IsInRange(unavailableDate.CheckOut, firstDate, lastDate);

                if (isFirstBoundaryCase)
                {
                    CheckFirstBoundaryCase(unavailableDate);
                }
                else if (isInRangeCase)
                {
                    CheckIsInRangeCase(unavailableDate);
                }
                else if (isLastBoundaryCase)
                {
                    CheckLastBoundaryCase(unavailableDate);
                }
            }

            foreach (DateRanges dates in candidatesForDeletion)
            {
                availableDates.Remove(dates);
            }

            if (availableDates.Count == 0)
            {
                FindAlternativeDates(daysNumber);
            }
            return availableDates;
        }

        // checks if date is between firstDate and lastDate
        public bool IsInRange(DateOnly date, DateOnly firstDate, DateOnly lastDate)
        {
            return date >= firstDate && date <= lastDate;
        }

        public void CheckFirstBoundaryCase(DateRanges unavailableDate)
        {
            bool checkOutIsInRange, containsUnavailableDates;

            foreach (DateRanges availableDate in availableDates)
            {
                checkOutIsInRange = unavailableDate.CheckOut > availableDate.CheckIn && unavailableDate.CheckOut <= availableDate.CheckOut;
                containsUnavailableDates = availableDate.CheckIn < unavailableDate.CheckOut && availableDate.CheckOut < unavailableDate.CheckOut;
                if (checkOutIsInRange || containsUnavailableDates)
                {
                    candidatesForDeletion.Add(availableDate);
                }
            }

        }

        public void CheckLastBoundaryCase(DateRanges unavailableDate)
        {
            bool checkInIsInRange, containsUnavailableDates;
            foreach (DateRanges availableDate in availableDates)
            {
                checkInIsInRange = unavailableDate.CheckIn >= availableDate.CheckIn && unavailableDate.CheckIn < availableDate.CheckOut;
                containsUnavailableDates = availableDate.CheckIn > unavailableDate.CheckIn && availableDate.CheckOut > unavailableDate.CheckIn;
                if (checkInIsInRange || containsUnavailableDates)
                {
                    candidatesForDeletion.Add(availableDate);
                }
            }
        }

        public void CheckIsInRangeCase(DateRanges unavailableDate)
        {
            bool checkInIsInRange, checkOutIsInRange, containsUnavailableDates;
            foreach (DateRanges availableDate in availableDates)
            {
                checkInIsInRange = unavailableDate.CheckIn >= availableDate.CheckIn && unavailableDate.CheckIn < availableDate.CheckOut;
                checkOutIsInRange = unavailableDate.CheckOut > availableDate.CheckIn && unavailableDate.CheckOut <= availableDate.CheckOut;
                containsUnavailableDates = availableDate.CheckIn > unavailableDate.CheckIn && availableDate.CheckOut < unavailableDate.CheckOut;
                if (checkInIsInRange || checkOutIsInRange || containsUnavailableDates)
                {
                    candidatesForDeletion.Add(availableDate);
                }
            }
        }

        // calculates all possible dates, for given date range
        public void FindAllDates(int daysNumber)
        {
            DateOnly startDate = firstDate;
            DateOnly endDate = firstDate.AddDays(daysNumber);

            while (endDate <= lastDate)
            {
                availableDates.Add(new DateRanges(startDate, endDate));
                startDate = startDate.AddDays(1);
                endDate = endDate.AddDays(1);
            }
        }

        // finds unavailable dates for chosen accommodation in given date range
        public void FindUnavailableDates(int accommodationId)
        {
            AccommodationReservationRepository reservationRepository = new AccommodationReservationRepository();

            foreach (AccommodationReservation reservation in reservationRepository.GetAll())
            {
                if (reservation.AccommodationId == accommodationId && reservation.State == ReservationState.Active)
                {
                    unavailableDates.Add(new DateRanges(reservation.CheckInDate, reservation.CheckOutDate));
                }
            }
        }

        // finds first available dates outside of the given date range
        public void FindAlternativeDates(int daysNumber)
        {
            DateOnly startDateBefore = firstDate.AddDays(-1);
            DateOnly endDateBefore = startDateBefore.AddDays(daysNumber);

            DateOnly endDateAfter = lastDate.AddDays(1);
            DateOnly startDateAfter = endDateAfter.AddDays(-daysNumber);

            while (availableDates.Count() < 4)
            {
                if (IsAlternativeDateAvailable(startDateBefore, endDateBefore))
                {
                    availableDates.Add(new DateRanges(startDateBefore, endDateBefore));
                }
                startDateBefore = startDateBefore.AddDays(-1);
                endDateBefore = endDateBefore.AddDays(-1);

                if (IsAlternativeDateAvailable(startDateAfter, endDateAfter))
                {
                    availableDates.Add(new DateRanges(startDateAfter, endDateAfter));
                }
                startDateAfter = startDateAfter.AddDays(1);
                endDateAfter = endDateAfter.AddDays(1);
            }
        }

        // checks if given date ranges are available
        public bool IsAlternativeDateAvailable(DateOnly checkIn, DateOnly checkOut)
        {
            bool checkInIsInRange, checkOutIsInRange, containsUnavailableDates;
            foreach (DateRanges unavailableDate in unavailableDates)
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
