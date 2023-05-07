using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ProjectSims.Repository;
using System.Windows.Documents;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public partial class CreateTourViewModel 
    {
        private TourService tourService;
        private KeyPointService keyPointService;
        public Guide Guide { get; set; }
        public CreateTourViewModel(Guide guide)
        {
            tourService = new TourService();
            keyPointService = new KeyPointService();
            Guide = guide;
        }
        public int CreateKeyPointAndReturnId(string name,KeyPointType type)
        {
            KeyPoint keyPoint = new KeyPoint(keyPointService.NextId(),name,type);
            keyPointService.Create(keyPoint);
            return keyPoint.Id;
        }
        public void CreateTour(int guideId,string name,string language,string location,string maxNumberGuests,List<DateTime> appointments,string duration,string startKeyPoint,List<string> otherKeyPoints,string finishKeyPoint,string description,List<string> images) 
        {
            foreach(var appointment in appointments)
            {
                List<int> keyPointIds = new List<int>();
                keyPointIds.Add(CreateKeyPointAndReturnId(startKeyPoint, KeyPointType.First));
                foreach (string keyPointName in otherKeyPoints)
                {
                    keyPointIds.Add(CreateKeyPointAndReturnId(keyPointName, KeyPointType.Intermediate));
                }
                keyPointIds.Add(CreateKeyPointAndReturnId(finishKeyPoint, KeyPointType.Last));
                Tour tour = new Tour(-1, guideId, name, location, description, language, Convert.ToInt32(maxNumberGuests), keyPointIds, appointment, Convert.ToDouble(duration), images, Convert.ToInt32(maxNumberGuests), TourState.Inactive, -1);
                tourService.Create(tour);
            }
        }
    }
}
