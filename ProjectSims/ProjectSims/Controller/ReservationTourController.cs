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

        public List<ReservationTour> GetAllTours()
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
