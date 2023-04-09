using ProjectSims.Domain.Model;
using ProjectSims.WPF.View.GuideView.Pages;
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
using ProjectSims.Service;

namespace ProjectSims.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuideStartingView.xaml
    /// </summary>
    public partial class GuideStartingView : Window
    {
        public Guide guide { get; set; }
        public TourService tourService { get; set; }
        public GuideStartingView(Guide g)
        {
            InitializeComponent();
            guide = g;
            tourService = new TourService();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Window login = new MainWindow();
            login.Show();
        }
        private void TrackTour_Click(object sender, RoutedEventArgs e)
        {
            Tour activeTour = tourService.GetToursByStateAndGuideId(TourState.Active, guide.Id).FirstOrDefault();
            if (activeTour != null)
            {
                Page tourTrackingPage = new TourTrackingView(activeTour,guide);
                GuideFrame.Content = tourTrackingPage;
            }    
        }
        private void AvailableTours_Click(object sender, RoutedEventArgs e)
        {
            Page availableToursPage = new AvailableToursView(guide);
            GuideFrame.Content = availableToursPage;
        }
        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            Page createTourView = new CreateTourView(guide);
            GuideFrame.Content = createTourView;
        }
        private void ScheduledTours_Click(object sender, RoutedEventArgs e)
        {
            Page scheduledToursView = new ScheduledToursView(guide);
            GuideFrame.Content = scheduledToursView;
        }
    }
}
