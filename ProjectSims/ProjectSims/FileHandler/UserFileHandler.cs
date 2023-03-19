using ProjectSims.Model;
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
            users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            users = _serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }
    }
}
