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
using System.Windows.Shapes;
using ProjectSims.Controller;
using ProjectSims.Model;
using ProjectSims.Observer;

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for Guest1View.xaml
    /// </summary>
    public partial class Guest1View : Window, IObserver
    {
        private readonly AccomodationController _accomodationController;
        public ObservableCollection<Accomodation> Accommodations { get; set; }
        public Accomodation SelectedAccommodation { get; set; }
        
        public Guest1View()
        {
            InitializeComponent();
            DataContext = this;

            _accomodationController = new AccomodationController();
            _accomodationController.Subscribe(this);
            Accommodations = new ObservableCollection<Accomodation>(_accomodationController.GetAllAccommodations());
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach(var accommodation in _accomodationController.GetAllAccommodations())
            {
                Accommodations.Add(accommodation);
            }
        }
    }
}
