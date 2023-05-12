using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Repository
{
    class LastShownRepository
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

        public void Add(LastShown lastShownItem)
        {
            lastShownList.Add(lastShownItem);
            lastShownFileHandler.Save(lastShownList);
            NotifyObservers();
        }

        public List<LastShown> GetAll()
        {
            return lastShownList;
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
