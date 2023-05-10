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
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public partial class CreateTourViewModel 
    {
        private TourService tourService;
        private TourRequestService tourRequestService;
        private KeyPointService keyPointService;
        private GuideScheduleService guideScheduleService;
        public TourRequest TourRequest { get; set; }
        public Guide Guide { get; set; }
        public CreateTourViewModel(Guide guide,TourRequest tourRequest)
        {
            tourService = new TourService();
            tourRequestService = new TourRequestService();
            keyPointService = new KeyPointService();
            guideScheduleService = new GuideScheduleService();
            Guide = guide;
            TourRequest = tourRequest;
        }
        public int CreateKeyPointAndReturnId(string name,KeyPointType type)
        {
            KeyPoint keyPoint = new KeyPoint(keyPointService.NextId(),name,type);
            keyPointService.Create(keyPoint);
            return keyPoint.Id;
        }
        public void CreateTour(string name,string language,string location,string maxNumberGuests,List<string> appointments,string startKeyPoint,List<string> otherKeyPoints,string finishKeyPoint,string description,List<string> images) 
        {
            foreach(string appointment in appointments)
            {
                List<int> keyPointIds = new List<int>();
                keyPointIds.Add(CreateKeyPointAndReturnId(startKeyPoint, KeyPointType.First));
                foreach (string keyPointName in otherKeyPoints)
                {
                    keyPointIds.Add(CreateKeyPointAndReturnId(keyPointName, KeyPointType.Intermediate));
                }
                keyPointIds.Add(CreateKeyPointAndReturnId(finishKeyPoint, KeyPointType.Last));
                DateTime start = DateTime.ParseExact(appointment.Split("-")[0], "MM/dd/yyyy H:m", CultureInfo.InvariantCulture);
                double duration = Convert.ToDouble(appointment.Split("-")[1]);
                Tour tour = new Tour(-1, Guide.Id, name, location, description, language, Convert.ToInt32(maxNumberGuests), keyPointIds, start, duration, images, Convert.ToInt32(maxNumberGuests), TourState.Inactive, -1);
                tourService.Create(tour);
                guideScheduleService.Create(new GuideSchedule(-1, Guide.Id, tour.Id, start, start.AddHours(duration)));
            }
            if(TourRequest != null)
            {
                TourRequest.State = TourRequestState.Accepted;
                tourRequestService.Update(TourRequest);
            }
        }
        public bool GuideIsAvailable(DateOnly date,int hour,int minute,double duration)
        {
            DateTime start = new DateTime(date.Year,date.Month,date.Day,hour, minute, 0);
            DateTime end = start.AddHours(duration);
            foreach (var freeAppointment in guideScheduleService.GetFreeAppointmentsForThatDay(Guide.Id,date))
            {
                if ((start >= freeAppointment.Item1) && (start <= freeAppointment.Item2) && (end <= freeAppointment.Item2) && (end <= freeAppointment.Item2))
                    return true;
            }
            return false ;
        }
    }
}
