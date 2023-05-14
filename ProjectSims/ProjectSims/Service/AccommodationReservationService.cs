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
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.View.OwnerView.Pages;

namespace ProjectSims.Service
{
    public class AccommodationReservationService
    {
        private IAccommodationReservationRepository reservationRepository;
        private RequestService requestService;

        public AccommodationReservationService()
        {
            reservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            requestService = new RequestService();
        }
        public List<AccommodationReservation> GetAllReservations()
        {
            return reservationRepository.GetAll();
        }

        public List<AccommodationReservation> GetAllByOwnerId(int id)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (var reservation in GetAllReservations())
            {
                if (reservation.Accommodation.IdOwner == id && reservation.State == ReservationState.Active)
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }  
        
        public List<AccommodationReservation> GetActiveAndCanceledByOwnerId(int ownerId, int accommodationId)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (var reservation in GetAllReservations())
            {
                if (reservation.Accommodation.IdOwner == ownerId && reservation.AccommodationId == accommodationId)
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public int GetAllReservationsByYear(List<AccommodationReservation> reservations, string year)
        {
            int number = 0;
            foreach (AccommodationReservation reservation in reservations)
            {
                if (reservation.CheckInDate.Year.ToString().Equals(year))
                {
                    number++;
                }
            }
            return number;
        }

        public int GetAllShiftedReservationsByYear(List<AccommodationReservation> reservations, string year)
        {
            int number = 0;
            foreach (AccommodationReservation reservation in reservations)
            {
                foreach (Request request in requestService.GetAllRequestByOwner(reservation.Accommodation.IdOwner))
                {
                    if (reservation.CheckInDate.Year.ToString().Equals(year) && reservation.Id == request.ReservationId)
                    {
                        number++;
                    }
                }
            }
            return number;
        }
        
        public int GetAllCanceledReservationsByYear(List<AccommodationReservation> reservations, string year)
        {
            int number = 0;
            foreach (AccommodationReservation reservation in reservations)
            {
                if (reservation.CheckInDate.Year.ToString().Equals(year) && reservation.State == ReservationState.Canceled)
                {
                    number++;
                }
            }
            return number;
        }
        

        public AccommodationReservation GetReservation(int id)
        {
            return reservationRepository.GetById(id);
        }

        public List<AccommodationReservation> GetReservationByGuest(int guestId)
        {
            return reservationRepository.GetByGuest(guestId);
        }
        
        public AccommodationReservation GetReservation(int guestId, int accommodationId, DateOnly checkInDate, DateOnly checkOutDate)
        {
            return reservationRepository.GetReservation(guestId, accommodationId, checkInDate, checkOutDate);
        }

        public void CreateReservation(int accommodationId, int guestId, DateOnly checkIn, DateOnly checkOut, int guestNumber)
        {
            int id = reservationRepository.NextId();
            AccommodationReservation reservation = new AccommodationReservation(id, accommodationId, guestId, checkIn, checkOut, guestNumber, ReservationState.Active, false, false);
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
                if(DateOnly.FromDateTime(DateTime.Today) <= reservation.CheckOutDate.AddDays(5) && DateOnly.FromDateTime(DateTime.Today) >= reservation.CheckOutDate && reservation.RatedAccommodation == false)
                {
                    accommodationsForRating.Add(reservation);
                }
            }
            return accommodationsForRating;
        }

        public void ChangeReservationRatedState(AccommodationReservation reservation)
        {
            reservation.RatedAccommodation = true;
            reservationRepository.Update(reservation);
        }

        public Boolean IsAnyGuestRatable()
        {
            List<AccommodationReservation> reservations = GetAllReservations();

            foreach (var item in reservations)
            {
                if (DateOnly.FromDateTime(DateTime.Today).CompareTo(item.CheckOutDate) > 0 && GetReservation(item.Id).RatedGuest == false)
                {
                    return true;
                }
            }
            return false;
        }

        public void Subscribe(IObserver observer)
        {
            reservationRepository.Subscribe(observer);
        }
    }
}
