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
using ProjectSims.Domain.Model;
using ProjectSims.Service;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Guest1 Guest { get; set; }
        private SuperGuestService superGuestService;
        public Profile(Guest1 guest)
        {
            InitializeComponent();
            DataContext = this;
            Guest = guest;
            superGuestService = new SuperGuestService();
            CheckSuperGuest();
        }

        public void CheckSuperGuest()
        {
            if (Guest.SuperGuestId != -1)
            {
                if(Guest.SuperGuest.StartDate < DateOnly.FromDateTime(DateTime.Today).AddDays(-365))
                {
                    superGuestService.RemoveSuperGuest(Guest);
                    NotSuperGuestView();
                }
            }

                if (superGuestService.HasTenReservations(Guest.Id))
            {
                if (Guest.SuperGuestId == -1)
                {
                    superGuestService.CreateSuperGuest(Guest);
                }
                SuperGuestView();
            }

            if (Guest.SuperGuestId != -1)
            {

            }
            NotSuperGuestView();
        }

        public void SuperGuestView()
        {
            CaptionFirst.Content = "Postali ste";
            CaptionSecond.Content = "Super gost!";
        }

        public void NotSuperGuestView()
        {
            CaptionFirst.Content = "Koliko jos da biste postali";
            CaptionSecond.Content = "Super gost";
        }
    }
}
