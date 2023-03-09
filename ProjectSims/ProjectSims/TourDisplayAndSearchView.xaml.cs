using ProjectSims.Controller;
using ProjectSims.Model;
using ProjectSims.Observer;
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

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for TourDisplayAndSearchView.xaml
    /// </summary>
    public partial class TourDisplayAndSearchView : Window , IObserver
    {
        private TourController tourController;

        public ObservableCollection<Tour> ListTour;
        public TourDisplayAndSearchView()
        {
            InitializeComponent();
            DataContext = this;
            /*
            tourController = new TourController();
            tourController.Subscribe(this);
            ListTour = new ObservableCollection<Tour>(tourController.GetAllTours());
            */

        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
