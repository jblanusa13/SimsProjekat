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
    public class ForumCommentRepository : IForumCommentRepository
    {
        private ForumCommentFileHandler commentFileHandler;
        private List<ForumComment> comments;
        private List<IObserver> observers;
        public ForumCommentRepository()
        {
            commentFileHandler = new ForumCommentFileHandler();
            comments = commentFileHandler.Load();
            observers = new List<IObserver>();
        }
        public List<ForumComment> GetAllByForumId(int forumId)
        {
            return comments.Where(c => c.ForumId == forumId).ToList();
        }
        public List<ForumComment> GetAll()
        {
            return comments;
        }  
        public int NextId()
        {
            return comments.Max(t => t.Id) + 1;
        }

        public void Create(ForumComment entity)
        {
            entity.Id = NextId();
            comments.Add(entity);
            commentFileHandler.Save(comments);
            NotifyObservers();
        }
        public void Update(ForumComment entity)
        {
            int index = comments.FindIndex(guest => guest.Id == entity.Id);
            if (index != -1)
            {
                comments[index] = entity;
            }
            commentFileHandler.Save(comments);
            NotifyObservers();
        }

        public void Remove(ForumComment entity)
        {
            comments.Remove(entity);
            commentFileHandler.Save(comments);
            NotifyObservers();
        }
        public ForumComment GetById(int key)
        {
            return comments.Find(guest => guest.Id == key);
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
