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

        public UserFileHandler()
        {
            _serializer = new Serializer<User>();
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<User> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}
