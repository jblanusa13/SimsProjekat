using ProjectSims.Service;
using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.View;
using ProjectSims.View.Guest1View;
using ProjectSims.View.GuideView;
using ProjectSims.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using ProjectSims.WPF.View.Guest2View;
using ProjectSims.WPF.View.OwnerView;

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserService userService;
        private OwnerService ownerService;
        private Guest1Service guest1Service;
        private Guest2Service guest2Service;
        private GuideService guideService;
        public MainWindow()
        {
            InitializeComponent();

            userService = new UserService();
            ownerService = new OwnerService();
            guest1Service = new Guest1Service();
            guest2Service = new Guest2Service();
            guideService = new GuideService();

            UsernameTextBox.Focus();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = userService.GetByUsername(UsernameTextBox.Text);
            if (user != null)
            {
                if (user.Password == PasswordTextBox.Password)
                {
                    Owner owner = ownerService.GetOwnerByUserId(user.Id);
                    if(owner != null)
                    {
                        OwnerStartingView ownerView = new OwnerStartingView(owner);
                        ownerView.Show();
                        Close();
                    }

                    Guest1 guest1 = guest1Service.GetGuestByUserId(user.Id);
                    if(guest1 != null)
                    {
                        Guest1StartView guest1View = new Guest1StartView(guest1);
                        guest1View.Show();
                        Close();
                    }

                    Guide guide = guideService.GetGuideByUserId(user.Id);
                    if(guide!= null)
                    {
                        GuideStartingView startingView = new GuideStartingView(guide);
                        startingView.Show();
                        Close();
                    }

                    Guest2 guest2 = guest2Service.GetGuestByUserId(user.Id);
                    if(guest2 != null)
                    {
                        Guest2StartingView guest2View = new Guest2StartingView(guest2);
                        guest2View.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
        }
    }
}
