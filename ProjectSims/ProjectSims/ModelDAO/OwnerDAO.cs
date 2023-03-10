using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.ModelDAO
{
    class OwnerDAO : ISubject
    {
        private OwnerFileHandler ownerFile;
        private List<Owner> owners;

        private List<IObserver> observers;

        public OwnerDAO()
        {
            ownerFile = new OwnerFileHandler();
            owners = ownerFile.Load();
            observers = new List<IObserver>();
        }

        public int NextId()
        {
            return owners.Max(t => t.Id) + 1;
        }

        public void Add(Owner owner)
        {
            owner.Id = NextId();
            owners.Add(owner);
            ownerFile.Save(owners);
            NotifyObservers();
        }

        public void Remove(Owner owner)
        {
            owners.Remove(owner);
            ownerFile.Save(owners);
            NotifyObservers();
        }

        public void Update(Owner owner)
        {
            int index = owners.FindIndex(o => owner.Id == o.Id);
            if (index != -1)
            {
                owners[index] = owner;
            }
            ownerFile.Save(owners);
            NotifyObservers();
        }


        public List<Owner> GetAll()
        {
            return owners;
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
