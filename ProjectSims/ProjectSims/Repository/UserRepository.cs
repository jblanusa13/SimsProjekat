using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.Serializer;

namespace ProjectSims.Repository
{
    public class UserRepository
    {
        private UserFileHandler userFileHandler;
        private List<User> users;
        public UserRepository()
        {
            userFileHandler = new UserFileHandler();
            users = userFileHandler.Load();
        }

        public User Get(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }
    }
}
