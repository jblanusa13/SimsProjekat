using ProjectSims.Controller;
using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.View;
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

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
                        AccommodationRegistrationView accommodationRegistrationView = new AccommodationRegistrationView();
                        accommodationRegistrationView.Show();
                    }

                    Guest1 guest1 = guest1File.GetByUserId(user.Id);
                    if(guest1 != null)
                    {
                        Guest1View guest1View = new Guest1View();
                        guest1View.Show();
                    }

                    Guide guide = guideFile.GetByUserId(user.Id);
                    if(guide!= null)
                    {
                        CreateTourView createTour = new CreateTourView();
                        createTour.Show();
                    }

                    Guest2 guest2 = guest2File.GetByUserId(user.Id);
                    if(guest2 != null)
                    {
                        SearchTourView guest2View = new SearchTourView(guest2);
                        guest2View.Show();
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
