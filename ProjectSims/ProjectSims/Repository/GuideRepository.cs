using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Repository
{
    public class GuideRepository : ISubject, IGuideRepository
    {
        private GuideFileHandler guideFileHandler;
        private List<Guide> guides;

        private List<IObserver> observers;

        public GuideRepository()
        {
            guideFileHandler = new GuideFileHandler();
            guides = guideFileHandler.Load();
            observers = new List<IObserver>();
        }

        public int NextId()
        {
            return guides.Max(t => t.Id) + 1;
        }

        public void Create(Guide guide)
        {
            guide.Id = NextId();
            guides.Add(guide);
            guideFileHandler.Save(guides);
            NotifyObservers();
        }

        public void Remove(Guide guide)
        {
            guides.Remove(guide);
            guideFileHandler.Save(guides);
            NotifyObservers();
        }

        public void Update(Guide guide)
        {
            int index = guides.FindIndex(g => guide.Id == g.Id);
            if (index != -1)
            {
                guides[index] = guide;
            }
            guideFileHandler.Save(guides);
            NotifyObservers();
        }
        public List<Guide> GetAll()
        {
            return guideFileHandler.Load();
        }
        public Guide GetById(int id)
        {
            return guides.Find(guide => guide.Id == id);
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
