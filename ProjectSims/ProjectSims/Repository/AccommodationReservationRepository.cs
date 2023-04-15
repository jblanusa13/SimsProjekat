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

namespace ProjectSims.Repository
{
    public class AccommodationReservationRepository : ISubject
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

        public List<AccommodationReservation> GetAll()
        {
            return reservations;
        }

        public AccommodationReservation Get(int id)
        {
            return reservations.Find(r => r.Id == id);
        }

        public List<AccommodationReservation> GetByGuest(int guestId)
        {
            List<AccommodationReservation> guestReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in reservations)
            {
                if(reservation.GuestId == guestId)
                {
                    guestReservations.Add(reservation);
                }
            }
            return guestReservations;
        }

        public void Add(AccommodationReservation reservation)
        {
            reservations.Add(reservation);
            reservationFileHandler.Save(reservations);
            NotifyObservers();
        }
        public void Update(AccommodationReservation reservation)
        {
            int index = reservations.FindIndex(a => reservation.Id == a.Id);
            if (index != -1)
            {
                reservations[index] = reservation;
            }
            reservationFileHandler.Save(reservations);
            NotifyObservers();          
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
