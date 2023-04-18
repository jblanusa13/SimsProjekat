using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest2View.Pages;
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

namespace ProjectSims.WPF.View.GuideView.Pages
{
    public partial class FinishedToursStatisticsView : Page, IObserver
    {
        private TourService tourService;
        public ObservableCollection<Tour> FinishedTours { get; set; }
        public Tour SelectedTour { get; set; }
        public Tour MostVisitedTour { get; set; }
        public Tour MostVisitedTourThisYear { get; set; }
        public Guide Guide { get; set; }
        public FinishedToursStatisticsView(Guide guide)
        {
            InitializeComponent();
            DataContext = this;
            tourService = new TourService();
            tourService.Subscribe(this);
            Guide = guide;
            FinishedTours = new ObservableCollection<Tour>(tourService.GetToursByStateAndGuideId(TourState.Finished, Guide.Id));
            MostVisitedTour = tourService.GetMostVisitedTour(guide.Id,false);
            MostVisitedTourThisYear = tourService.GetMostVisitedTour(guide.Id, true);
            if(MostVisitedTour != null)
                MostVisitedTourTextBox.Text = MostVisitedTour.Name + "," + MostVisitedTour.StartOfTheTour.ToString("dd/MM/yyyy HH:mm"); 
            if(MostVisitedTourThisYear != null)
                MostVisitedTourThisYearTextBox.Text = MostVisitedTourThisYear.Name + "," + MostVisitedTourThisYear.StartOfTheTour.ToString("dd/MM/yyyy HH:mm"); 
        }
        private void TourInfo_Click(object sender, RoutedEventArgs e)
        {
            SelectedTour = ((FrameworkElement)sender).DataContext as Tour;
            if(SelectedTour != null) 
            { 
                this.NavigationService.Navigate(new TourDetailsAndStatisticsView(SelectedTour));
            }

            
        }
        public void Update()
        {
            FinishedTours.Clear();
            foreach (var tour in tourService.GetToursByStateAndGuideId(TourState.Finished, Guide.Id))
            {
                FinishedTours.Add(tour);
            }
        }

        private void FinishedToursListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
