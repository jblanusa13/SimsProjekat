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
    public class OwnerCommentRepository : IOwnerCommentRepository
    {
        private OwnerCommentFileHandler commentFileHandler;
        private List<OwnerComment> comments;
        private List<IObserver> observers;
        public OwnerCommentRepository()
        {
            commentFileHandler = new OwnerCommentFileHandler();
            comments = commentFileHandler.Load();
            observers = new List<IObserver>();
        }
        public List<OwnerComment> GetAllByOwner(int ownerId)
        {
            return comments.Where(c => c.OwnerId == ownerId).ToList();
        }
 
        public List<OwnerComment> GetAll()
        {
            return comments;
        }

        public int NextId()
        {
            return comments.Max(t => t.Id) + 1;
        }

        public void Create(OwnerComment entity)
        {
            entity.Id = NextId();
            comments.Add(entity);
            commentFileHandler.Save(comments);
            NotifyObservers();
        }
        public void Update(OwnerComment entity)
        {
            int index = comments.FindIndex(guest => guest.Id == entity.Id);
            if (index != -1)
            {
                comments[index] = entity;
            }
            commentFileHandler.Save(comments);
            NotifyObservers();
        }

        public void Remove(OwnerComment entity)
        {
            comments.Remove(entity);
            commentFileHandler.Save(comments);
            NotifyObservers();
        }
        public OwnerComment GetById(int key)
        {
            return comments.Find(comment => comment.Id == key);
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
