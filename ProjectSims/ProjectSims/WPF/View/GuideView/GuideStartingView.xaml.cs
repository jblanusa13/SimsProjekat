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
using ProjectSims.Observer;
using System.Windows.Navigation;
using ProjectSims.WPF.ViewModel.GuideViewModel;

namespace ProjectSims.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuideStartingView.xaml
    /// </summary>
    public partial class GuideStartingView : Window, IObserver
    {
        public TourService tourService { get; set; }
        public Guide Guide { get; set; }
        public Tour ActiveTour { get; set; }
        public GuideStartingView(Guide g)
        {
            InitializeComponent();
            tourService = new TourService();
            Guide = g;
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Account_Click(object sender, RoutedEventArgs e)
        {
            GuideFrame.Content = new GuideAccountView(Guide);
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void TrackTour_Click(object sender, RoutedEventArgs e)
        {
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
            if (ActiveTour != null)
            {
                Page tourTrackingPage = new TourTrackingView(ActiveTour,Guide);
                GuideFrame.Content = tourTrackingPage;
            }
        }
        private void AvailableTours_Click(object sender, RoutedEventArgs e)
        {
            Page availableToursPage = new AvailableToursView(Guide);
            GuideFrame.Content = availableToursPage;
        }
        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            Page createTourPage = new CreateTourView(Guide,null,null,null,new DateTime(0001,1,1),0);
            GuideFrame.Content = createTourPage;
        }
        private void Suggestions_Click(object sender, RoutedEventArgs e)
        {
            Page suggestionsPage = new SuggestionsView(Guide);
            GuideFrame.Content = suggestionsPage;
        }
        private void ScheduledTours_Click(object sender, RoutedEventArgs e)
        {
            Page scheduledToursView = new ScheduledToursView(Guide);
            GuideFrame.Content = scheduledToursView;
        }
        private void FinishedToursStatistics_Click(object sender, RoutedEventArgs e)
        {
            Page finishedToursStatisticsView = new FinishedToursStatisticsView(Guide);
            GuideFrame.Content = finishedToursStatisticsView;
        }
        private void FinishedToursRatings_Click(object sender, RoutedEventArgs e)
        {
            Page finishedToursRatingsView = new FinishedToursRatingsView(Guide);
            GuideFrame.Content = finishedToursRatingsView;
        }
        private void TourRequests_Click(object sender, RoutedEventArgs e)
        {
            Page finishedTourRequestsView = new TourRequestsView(Guide);
            GuideFrame.Content = finishedTourRequestsView;
        }
        private void RequestStatistics_Click(object sender, RoutedEventArgs e)
        {
            Page requestStatisticsView = new RequestStatisticsView(Guide);
            GuideFrame.Content = requestStatisticsView;
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var startView = new MainWindow();
            startView.Show();
            Window.GetWindow(this).Close();
        }
        public void Update()
        {
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
        }
    }
}
