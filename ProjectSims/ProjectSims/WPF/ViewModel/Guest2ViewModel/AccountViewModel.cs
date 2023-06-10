using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class AccountViewModel:IObserver
    {
        public string UserNameAndSurname { get; set; }
        public string Username { get; set; }
        public Guest2 guest2 { get; set; }

        private Guest2Service guest2Service;
        private UserService userService;

        public AccountViewModel(Guest2 g)
        {
            guest2Service = new Guest2Service();
            guest2Service.Subscribe(this);
            userService = new UserService();
            guest2 = g;
            UserNameAndSurname = g.Name + " " + g.Surname;
            Username = userService.GetUser(guest2.UserId).Username;
        }

        public void Update()
        {
            guest2 = guest2Service.GetGuestById(guest2.Id);
            UserNameAndSurname = guest2.Name + " " + guest2.Surname;
        }
    }
}
