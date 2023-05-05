using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Repository
{
    public class GuideScheduleRepository : ISubject, IGuideScheduleRepository
    {
        private GuideScheduleFileHandler guideScheduleFileHandler;
        private List<GuideSchedule> guideSchedules;

        private List<IObserver> observers;

        public GuideScheduleRepository()
        {
            guideScheduleFileHandler = new GuideScheduleFileHandler();
            guideSchedules = guideScheduleFileHandler.Load();
            observers = new List<IObserver>();
        }

        public int NextId()
        {
            return guideSchedules.Max(t => t.Id) + 1;
        }

        public void Create(GuideSchedule guideSchedule)
        {
            guideSchedule.Id = NextId();
            guideSchedules.Add(guideSchedule);
            guideScheduleFileHandler.Save(guideSchedules);
            NotifyObservers();
        }

        public void Remove(GuideSchedule guideSchedule)
        {
            guideSchedules.Remove(guideSchedule);
            guideScheduleFileHandler.Save(guideSchedules);
            NotifyObservers();
        }

        public void Update(GuideSchedule guideSchedule)
        {
            int index = guideSchedules.FindIndex(s => guideSchedule.Id == s.Id);
            if (index != -1)
            {
                guideSchedules[index] = guideSchedule;
            }
            guideScheduleFileHandler.Save(guideSchedules);
            NotifyObservers();
        }
        public List<GuideSchedule> GetAll()
        {
            return guideScheduleFileHandler.Load();
        }
        public GuideSchedule GetById(int id)
        {
            return guideSchedules.Find(s => s.Id == id);
        }
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
