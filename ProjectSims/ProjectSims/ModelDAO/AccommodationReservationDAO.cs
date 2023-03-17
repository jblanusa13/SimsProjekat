using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.Observer;

namespace ProjectSims.ModelDAO
{
    public class AccommodationReservationDAO : ISubject
    {
        private AccommodationReservationFileHandler _reservationFileHandler;
        private List<AccommodationReservation> _reservations;
        private List<DateRanges> _availableDates;
        private List<DateRanges> _unavailableDates;
        private DateOnly _firstDate;
        private DateOnly _lastDate;

        private readonly List<IObserver> _observers;

        public AccommodationReservationDAO()
        {
            _reservationFileHandler = new AccommodationReservationFileHandler();
            _reservations = _reservationFileHandler.Load();
            _availableDates = new List<DateRanges>();
            _unavailableDates = new List<DateRanges>();

            _observers = new List<IObserver>();
        }
        public List<AccommodationReservation> GetAll()
        {
            return _reservations;
        }

        // finds available dates for chosen accommodation in given date range
        public List<DateRanges> FindAvailableDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
            _firstDate = firstDate;
            _lastDate = lastDate;

            FindAllDates(daysNumber);
            FindUnavailableDates(accommodationId);

            /*
            List<DateRanges> candidatesForDeletion = new List<DateRanges>();

            bool isFirstBoundaryCase, isInRangeCase, isLastBoundaryCase;
            foreach(DateRanges unavailableDate in _unavailableDates)
            {
                // Date range for search: 15.03-22.02, checkIn = 13.03, checkOut = 17.03
                isFirstBoundaryCase = !IsInRange(unavailableDate.CheckIn, _firstDate, _lastDate) && IsInRange(unavailableDate.CheckOut, _firstDate, _lastDate);

                // Date range for search: 15.03-22.02, checkIn = 15.03, checkOut = 17.03
                isInRangeCase = IsInRange(unavailableDate.CheckIn, _firstDate, _lastDate) && IsInRange(unavailableDate.CheckOut, _firstDate, _lastDate);

                // Date range for search: 15.03-22.02, checkIn = 20.03, checkOut = 25.03
                isLastBoundaryCase = IsInRange(unavailableDate.CheckIn, _firstDate, _lastDate) && !IsInRange(unavailableDate.CheckOut, _firstDate, _lastDate);

                if (isFirstBoundaryCase)
                {
                    foreach(DateRanges availableDate in _availableDates)
                    {
                        if(unavailableDate.CheckOut > availableDate.CheckIn && unavailableDate.CheckOut <= availableDate.CheckOut){
                            candidatesForDeletion.Add(availableDate);
                        }
                    }
                }
                else if (isInRangeCase)
                {
                    bool checkInIsInRange, checkOutIsInRange;
                    foreach (DateRanges availableDate in _availableDates)
                    {
                        checkInIsInRange = unavailableDate.CheckIn >= availableDate.CheckIn && unavailableDate.CheckIn < availableDate.CheckOut;
                        checkOutIsInRange = unavailableDate.CheckOut > availableDate.CheckIn && unavailableDate.CheckOut <= availableDate.CheckOut;
                        if(checkInIsInRange || checkOutIsInRange)
                        {
                            candidatesForDeletion.Add(availableDate);
                        }
                    }
                }
                else if (isLastBoundaryCase)
                {
                    foreach (DateRanges availableDate in _availableDates)
                    {
                        if (unavailableDate.CheckIn >= availableDate.CheckIn && unavailableDate.CheckIn < availableDate.CheckOut)
                        {
                            candidatesForDeletion.Add(availableDate);
                        }
                    }
                }
            }

            foreach (DateRanges dates in candidatesForDeletion)
            {
                _availableDates.Remove(dates);
            }*/
            List<DateRanges> posaljinesto = new List<DateRanges>();
            posaljinesto.Add(new DateRanges(DateOnly.Parse("01.01.2001."), DateOnly.Parse("01.01.2002.")));
            if(_unavailableDates == null)
            {
                return posaljinesto;
            }
            return _unavailableDates;
        }

        // checks if date is between firstDate and lastDate
        public bool IsInRange(DateOnly date, DateOnly firstDate, DateOnly lastDate)
        {
            return date >= firstDate && date <= lastDate;
        }

        // calculates all possible dates, for given date range
        public void FindAllDates(int daysNumber)
        {
            DateOnly startDate = _firstDate;
            DateOnly endDate = _firstDate.AddDays(daysNumber);

            while (endDate <= _lastDate) {
                _availableDates.Add(new DateRanges(startDate, endDate));
                startDate.AddDays(1);
                endDate.AddDays(1);
            }
        }

        // finds unavailable dates for chosen accommodation in given date range
        public void FindUnavailableDates(int accommodationId)
        {
            foreach(AccommodationReservation reservation in _reservations)
            {
                if(reservation.AccommodationId == accommodationId)
                {
                    if(IsUnavailable(reservation.CheckInDate, reservation.CheckOutDate))
                    {
                        _unavailableDates.Add(new DateRanges(reservation.CheckInDate, reservation.CheckOutDate));
                    }
                }
                return;
            }
        }

        // checks if given date ranges are unavailable
        public bool IsUnavailable(DateOnly checkIn, DateOnly checkOut)
        {
            if(IsBefore(checkIn) && IsBefore(checkOut))
            {
                return false;
            }
            else if(IsAfter(checkIn) && IsAfter(checkOut))
            {
                return false;
            }

            return true;

        }
        public bool IsBefore(DateOnly date)
        {
            return date <= _firstDate;
        }

        public bool IsAfter(DateOnly date)
        {
            return date >= _lastDate;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
