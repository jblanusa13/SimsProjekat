using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using ProjectSims.Serializer;

namespace ProjectSims.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserFileHandler userFileHandler;
        private List<User> users;
        private List<IObserver> observers;
        public UserRepository()
        {
            userFileHandler = new UserFileHandler();
            users = userFileHandler.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            return users.Max(u => u.Id) + 1;
        }
        public void Create(User user)
        {
            user.Id = NextId();
            users.Add(user);
            userFileHandler.Save(users);
            NotifyObservers();
        }
        public void Remove(User user)
        {
            users.Remove(user);
            userFileHandler.Save(users);
            NotifyObservers();
        }
        public void Update(User user)
        {
            int index = users.FindIndex(u => user.Id == u.Id);
            if (index != -1)
            {
                users[index] = user;
            }
            userFileHandler.Save(users);
            NotifyObservers();
        }
        public User GetById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }
        public List<User> GetAll()
        {
            return userFileHandler.Load();
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
