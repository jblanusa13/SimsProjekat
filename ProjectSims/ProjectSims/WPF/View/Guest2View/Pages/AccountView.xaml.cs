using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : Page
    {
        public string UserNameAndSurname { get; set; }
        public string AccountUsername { get; set; }
        private UserService userService;
        public Guest2 guest2 { get; set; }
        public AccountView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            guest2 = g;
            UserNameAndSurname = g.Name + " " + g.Surname;
            userService = new UserService();
            AccountUsername = userService.GetUser(guest2.UserId).Username;
        }
    }
}
