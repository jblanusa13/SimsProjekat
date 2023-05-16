using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class UserService
    {
        private IUserRepository userRepository;
        public UserService()
        {
            userRepository = Injector.CreateInstance<IUserRepository>();
        }

        public User GetUser(int id)
        {
            return userRepository.GetById(id);
        }

        public User GetByUsername(string username)
        {
            return userRepository.GetByUsername(username);
        }

        public void Update(User user) 
        {
            userRepository.Update(user);
        }
    }
}
