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
        public UserRepository()
        {
            userFileHandler = new UserFileHandler();
            users = userFileHandler.Load();
        }
        public User GetByUsername(string username)
        {
            return users.FirstOrDefault(u => u.Username == username);
        }
        public int NextId()
        {
            if (users.Count == 0)
            {
                return 0;
            }
            return users.Max(u => u.Id) + 1;
        }
        public void Create(User user)
        {
            user.Id = NextId();
            users.Add(user);
            userFileHandler.Save(users);
        }
        public void Remove(User user)
        {
            users.Remove(user);
            userFileHandler.Save(users);
        }
        public void Update(User user)
        {
            int index = users.FindIndex(u => user.Id == u.Id);
            if (index != -1)
            {
                users[index] = user;
            }
            userFileHandler.Save(users);
        }
        public User GetById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }
        public List<User> GetAll()
        {
            return userFileHandler.Load();
        }
    }
}
