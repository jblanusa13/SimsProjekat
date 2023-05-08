using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjectSims.Observer;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class FinishedToursRatingsViewModel : IObserver
    {
        private TourService tourService;
        public ObservableCollection<Tour> FinishedTours { get; set; }
        public Guide Guide { get; set; }
        public FinishedToursRatingsViewModel(Guide guide)
        {
            tourService = new TourService();
            tourService.Subscribe(this);
            Guide = guide;
            FinishedTours = new ObservableCollection<Tour>(tourService.GetToursByStateAndGuideId(TourState.Finished, Guide.Id));
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
   
