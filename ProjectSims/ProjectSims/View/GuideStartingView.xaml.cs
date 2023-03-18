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
using System.Windows.Shapes;

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for GuideStartingView.xaml
    /// </summary>
    public partial class GuideStartingView : Window
    {
        public GuideStartingView()
        {
            InitializeComponent();
        }
        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            CreateTourView createTourView = new CreateTourView();
            createTourView.Show();
        }
        private void TrackTour_Click(object sender, RoutedEventArgs e)
        {
            TourTrackingView tourTrackingView = new TourTrackingView();
            tourTrackingView.Show();
        }
    }
}
