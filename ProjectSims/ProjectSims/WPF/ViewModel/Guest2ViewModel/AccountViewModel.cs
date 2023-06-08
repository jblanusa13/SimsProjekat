using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class AccountViewModel
    {
        public string UserNameAndSurname { get; set; }
        public Guest2 guest2 { get; set; }

        private Guest2Service guest2Service;

        public AccountViewModel(Guest2 g)
        {
            guest2Service = new Guest2Service();
            guest2 = g;
            UserNameAndSurname = g.Name + " " + g.Surname;
        }
    }
}
