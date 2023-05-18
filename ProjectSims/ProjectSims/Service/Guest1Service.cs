using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;
using ProjectSims.Serializer;

namespace ProjectSims.Service
{
    public class Guest1Service
    {
        private IGuest1Repository guestRepository;
        private IUserRepository userRepository;
        private ISuperGuestRepository superGuestRepository;
        public Guest1Service()
        {
            guestRepository = Injector.CreateInstance<IGuest1Repository>();
            userRepository = Injector.CreateInstance<IUserRepository>();
            superGuestRepository = Injector.CreateInstance<ISuperGuestRepository>();

            InitializeUser();
            InitializeSuperGuest();
        }

        public Guest1 GetGuestByUserId(int userId)
        {
            return guestRepository.GetByUserId(userId);
        }

        public Guest1 GetGuest1(int id)
        {
            return guestRepository.GetById(id);
        }

        private void InitializeUser()
        {
            foreach (var guest in guestRepository.GetAll())
            {
                guest.User = userRepository.GetById(guest.UserId);
            }
        }
        private void InitializeSuperGuest()
        {
            foreach (var guest in guestRepository.GetAll())
            {
                if (guest.SuperGuestId != -1)
                {
                    guest.SuperGuest = superGuestRepository.GetById(guest.SuperGuestId);
                }
            }
        }
    }
}
