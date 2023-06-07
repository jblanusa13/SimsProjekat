using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;

namespace ProjectSims.Repository
{
    public class ForumRepository : IForumRepository
    {
        private ForumFileHandler forumFileHandler;
        private List<Forum> forums;
        private List<IObserver> observers;
        public ForumRepository()
        {
            forumFileHandler = new ForumFileHandler();
            forums = forumFileHandler.Load();
            observers = new List<IObserver>();
        }
        public List<Forum> GetAll()
        {
            return forums;
        }

        public int NextId()
        {
            return forums.Max(t => t.Id) + 1;
        }

        public void Create(Forum entity)
        {
            entity.Id = NextId();
            forums.Add(entity);
            forumFileHandler.Save(forums);
            NotifyObservers();
        }
        public void Update(Forum entity)
        {
            int index = forums.FindIndex(guest => guest.Id == entity.Id);
            if (index != -1)
            {
                forums[index] = entity;
            }
            forumFileHandler.Save(forums);
            NotifyObservers();
        }

        public void Remove(Forum entity)
        {
            forums.Remove(entity);
            forumFileHandler.Save(forums);
            NotifyObservers();
        }
        public Forum GetById(int key)
        {
            return forums.Find(guest => guest.Id == key);
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
