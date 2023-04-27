using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IReservationTourRepository : IGenericRepository<ReservationTour,int>
    {
        public List<ReservationTour> GetReservationsByTour(Tour tour);
        public List<ReservationTour> GetReservationsByTourAndState(Tour tour, Guest2State state);
        public ReservationTour GetReservationByGuestAndTour(Tour tour, Guest2 guest);
        public ReservationTour GetTourIdWhereGuestIsWaiting(Guest2 guest);
        public int NextId();
    }
}
