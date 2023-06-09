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
    public class LastShownRepository : ILastShownRepository
    {
        private LastShownFileHandler lastShownFileHandler;
        private List<LastShown> lastShownList;
        private List<IObserver> observers;

        public LastShownRepository()
        {
            lastShownFileHandler = new LastShownFileHandler();
            lastShownList = lastShownFileHandler.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            return -1;
        }

        public void Create(LastShown entity)
        {
            lastShownList.Add(entity);
            lastShownFileHandler.Save(lastShownList);
            NotifyObservers();
        }

        public void Update(LastShown entity)
        {
        }

        public void Remove(LastShown entity)
        {
            lastShownList.Remove(entity);
            lastShownFileHandler.Save(lastShownList);
            NotifyObservers();
        }

        public List<LastShown> GetAll()
        {
            return lastShownList;
        }
        public LastShown GetById(int key)
        {
            return GetAll().First();
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
