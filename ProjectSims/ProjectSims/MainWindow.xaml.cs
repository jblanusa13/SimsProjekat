using ProjectSims.Service;
using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.View;
using ProjectSims.View.Guest1View;
using ProjectSims.View.Guest2View;
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

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int CurrentUserId { get; set; }
        private readonly UserFileHandler userFile;

        private readonly OwnerFileHandler ownerFile;
        private readonly Guest1FileHandler guest1File;
        private readonly Guest2FileHandler guest2File;
        private readonly GuideFileHandler guideFile;
        public MainWindow()
        {
            InitializeComponent();
            userFile = new UserFileHandler();
            ownerFile = new OwnerFileHandler();
            guest1File = new Guest1FileHandler();
            guest2File = new Guest2FileHandler();
            guideFile = new GuideFileHandler();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = userFile.GetByUsername(UsernameTextBox.Text);
            if (user != null)
            {
                if (user.Password == PasswordTextBox.Password)
                {
                    Owner owner = ownerFile.GetByUserId(user.Id);
                    if(owner != null)
                    {
                        CurrentUserId = owner.UserId;
                        OwnerView ownerView = new OwnerView(owner);
                        ownerView.Show();
                        Close();
                    }

                    Guest1 guest1 = guest1File.GetByUserId(user.Id);
                    if(guest1 != null)
                    {
                        Guest1View guest1View = new Guest1View(guest1);
                        guest1View.Show();
                        Close();
                    }

                    Guide guide = guideFile.GetByUserId(user.Id);
                    if(guide!= null)
                    {
                        GuideStartingView startingView = new GuideStartingView(guide);
                        startingView.Show();
                        Close();
                    }

                    Guest2 guest2 = guest2File.GetByUserId(user.Id);
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
