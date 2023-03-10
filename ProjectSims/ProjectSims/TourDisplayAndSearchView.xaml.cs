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

        public  ObservableCollection<Tour> ListTour { get; }
        public Tour SelectedTour { get; set; }
        public TourDisplayAndSearchView()
        {
            InitializeComponent();
            DataContext = this;
            
            tourController = new TourController();
            tourController.Subscribe(this);           
            ListTour = new ObservableCollection<Tour>(tourController.GetAllTours());
            

        }

        private void UpdateTourList()
        {
            ListTour.Clear();
            foreach (var tour in tourController.GetAllTours())
            {
                ListTour.Add(tour);
            }
        }

        public void Update()
        {
            UpdateTourList();
        }

        private void See_More_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                var see_more = new DetailsTourWindow(SelectedTour);
                see_more.Show();
            }
            else
            {
                MessageBox.Show("You must select a tour for more details!");
            }

        }
    }
}
