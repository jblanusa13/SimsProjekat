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

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for TourTrackingView.xaml
    /// </summary>
    public partial class TourTrackingView : Window
    {
        private TourController tourController;
        private KeyPointController keyPointController;
        public Tour tour { get; set; }
        public List<KeyPoint> KeyPointList { get; set; }
        public TourTrackingView(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            tour = selectedTour;
            tourController = new TourController();
            keyPointController = new KeyPointController();
            KeyPointList = tourController.GetTourKeyPoints(tour);

            
            TourInfoTextBox.Text = selectedTour.Name + "," + selectedTour.StartOfTheTour.ToString("dd/MM/yyyy HH:mm");
            
        }

    }
}
