using ProjectSims.Controller;
using ProjectSims.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private readonly GuestAccommodationController _guestAccommodationController;
        public ObservableCollection<GuestAccommodation> GuestAccommodations { get; set; }
        public GuestAccommodation SelectedGuestAccommodation { get; set; }
        public Page1()
        {
            InitializeComponent();
            DataContext = this;

            _guestAccommodationController = new GuestAccommodationController();
            GuestAccommodations = new ObservableCollection<GuestAccommodation>(_guestAccommodationController.GetAllGuestAccommodations());

        }
        
    }
}
