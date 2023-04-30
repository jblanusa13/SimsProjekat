using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.WPF.View.OwnerView;

namespace ProjectSims.Repository
{
    class OwnerRepository : IOwnerRepository
    {
        private OwnerFileHandler ownerFileHandler;
        private List<Owner> owners;
        private List<IObserver> observers;

        public OwnerRepository()
        {
            ownerFileHandler = new OwnerFileHandler();
            owners = ownerFileHandler.Load();
            observers = new List<IObserver>();
        }
        public void Create(Owner owner)
        {
            owner.Id = NextId();
            owners.Add(owner);
            ownerFileHandler.Save(owners);
            NotifyObservers();
        }
        
        public int NextId()
        {
            return owners.Max(owner => owner.Id) + 1;
        }

        public void Add(Owner owner)
        {
            owner.Id = NextId();
            owners.Add(owner);
            ownerFileHandler.Save(owners);
            NotifyObservers();
        }

        public void Remove(Owner owner)
        {
            owners.Remove(owner);
            ownerFileHandler.Save(owners);
            NotifyObservers();
        }

        public void Update(Owner owner)
        {
            int index = owners.FindIndex(owner => owner.Id == owner.Id);
            if (index != -1)
            {
                owners[index] = owner;
            }
            ownerFileHandler.Save(owners);
            NotifyObservers();
        }
        public Owner GetById(int id)
        {
            return owners.Find(o => o.Id == id);
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

        public bool ExistAccommodation(Owner owner, int accommodationId) 
        {
            foreach (int id in owner.AccommodationIds)
            {
                if (id == accommodationId)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddAccommodationId(Owner owner, int accommodationId)
        {
            if (!ExistAccommodation(owner, accommodationId))
            {
                owner.AccommodationIds.Add(accommodationId);
                Update(owner);
            }
        }

    }
}