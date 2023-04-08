using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class UserService
    {
        private UserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
        }

        public User GetUser(int id)
        {
            return userRepository.Get(id);
        }
    }
}
