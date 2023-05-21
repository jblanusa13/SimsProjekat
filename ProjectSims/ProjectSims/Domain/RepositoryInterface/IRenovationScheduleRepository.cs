using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IRenovationScheduleRepository : IGenericRepository<RenovationSchedule, int>, ISubject
    {
        public DateOnly FindMaxDate(List<DateOnly> dates);
        public List<DateOnly> GetPassedRenovationDatesforAccommodation(int accommodationId);
        public List<RenovationSchedule> GetPassedAndFutureRenovationsByOwner(int ownerId);
    }
}
