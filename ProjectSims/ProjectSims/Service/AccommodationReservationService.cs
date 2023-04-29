using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using ProjectSims.FileHandler;
using ProjectSims.WPF.View.Guest1View;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Service
{
    public class AccommodationReservationService
    {
        private IAccommodationReservationRepository reservationRepository;
       // private AccommodationService accommodationService;
       // private LocationService locationService;
       // private Guest1Service guest1Service;
        private RequestService requestService;
        private List<DateRanges> unavailableDates;
        private DateRangesService dateRangesService;

        public AccommodationReservationService()
        {
            reservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            // accommodationService = new AccommodationService();
            // locationService = new LocationService();
            // guest1Service = new Guest1Service();
            requestService = new RequestService();
            unavailableDates = new List<DateRanges>();
            dateRangesService = new DateRangesService();
        }

        public AccommodationReservation GetReservation(int id)
        {
            return reservationRepository.GetById(id);
        }
        /*
                public List<AccommodationReservation> GetReservationByGuest(int guestId)
                {
                    List<AccommodationReservation> reservations = reservationRepository.GetByGuest(guestId);
                    reservations.ForEach(reservation => reservation.Accommodation = accommodationService.GetAccommodation(reservation.AccommodationId));
                    reservations.ForEach(reservation => reservation.Accommodation.Location = locationService.GetLocation(reservation.Accommodation.IdLocation));
                    reservations.ForEach(reservation => reservation.Guest = guest1Service.GetGuest1(guestId));
                    return reservations;
                }*/
        public List<AccommodationReservation> GetReservationByGuest(int guestId)
        {
            return reservationRepository.GetByGuest(guestId);
        }
        public AccommodationReservation GetReservation(int guestId, int accommodationId, DateOnly checkInDate, DateOnly checkOutDate)
        {
            AccommodationReservation reservation = null;
            List<AccommodationReservation> reservations = reservationRepository.GetByGuest(guestId);
            foreach (var item in reservations)
            {
                if (item.CheckInDate==checkInDate && item.CheckOutDate==checkOutDate && item.AccommodationId==accommodationId) 
                {
                    reservation = item;                    
                }
            }
            return reservation;
        }

        public void CreateReservation(int accommodationId, int guestId, DateOnly checkIn, DateOnly checkOut, int guestNumber)
        {
            int id = reservationRepository.NextId();
            AccommodationReservation reservation = new AccommodationReservation(id, accommodationId, guestId, checkIn, checkOut, guestNumber, ReservationState.Active, false);
            reservationRepository.Create(reservation);
        }

        public void Update(AccommodationReservation reservation)
        {
            reservationRepository.Update(reservation);
        }

        public void RemoveReservation(AccommodationReservation reservation)
        {
            reservation.State = ReservationState.Canceled;
            reservationRepository.Update(reservation);

            requestService.UpdateRequestsWhenCancelReservation(reservation);
        }

        public bool CanCancel(AccommodationReservation reservation)
        {
            int dismissalDays = reservation.Accommodation.DismissalDays;
            if(DateOnly.FromDateTime(DateTime.Today) <= reservation.CheckInDate.AddDays(-dismissalDays))
            {
                return true;
            }
            return false;
        }
        public List<AccommodationReservation> GetAccommodationsForRating(Guest1 guest)
        {
            List<AccommodationReservation> guestReservations = new List<AccommodationReservation>();
            List<AccommodationReservation> accommodationsForRating = new List<AccommodationReservation>();


            guestReservations = GetReservationByGuest(guest.Id);
            foreach (AccommodationReservation reservation in guestReservations)
            {
                if(DateOnly.FromDateTime(DateTime.Today) <= reservation.CheckOutDate.AddDays(5) && DateOnly.FromDateTime(DateTime.Today) >= reservation.CheckOutDate && reservation.Rated == false)
                {
                    accommodationsForRating.Add(reservation);
                }
            }

            return accommodationsForRating;
        }

        public void ChangeReservationRatedState(AccommodationReservation reservation)
        {
            reservation.Rated = true;
            reservationRepository.Update(reservation);
        }

        public void Subscribe(IObserver observer)
        {
            reservationRepository.Subscribe(observer);
        }

        public List<AccommodationReservation> GetAllReservations()
        {
            return reservationRepository.GetAll();
        }
        public List<DateRanges> FindUnavailableDates(Request request)
        {
            foreach (AccommodationReservation reservation in reservationRepository.GetAll())
            {
                if (reservation.AccommodationId == GetReservation(request.ReservationId).AccommodationId
                    && reservation.State == ReservationState.Active
                    && reservation.Id != request.ReservationId)
                {
                    unavailableDates.Add(new DateRanges(reservation.CheckInDate, reservation.CheckOutDate));
                }
            }
            return unavailableDates;
        }

        public void SetReserved(Request request)
        {
            foreach (var date in unavailableDates)
            {
                if (IsInRange(request, date.CheckIn, date.CheckOut))
                {
                    request.Reserved = true;
                }
                else
                {
                    request.Reserved = false;
                }
            }
        }

        public bool IsInRange(Request request, DateOnly firstDate, DateOnly lastDate)
        {
            int days = GetReservation(request.ReservationId).CheckOutDate.DayNumber - GetReservation(request.ReservationId).CheckInDate.DayNumber;

            //Requested: 12.03, Vacation days: 5, Reserved: 15.03-19.03.
            for (int i = 0; i < days; i++)
            {
                if (dateRangesService.IsInRange(request.ChangeDate.AddDays(i), firstDate, lastDate))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
