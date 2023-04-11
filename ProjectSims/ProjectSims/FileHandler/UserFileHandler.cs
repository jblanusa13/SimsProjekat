using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class UserFileHandler
    {
        private const string FilePath = "../../../Resources/Data/user.csv";

        private Serializer<User> _serializer;

        private List<User> users;

        public UserFileHandler()
        {
            _serializer = new Serializer<User>();
            //users = _serializer.FromCSV(FilePath);
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<User> users)
        {
            _serializer.ToCSV(FilePath, users);
        }

        public User GetByUsername(string username)
        {
            users = _serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }
        public User Get(int id)
        {
            users = _serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Id == id);
        }
    }
}
