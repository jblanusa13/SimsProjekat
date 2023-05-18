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
            Page accountPage = new AccountView(Guide);
            GuideFrame.Content = accountPage;
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
            Page createTourPage = new CreateTourView(Guide,null,null,null);
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
            Page finishedTourtourRequestsView = new TourRequestsView(Guide);
            GuideFrame.Content = finishedTourtourRequestsView;
        }
        private void RequestStatistics_Click(object sender, RoutedEventArgs e)
        {
            Page requestStatisticsView = new RequestStatisticsView(Guide);
            GuideFrame.Content = requestStatisticsView;
        }
        public void Update()
        {
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
        }
    }
}
