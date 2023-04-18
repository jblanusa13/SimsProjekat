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

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public partial class FinishedToursStatisticsViewModel : IObserver
    {
        private TourService tourService;
        public ObservableCollection<Tour> FinishedTours { get; set; }
        public Tour MostVisitedTour { get; set; }
        public Tour MostVisitedTourThisYear { get; set; }
        public Guide Guide { get; set; }
        public FinishedToursStatisticsViewModel(Guide guide)
        {
            tourService = new TourService();
            tourService.Subscribe(this);
            Guide = guide;
            FinishedTours = new ObservableCollection<Tour>(tourService.GetToursByStateAndGuideId(TourState.Finished, Guide.Id));
            MostVisitedTour = tourService.GetMostVisitedTour(guide.Id, false);
            MostVisitedTourThisYear = tourService.GetMostVisitedTour(guide.Id, true);
        }
        public void Update()
        {
            FinishedTours.Clear();
            foreach (var tour in tourService.GetToursByStateAndGuideId(TourState.Finished, Guide.Id))
            {
                FinishedTours.Add(tour);
            }
        }
    }
}
