using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSims.ModelDAO
{
    class KeyPointDAO : ISubject
    {

        private KeyPointFileHandler keyPointFile;
        private List<KeyPoint> keyPoints;
        private List<IObserver> observers;
        public KeyPointDAO()
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
        public int Add(KeyPoint keyPoint)
        {
            keyPoint.Id = NextId();
            keyPoints.Add(keyPoint);
            keyPointFile.Save(keyPoints);
            NotifyObservers();
            return keyPoint.Id;
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
