using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ProjectSims.Observer;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class FinishedToursStatisticsViewModel : IObserver
    {
        private TourService tourService;
       
        public List<Tour> Tours;
        public List<Tour> OtherTours;
        public ObservableCollection<Tour> FinishedTours { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand BackCommand { get; set; }
        public Tour SelectedTour { get; set; }
       public Tour MostVisitedTour { get; set; }
       public Tour MostVisitedTourThisYear { get; set; }
        public Guide Guide { get; set; }
        public FinishedToursStatisticsViewModel(Guide guide,NavigationService navService)
        {
            tourService = new TourService();
            tourService.Subscribe(this);
            Guide = guide;
            NavService = navService;
            MostVisitedTour = tourService.GetMostVisitedTour(guide.Id, false);
            MostVisitedTour.Name = MostVisitedTour.Name;
            MostVisitedTourThisYear = tourService.GetMostVisitedTour(guide.Id, true);
            MostVisitedTourThisYear.Name = MostVisitedTourThisYear.Name;
            Tours = new List<Tour>();
            Tours.Add(MostVisitedTour);
            Tours.Add(MostVisitedTourThisYear);
            OtherTours = tourService.GetToursByStateAndGuideId(TourState.Finished, Guide.Id);
            OtherTours.Remove(tourService.GetMostVisitedTour(guide.Id, false));
            OtherTours.Remove(tourService.GetMostVisitedTour(guide.Id, true));
            Tours.AddRange(OtherTours);
            FinishedTours = new ObservableCollection<Tour>(Tours);
            BackCommand = new RelayCommand(Execute_BackCommand);
        }
        public void Update()
        {
            FinishedTours.Clear();
            Tours.Clear();
            OtherTours.Clear();
            MostVisitedTour = tourService.GetMostVisitedTour(Guide.Id, false);
            MostVisitedTourThisYear = tourService.GetMostVisitedTour(Guide.Id, true);
            Tours.Add(MostVisitedTour);
            Tours.Add(MostVisitedTourThisYear);
            OtherTours = tourService.GetToursByStateAndGuideId(TourState.Finished, Guide.Id);
            OtherTours.Remove(tourService.GetMostVisitedTour(Guide.Id, false));
            OtherTours.Remove(tourService.GetMostVisitedTour(Guide.Id, true));
            Tours.AddRange(OtherTours);
            foreach (var tour in Tours)
            {           
                FinishedTours.Add(tour);
            }
        }
        public void Execute_BackCommand(object obj)
        {
            NavService.Navigate(new HomeView(Guide, NavService));
        }
        public void ViewDetails(Tour tour)
        {
            NavService.Navigate(new TourDetailsAndStatisticsView(tour,NavService,Guide));
        }
    }
}
