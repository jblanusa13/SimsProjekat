using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Repository
{
    class KeyPointRepository : ISubject, IKeyPointRepository
    {

        private KeyPointFileHandler keyPointFile;
        private List<KeyPoint> keyPoints;
        private List<IObserver> observers;
        public KeyPointRepository()
        {
            keyPointFile = new KeyPointFileHandler();
            keyPoints = keyPointFile.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (keyPoints.Count == 0)
            {
                return 0;
            }
            return keyPoints.Max(k => k.Id) + 1;
        }
        public void Create(KeyPoint keyPoint)
        {
            keyPoints.Add(keyPoint);
            keyPointFile.Save(keyPoints);
            NotifyObservers();
        }
        public void Remove(KeyPoint keyPoint)
        {
            keyPoints.Remove(keyPoint);
            keyPointFile.Save(keyPoints);
            NotifyObservers();
        }
        public void Update(KeyPoint keyPoint)
        {
            int index = keyPoints.FindIndex(k => keyPoint.Id == k.Id);
            if (index != -1)
            {
                keyPoints[index] = keyPoint;
            }
            keyPointFile.Save(keyPoints);
            NotifyObservers();
        }
        public List<KeyPoint> GetAll()
        {
            return keyPoints;
        }
        public KeyPoint GetById(int id)
        {
            return keyPoints.Find(k => k.Id == id);
        }
        public List<KeyPoint> GetKeyPointsByStateAndIds(List<int> ids,bool state)
        {
          return keyPoints.Where(k=> ids.Contains(k.Id) && k.Finished == state).ToList();
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
