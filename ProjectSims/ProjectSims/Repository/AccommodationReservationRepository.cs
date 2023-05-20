using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        private AccommodationReservationFileHandler reservationFileHandler;
        private List<AccommodationReservation> reservations;
        private readonly List<IObserver> observers;

        public AccommodationReservationRepository()
        {
            reservationFileHandler = new AccommodationReservationFileHandler();
            reservations = reservationFileHandler.Load();
            observers = new List<IObserver>();
        }
        public List<AccommodationReservation> GetByGuest(int guestId)
        {
            List<AccommodationReservation> guestReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in reservations)
            {
                if (reservation.GuestId == guestId && reservation.State == ReservationState.Active)
                {
                    guestReservations.Add(reservation);
                }
            }
            return guestReservations;
        }

        public AccommodationReservation GetReservation(int guestId, int accommodationId, DateOnly checkInDate, DateOnly checkOutDate)
        {
            AccommodationReservation reservation = null;
            List<AccommodationReservation> reservations = GetByGuest(guestId);
            foreach (var item in reservations)
            {
                if (item.CheckInDate == checkInDate && item.CheckOutDate == checkOutDate && item.AccommodationId == accommodationId)
                {
                    reservation = item;
                }
            }
            return reservation;
        }

        public List<AccommodationReservation> GetInLastYear()
        {
            return reservations.Where(r => (r.CheckInDate >= DateOnly.FromDateTime(DateTime.Today).AddDays(-365))).ToList();
        }

        public List<AccommodationReservation> GetAll()
        {
            return reservations;
        }
        public int NextId()
        {
            if (reservations.Count == 0)
            {
                return 0;
            }
            return reservations.Max(r => r.Id) + 1;
        }
        public void Create(AccommodationReservation entity)
        {
            reservations.Add(entity);
            reservationFileHandler.Save(reservations);
            NotifyObservers();
        }

        public void Update(AccommodationReservation entity)
        {
            int index = reservations.FindIndex(a => entity.Id == a.Id);
            if (index != -1)
            {
                reservations[index] = entity;
            }
            reservationFileHandler.Save(reservations);
            NotifyObservers();
        }

        public void Remove(AccommodationReservation entity)
        {
            reservations.Remove(entity);
            reservationFileHandler.Save(reservations);
            NotifyObservers();
        }

        public AccommodationReservation GetById(int key)
        {
            return reservations.Find(r => r.Id == key);
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
