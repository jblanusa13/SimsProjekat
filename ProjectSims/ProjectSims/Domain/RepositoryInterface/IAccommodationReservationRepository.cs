using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IAccommodationReservationRepository : IGenericRepository<AccommodationReservation, int>, ISubject
    {
        public List<AccommodationReservation> GetByGuest(int guestId);
        public AccommodationReservation GetReservation(int guestId, int accommodationId, DateOnly checkInDate, DateOnly checkOutDate);
        public List<AccommodationReservation> GetForGuestInLastYear(int guestId);
    }
}
