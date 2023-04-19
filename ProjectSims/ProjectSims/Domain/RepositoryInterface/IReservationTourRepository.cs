using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IReservationTourRepository
    {
        public void Create(ReservationTour reservation);
        public void Remove(ReservationTour reservation);
        public void Update(ReservationTour reservation);
        public List<ReservationTour> GetAll();
        public int NextId();
    }
}
