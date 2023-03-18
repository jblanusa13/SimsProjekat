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

        private OwnerController ownerController;
        public MainWindow()
        {
            InitializeComponent();
            userFile = new UserFileHandler();
            ownerController = new OwnerController();
        }

        public void Guest1_Click(object sender, RoutedEventArgs e)
        {
            Guest1View guest1 = new Guest1View();
            guest1.Show();
        }
        private void Guest2_Click(object sender, RoutedEventArgs e)
        {
            SearchTourView guest2 = new SearchTourView();
            guest2.Show();
        }

        private void Guide(object sender, RoutedEventArgs e)
        {
            CreateTourView createTour = new CreateTourView();
            createTour.Show();
        }
        private void Owner_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRegistrationView accommodationRegistrationView = new AccommodationRegistrationView();
            accommodationRegistrationView.Show();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = userFile.GetByUsername(UsernameTextBox.Text);
            if (user != null)
            {
                if (user.Password == PasswordTextBox.Password)
                {
                    foreach(Owner owner in ownerController.GetAllOwners())
                    {
                        if(user.Id == owner.Id)
                        {
                            AccommodationRegistrationView accommodationRegistrationView = new AccommodationRegistrationView();
                            accommodationRegistrationView.Show();
                        }
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
