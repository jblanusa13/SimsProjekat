using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    interface IGuestRatingRepository : IGenericRepository<GuestRating, int>, ISubject
    {
        public List<GuestRating> GetAllForGuest(int guestId);
        public void ReloadRatingList();
    }
}
