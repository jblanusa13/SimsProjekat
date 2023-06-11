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
        private GuideService guideService;
        private TourRequestService tourRequestService;
        private KeyPointService keyPointService;
        private NotificationTourService notificationTourService;
        private List<int> lastAddedTours;
        public TourRequest TourRequest { get; set; }
        public Guide Guide { get; set; }
        public CreateTourViewModel(Guide guide, TourRequest tourRequest)
        {
            tourService = new TourService();
            tourRequestService = new TourRequestService();
            keyPointService = new KeyPointService();
            guideService = new GuideService();
            notificationTourService = new NotificationTourService();
            lastAddedTours = new List<int>();
            Guide = guide;
            TourRequest = tourRequest;
        }
        public KeyPoint CreateAndReturnKeyPoint(string name, KeyPointType type)
        {
            int nextKeyId = keyPointService.NextId();
            KeyPoint keyPoint = new KeyPoint(nextKeyId, name, type);
            keyPointService.Create(keyPoint);
            return keyPoint;
        }
        public void CreateTour(string name, string language, string location, string maxNumberGuests,
                List<Tuple<DateTime, int>> appointments, string startKeyPoint, List<string> otherKeyPoints, string finishKeyPoint,
                string description, List<string> images, bool createdByLocation, bool createdByLanguage)
        {
            foreach (var appointment in appointments)
            {
                List<KeyPoint> keyPoints = new List<KeyPoint>();
                keyPoints.Add(CreateAndReturnKeyPoint(startKeyPoint, KeyPointType.First));
                foreach (string keyPointName in otherKeyPoints)
                {
                    keyPoints.Add(CreateAndReturnKeyPoint(keyPointName, KeyPointType.Intermediate));
                }
                keyPoints.Add(CreateAndReturnKeyPoint(finishKeyPoint, KeyPointType.Last));
                DateTime start = appointment.Item1;
                double duration = appointment.Item2;
                Tour tour = new Tour(-1, Guide.Id, name, location, description, language, Convert.ToInt32(maxNumberGuests), keyPoints.Select(k => k.Id).ToList(), start, duration, images, Convert.ToInt32(maxNumberGuests), TourState.Inactive, -1, keyPoints, -1);
                int lastAddedTour = tourService.NextId();
                lastAddedTours.Add(lastAddedTour);
                tourService.Create(tour);
                if (TourRequest != null)
                {
                    TourRequest.State = TourRequestState.Accepted;
                    TourRequest.AcceptedStartOfAppointment = tour.StartOfTheTour;
                    TourRequest.AcceptedEndOfAppointment = tour.StartOfTheTour.AddHours(tour.Duration);
                    TourRequest.GuideId = Guide.Id;
                    tourRequestService.Update(TourRequest);
                    string content = "Obavjestenje o novim turama (Vas zahtjev je prihvacen)";
                    NotificationTour notification = new NotificationTour(-1, TourRequest.Guest2Id, TourRequest.GuideId, lastAddedTours, content, DateTime.Now, false);
                    notificationTourService.Create(notification);
                }
            }
            SendNotificationsAllGuests(createdByLanguage, language, createdByLocation, location);
        }
        public void SendNotificationsAllGuests(bool CreatedByLanguage, string language, bool CreatedByLocation, string location)
        {
            if (CreatedByLanguage != false)
            {
                List<TourRequest> requests = tourRequestService.GetAllUnrealizedRequestsToLanguage(language);
                List<int> guest2Ids = tourRequestService.GetAllGuest2Ids(requests);
                foreach (int guest2Id in guest2Ids)
                {
                    string content = "Obavjestenje o novim turama koju je vodic kreirao spram statistike o svim zahtjevima" +
                        "(najtrazeniji jezik).";
                    NotificationTour notification = CreateNotificationForGuest(guest2Id, content);
                    notificationTourService.Create(notification);
                }
            }
            if (CreatedByLocation != false)
            {
                List<TourRequest> requests = tourRequestService.GetAllUnrealizedRequestsToLocation(location);
                List<int> guest2Ids = tourRequestService.GetAllGuest2Ids(requests);
                foreach (int guest2Id in guest2Ids)
                {
                    string content = "Obavjestenje o novim turama koju je vodic kreirao spram statistike o svim zahtjevima" +
                        "(najtrazenija lokacija).";
                    NotificationTour notification = CreateNotificationForGuest(guest2Id, content);
                    notificationTourService.Create(notification);
                }
            }
        }
        private NotificationTour CreateNotificationForGuest(int guest2Id, string content)
        {
            NotificationTour notificaiton = new NotificationTour(-1, guest2Id, Guide.Id,
                        lastAddedTours, content, DateTime.Now, false);
            return notificaiton;
        }
        public bool GuideIsAvailable(DateTime date, int hour, int minute, int duration)
        {
            DateTime start = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            DateTime end = start.AddHours(duration);
            return guideService.CheckIfGuideIsAvailable(start, end, Guide.Id);
        }
    }
}
