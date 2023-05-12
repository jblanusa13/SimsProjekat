using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IRequestRepository : IGenericRepository<Request, int>, ISubject
    {
        public List<Request> GetAllByGuest(int guestId);
        public List<Request> GetAllByOwner(int ownerId);
        public Request GetByReservationId(int reservationId);
    }
}
