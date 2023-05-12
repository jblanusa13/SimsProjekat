using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IGuideScheduleRepository : IGenericRepository<GuideSchedule, int>, ISubject
    {
        public int NextId();
        public List<GuideSchedule> GetByGuideIdAndDate(int id,DateOnly date);
    }
}
