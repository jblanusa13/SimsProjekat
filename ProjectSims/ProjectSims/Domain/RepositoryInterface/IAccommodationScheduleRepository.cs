using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IAccommodationScheduleRepository : IGenericRepository<AccommodationSchedule, int>
    {
        public List<DateRanges> GetUnavailableDates(int accommodationId);
        public void AddUnavailableDate(AccommodationSchedule schedule, DateRanges dateRange);
    }
}
