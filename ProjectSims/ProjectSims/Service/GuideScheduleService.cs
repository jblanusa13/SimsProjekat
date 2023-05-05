using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class GuideScheduleService
    {
        private IGuideScheduleRepository guideScheduleRepository;

        public GuideScheduleService()
        {
            guideScheduleRepository = Injector.CreateInstance<IGuideScheduleRepository>();
        }

        public List<GuideSchedule> GetAll()
        {
            return guideScheduleRepository.GetAll();
        }
        public GuideSchedule GetById(int id)
        {
            return guideScheduleRepository.GetById(id);
        }
        public void Create(int guideId,int tourId,DateTime start,double duration)
        {           
            guideScheduleRepository.Create(new GuideSchedule(-1,guideId,tourId,start,start.AddHours(duration)));
        }

        public void Delete(GuideSchedule guideSchedule)
        {
            guideScheduleRepository.Remove(guideSchedule);
        }

        public void Update(GuideSchedule guideSchedule)
        {
            guideScheduleRepository.Update(guideSchedule);
        }

        public void Subscribe(IObserver observer)
        {
            guideScheduleRepository.Subscribe(observer);
        }
    }
}
