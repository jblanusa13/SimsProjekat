using ProjectSims.Controller;
using ProjectSims.ModelDAO;
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
        private AccommodationDAO AccommodationDao { get; set; }
        private OwnerDAO OwnerDao { get;  set; }

        private readonly AccommodationController accommodationController;
        private readonly OwnerController ownerController;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            AccommodationDao = new AccommodationDAO();
            OwnerDao = new OwnerDAO();

            AccommodationDao.OwnerDao = OwnerDao;

            AccommodationDao.ConnectAccommodationWithOwner();

            ownerController = new OwnerController();
            accommodationController = new AccommodationController();

//            accommodationController.Subscribe(this);
        }

        public void Guest1_Click(object sender, RoutedEventArgs e)
        {
            Guest1View guest1 = new Guest1View();
            guest1.Show();
        }

        public void Owner_Click(object sender, RoutedEventArgs e) 
        {
            AccommodationRegistrationView accommodationRegistrationView = new AccommodationRegistrationView();
            accommodationRegistrationView.Show();
        }
    }
}
