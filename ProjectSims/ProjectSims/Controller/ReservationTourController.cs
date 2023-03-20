using ProjectSims.Model;
using ProjectSims.ModelDAO;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Controller
{
    public class ReservationTourController
    {
        private ReservationTourDAO reservations;

        public ReservationTourController()
        {
            reservations = new ReservationTourDAO();
        }

        public List<ReservationTour> GetAllReservations()
        {
            return reservations.GetAll();
        }

        public void Create(ReservationTour reservation)
        {
            reservations.Add(reservation);
        }

        public void Delete(ReservationTour reservation)
        {
            reservations.Remove(reservation);
        }
        public List<int> FindGuestIdsByTourId(int tourId)
        {
            List<int> guestIds = new List<int>();
            foreach (ReservationTour reservation in GetAllReservations())
            {
                if (reservation.TourId == tourId)
                {
                    guestIds.Add(reservation.Guest2Id);
                }
            }
            return guestIds;
        }

        public void Update(ReservationTour reserevation)
        {
            reservations.Update(reserevation);
        }

        public void Subscribe(IObserver observer)
        {
            reservations.Subscribe(observer);
        }

    }
}
