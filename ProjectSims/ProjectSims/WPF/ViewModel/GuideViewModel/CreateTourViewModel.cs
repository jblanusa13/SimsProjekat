﻿using ProjectSims.Domain.Model;
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
        private NotificationTourService notificationTourService;
        private List<int> lastAddedTours;

        public TourRequest TourRequest { get; set; }
        public Guide Guide { get; set; }
        public CreateTourViewModel(Guide guide,TourRequest tourRequest)
        {
            tourService = new TourService();
            tourRequestService = new TourRequestService();
            keyPointService = new KeyPointService();
            guideScheduleService = new GuideScheduleService();
            notificationTourService = new NotificationTourService();
            lastAddedTours = new List<int>();
            Guide = guide;
            TourRequest = tourRequest;
        }
        public KeyPoint CreateAndReturnKeyPoint(string name,KeyPointType type)
        {
            KeyPoint keyPoint = new KeyPoint(keyPointService.NextId(),name,type);
            keyPointService.Create(keyPoint);
            return keyPoint;
        }
        public void CreateTour(string name,string language,string location,string maxNumberGuests,
                List<string> appointments,string startKeyPoint,List<string> otherKeyPoints,string finishKeyPoint,
                string description,List<string> images, bool createdByLocation, bool createdByLanguage) 
        {
            foreach(string appointment in appointments)
            {
                List<KeyPoint> keyPoints = new List<KeyPoint>();
                keyPoints.Add(CreateAndReturnKeyPoint(startKeyPoint, KeyPointType.First));
                foreach (string keyPointName in otherKeyPoints)
                {
                    keyPoints.Add(CreateAndReturnKeyPoint(keyPointName, KeyPointType.Intermediate));
                }
                keyPoints.Add(CreateAndReturnKeyPoint(finishKeyPoint, KeyPointType.Last));
                DateTime start = DateTime.ParseExact(appointment.Split("-")[0], "MM/dd/yyyy H:m", CultureInfo.InvariantCulture);
                double duration = Convert.ToDouble(appointment.Split("-")[1]);
                Tour tour = new Tour(-1, Guide.Id, name, location, description, language, Convert.ToInt32(maxNumberGuests), keyPoints.Select(k => k.Id).ToList(), start, duration, images, Convert.ToInt32(maxNumberGuests), TourState.Inactive, -1);
                int lastAddedTour = tourService.NextId();
                lastAddedTours.Add(lastAddedTour);
                tourService.Create(tour);
                guideScheduleService.Create(new GuideSchedule(-1, Guide.Id, tour.Id, start, start.AddHours(duration)));
            }
            SendNotificationIfTourCreatedByMostWantedLanugageAllGuests(createdByLanguage, language);
            if(TourRequest != null)
            {
                TourRequest.State = TourRequestState.Accepted;
                TourRequest.GuideId = Guide.Id;
                tourRequestService.Update(TourRequest);
                string content = "Obavjestenje o novim turama";
                NotificationTour notification = new NotificationTour(-1, TourRequest.Guest2Id, TourRequest.GuideId,
                    lastAddedTours, content, DateTime.Now, false);
                notificationTourService.Create(notification);
            }
        }
        public void SendNotificationIfTourCreatedByMostWantedLanugageAllGuests(bool CreatedByLanguage,string language)
        {
            if (CreatedByLanguage != false)
            {
                List<TourRequest> requests = tourRequestService.GetAllUnrealizedRequestsToLanguage(language);
                List<int> guest2Ids = tourRequestService.GetAllGuest2Ids(requests);
                foreach (int guest2Id in guest2Ids)
                {
                    string content = "Obavjestenje o novim turama koju je vodic kreirao spram statistike o zahtjevima(najtrazeniji jezik).";
                    NotificationTour notification = new NotificationTour(-1, guest2Id, Guide.Id,
                        lastAddedTours, content, DateTime.Now, false);
                    notificationTourService.Create(notification);
                }
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
