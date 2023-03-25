using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.Observer;

namespace ProjectSims.ModelDAO
{
    public class AccommodationReservationDAO : ISubject
    {
        private AccommodationReservationFileHandler _reservationFileHandler;
        private List<AccommodationReservation> _reservations;

        private Guest1FileHandler _guest1FileHandler;
        private List<Guest1> _guests;

        private UserFileHandler _userFileHandler;

        private List<DateRanges> _availableDates;
        private List<DateRanges> _unavailableDates;
        private List<DateRanges> _candidatesForDeletion;
        private DateOnly _firstDate;
        private DateOnly _lastDate;

        private readonly List<IObserver> _observers;


        public AccommodationReservationDAO()
        {
            _reservationFileHandler = new AccommodationReservationFileHandler();
            _reservations = _reservationFileHandler.Load();

            _guest1FileHandler = new Guest1FileHandler();
            _guests = _guest1FileHandler.Load();

            _userFileHandler = new UserFileHandler();

            _availableDates = new List<DateRanges>();
            _unavailableDates = new List<DateRanges>();
            _candidatesForDeletion = new List<DateRanges>();

            _observers = new List<IObserver>();

            _firstDate = new DateOnly();
            _lastDate = new DateOnly();
        }

        public User GetUserByUsername(string username)
        {
            return _userFileHandler.GetByUsername(username);
        }

        public Guest1 GetGuestByUsername(string username)
        {
            User user = GetUserByUsername(username);
            return _guests.Find(g => g.UserId == user.Id);       
        }

        public User GetUser(int id)
        {
            return _userFileHandler.Get(id);
        }
        public List<AccommodationReservation> GetAll()
        {
            return _reservations;
        }

        public int NextId()
        {
            return _reservations.Max(r => r.Id) + 1;
        }

        public void Add(int accommodationId, int guestId, DateOnly checkIn, DateOnly checkOut, int guestNumber)
        {
            int id = NextId();
            AccommodationReservation reservation = new AccommodationReservation(id, accommodationId, guestId, checkIn, checkOut, guestNumber);
            _reservations.Add(reservation);
            _reservationFileHandler.Save(_reservations);
        }

        // finds available dates for chosen accommodation in given date range
        public List<DateRanges> FindAvailableDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
            _availableDates.Clear();

            _firstDate = firstDate;
            _lastDate = lastDate;

            FindAllDates(daysNumber);
            FindUnavailableDates(accommodationId);

            bool isFirstBoundaryCase, isInRangeCase, isLastBoundaryCase;
            foreach(DateRanges unavailableDate in _unavailableDates)
            {
                // Date range for search: 15.03-22.03, checkIn = 14.03, checkOut = 17.03
                isFirstBoundaryCase = !IsInRange(unavailableDate.CheckIn, _firstDate, _lastDate) && IsInRange(unavailableDate.CheckOut, _firstDate, _lastDate);

                // Date range for search: 15.03-22.03, checkIn = 15.03, checkOut = 17.03
                isInRangeCase = IsInRange(unavailableDate.CheckIn, _firstDate, _lastDate) && IsInRange(unavailableDate.CheckOut, _firstDate, _lastDate);

                // Date range for search: 15.03-22.03, checkIn = 20.03, checkOut = 25.03
                isLastBoundaryCase = IsInRange(unavailableDate.CheckIn, _firstDate, _lastDate) && !IsInRange(unavailableDate.CheckOut, _firstDate, _lastDate);

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

            foreach (DateRanges dates in _candidatesForDeletion)
            {
                _availableDates.Remove(dates);
            }

            if(_availableDates.Count == 0)
            {
                FindAlternativeDates(daysNumber);
            }
            return _availableDates;
        }

        // checks if date is between firstDate and lastDate
        public bool IsInRange(DateOnly date, DateOnly firstDate, DateOnly lastDate)
        {
            return date >= firstDate && date <= lastDate;
        }

        public void CheckFirstBoundaryCase(DateRanges unavailableDate)
        {
            bool checkOutIsInRange, containsUnavailableDates;

            foreach (DateRanges availableDate in _availableDates)
            {
                checkOutIsInRange = unavailableDate.CheckOut > availableDate.CheckIn && unavailableDate.CheckOut <= availableDate.CheckOut;
                containsUnavailableDates = availableDate.CheckIn < unavailableDate.CheckOut && availableDate.CheckOut < unavailableDate.CheckOut;
                if (checkOutIsInRange || containsUnavailableDates)
                {
                    _candidatesForDeletion.Add(availableDate);
                }
            }

        }

        public void CheckLastBoundaryCase(DateRanges unavailableDate)
        {
            bool checkInIsInRange, containsUnavailableDates;
            foreach (DateRanges availableDate in _availableDates)
            {
                checkInIsInRange = unavailableDate.CheckIn >= availableDate.CheckIn && unavailableDate.CheckIn < availableDate.CheckOut;
                containsUnavailableDates = availableDate.CheckIn > unavailableDate.CheckIn && availableDate.CheckOut > unavailableDate.CheckIn;
                if (checkInIsInRange || containsUnavailableDates)
                {
                    _candidatesForDeletion.Add(availableDate);
                }
            }
        }

        public void CheckIsInRangeCase(DateRanges unavailableDate)
        {
            bool checkInIsInRange, checkOutIsInRange, containsUnavailableDates;
            foreach (DateRanges availableDate in _availableDates)
            {
                checkInIsInRange = unavailableDate.CheckIn >= availableDate.CheckIn && unavailableDate.CheckIn < availableDate.CheckOut;
                checkOutIsInRange = unavailableDate.CheckOut > availableDate.CheckIn && unavailableDate.CheckOut <= availableDate.CheckOut;
                containsUnavailableDates = availableDate.CheckIn > unavailableDate.CheckIn && availableDate.CheckOut < unavailableDate.CheckOut;
                if (checkInIsInRange || checkOutIsInRange || containsUnavailableDates)
                {
                    _candidatesForDeletion.Add(availableDate);
                }
            }
        }

        // calculates all possible dates, for given date range
        public void FindAllDates(int daysNumber)
        {
            DateOnly startDate = _firstDate;
            DateOnly endDate = _firstDate.AddDays(daysNumber);

            while (endDate <= _lastDate) {
            _availableDates.Add(new DateRanges(startDate, endDate));
            startDate = startDate.AddDays(1);
            endDate = endDate.AddDays(1);
            }
        }

        // finds unavailable dates for chosen accommodation in given date range
        public void FindUnavailableDates(int accommodationId)
        {
            foreach(AccommodationReservation reservation in _reservations)
            {
                if(reservation.AccommodationId == accommodationId)
                {
                    _unavailableDates.Add(new DateRanges(reservation.CheckInDate, reservation.CheckOutDate));
                }
            }
        }

        // finds first available dates outside of the given date range
        public void FindAlternativeDates(int daysNumber)
        {
            DateOnly startDateBefore = _firstDate.AddDays(-1);
            DateOnly endDateBefore = startDateBefore.AddDays(daysNumber);

            DateOnly endDateAfter = _lastDate.AddDays(1);
            DateOnly startDateAfter = endDateAfter.AddDays(-daysNumber);

            while (_availableDates.Count() < 4)
            {
                if (IsAlternativeDateAvailable(startDateBefore, endDateBefore))
                {
                    _availableDates.Add(new DateRanges(startDateBefore, endDateBefore));
                }
                startDateBefore = startDateBefore.AddDays(-1);
                endDateBefore = endDateBefore.AddDays(-1);

                if (IsAlternativeDateAvailable(startDateAfter, endDateAfter))
                {
                    _availableDates.Add(new DateRanges(startDateAfter, endDateAfter));
                }
                startDateAfter = startDateAfter.AddDays(1);
                endDateAfter= endDateAfter.AddDays(1);
            }
        }

        // checks if given date ranges are available
        public bool IsAlternativeDateAvailable(DateOnly checkIn, DateOnly checkOut)
        {
            bool checkInIsInRange, checkOutIsInRange, containsUnavailableDates;
            foreach (DateRanges unavailableDate in _unavailableDates)
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
