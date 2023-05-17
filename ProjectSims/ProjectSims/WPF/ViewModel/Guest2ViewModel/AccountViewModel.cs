using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
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
        public string AccountUsername { get; set; }
        private UserFileHandler userFile;
        public Guest2 guest2 { get; set; }

        public AccountViewModel(Guest2 g)
        {
            guest2 = g;
            UserNameAndSurname = g.Name + " " + g.Surname;
            userFile = new UserFileHandler();
            AccountUsername = userFile.Get(guest2.UserId).Username;
        }
    }
}
