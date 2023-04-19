using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class Guest2TrackingTourViewModel
    {
        private KeyPointService keyPointService;
        public GuideService guideService;
        public ObservableCollection<KeyPoint> UnFinishedKeyPoints { get; set; }
        public ObservableCollection<KeyPoint> FinishedKeyPoints { get; set; }
        public string CurentlyActiveStation { get; set; }
        public Tour tour { get; set; }
        public Guide guide { get; set; }

        public Guest2TrackingTourViewModel(Tour t)
        {
            tour = t;
            keyPointService = new KeyPointService();
            guideService = new GuideService();
            UnFinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.GetKeyPointsByStateAndIds(tour.KeyPointIds, false));
            FinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.GetKeyPointsByStateAndIds(tour.KeyPointIds, true));
            CurentlyActiveStation = UnFinishedKeyPoints.First().Name;
            guide = guideService.GetGuideById(tour.GuideId);
        }
    }
}
