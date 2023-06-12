using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using ProjectSims.Repository;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class TourRequestService
    {
        private ITourRequestRepository tourRequestRepository;
        private IGuest2Repository guest2Repository;
        private IGuideRepository guideRepository;
        private IRequestForComplexTourRepository requestForComplexTourRepository;
        private ITourRepository tourRepository;
        public TourRequestService()
        {
            tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            guest2Repository = Injector.CreateInstance<IGuest2Repository>();
            guideRepository = Injector.CreateInstance<IGuideRepository>();
            requestForComplexTourRepository = Injector.CreateInstance<IRequestForComplexTourRepository>();
            tourRepository = Injector.CreateInstance<ITourRepository>();
            InitializeGuest();
            InitializeGuide();
        }
        private void InitializeGuest()
        {
            foreach (var item in tourRequestRepository.GetAll())
            {
                item.Guest2 = guest2Repository.GetById(item.Guest2Id);
            }
        }
        private void InitializeGuide()
        {
            foreach (var item in tourRequestRepository.GetAll())
            {
                item.Guide = guideRepository.GetById(item.GuideId);
            }
        }

        public void UpdateTourRequestsFile()
        {            
            DateOnly today = DateOnly.FromDateTime(DateTime.Today).AddDays(2);
            List<TourRequest> requests = new List<TourRequest>(GetAllRequests());
            foreach (TourRequest request in requests)
            {
                if (today >= request.DateRangeStart && request.State == TourRequestState.Waiting)
                {
                    request.State = TourRequestState.Invalid;
                    Update(request);
                }
            }
        }

        public int NextId()
        {
            return tourRequestRepository.NextId();
        }
        public List<TourRequest> GetAllRequests()
        {
            return tourRequestRepository.GetAll();
        }
        public List<int> GetGuideOfComplexTourIds(RequestForComplexTour requestForComplexTour)
        {
            List<TourRequest> simpleRequests = requestForComplexTour.TourRequests;
            return simpleRequests.Select(s => s.GuideId).ToList();
        }
        public bool  CheckIfGuideAcceptPartOfComplexTour(TourRequest request,int guideId)
        {
            RequestForComplexTour requestForComplexTour = requestForComplexTourRepository.GetBySimpleRequestId(request.Id);
            return GetGuideOfComplexTourIds(requestForComplexTour).Contains(guideId);
        }
        public List<TourRequest> GetAvailableRequestsForGuide(int guideId)
        {
            List<TourRequest> availableRequestsForGuide = new List<TourRequest>();
            foreach (var request in tourRequestRepository.GetWaitingRequests())
            {
                if (request.RequestForComplexTour)
                {
                    if (!CheckIfGuideAcceptPartOfComplexTour(request, guideId))
                    {
                        availableRequestsForGuide.Add(request);
                    }
                }
                else
                {
                    availableRequestsForGuide.Add(request);
                }
            }
            return availableRequestsForGuide;
        }
        public TourRequest GetById(int id)
        {
            return tourRequestRepository.GetById(id);
        }
        public List<TourRequest> GetRequestsBySearchParameters(string location, string language, string maxNumberGuests, List<DateTime> dateRange)
        {
            List<TourRequest> wantedRequests = tourRequestRepository.GetWaitingRequests();
            if (location != "")
            {
                List<TourRequest> tourRequestsByLocation = tourRequestRepository.GetByLocation(location);
                wantedRequests.RemoveAll(request => !tourRequestsByLocation.Contains(request));
            }
            if (language != "")
            {
                List<TourRequest> tourRequestsByLanguage = tourRequestRepository.GetByLanguage(language);
                wantedRequests.RemoveAll(request => !tourRequestsByLanguage.Contains(request));
            }
            if (maxNumberGuests != "")
            {
                List<TourRequest> tourRequestsByMaxNumberGuests = tourRequestRepository.GetByMaxNumberGuests(int.Parse(maxNumberGuests));
                wantedRequests.RemoveAll(request => !tourRequestsByMaxNumberGuests.Contains(request));
            }
            if (dateRange.Count != 0)
            {
                List<TourRequest> tourRequestsInDateRange = tourRequestRepository.GetRequestsInDateRange(DateOnly.FromDateTime(dateRange.First()), DateOnly.FromDateTime(dateRange.Last()));
                wantedRequests.RemoveAll(request => !tourRequestsInDateRange.Contains(request));
            }
            return wantedRequests;
        }
        public Dictionary<int, int> GetStatisticsData(string location, string language, int year)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            List<int> years = GetYears();
            List<int> months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            List<TourRequest> filteredRequests = GetRequestsByStatisticsParameters(location, language);
            if (year == -1)
            {
                foreach (int y in years)
                {
                    result[y] = filteredRequests.Where(r => r.CreationDate.Year == y).Count();
                }
                return result;
            }
            foreach (int m in months)
            {
                result[m] = filteredRequests.Where(r => r.CreationDate.Month == m && r.CreationDate.Year == year).Count();
            }
            return result;
        }
        public List<TourRequest> GetRequestsByStatisticsParameters(string location, string language)
        {
            if (location == "" && language == "")
                return tourRequestRepository.GetAll();
            else if (language == "")
                return tourRequestRepository.GetByLocation(location);
            else if (location == "")
                return tourRequestRepository.GetByLanguage(language);
            return tourRequestRepository.GetByLocation(location).Where(r => tourRequestRepository.GetByLanguage(language).Contains(r)).ToList();
        }
        public List<DateOnly> GetDaysBetweenRange(TourRequest tourRequest)
        {
            List<DateOnly> days = new List<DateOnly>();
            DateOnly day = tourRequest.DateRangeStart;
            int numberOfDays = (tourRequest.DateRangeEnd.DayNumber - tourRequest.DateRangeStart.DayNumber);
            for(int i=0; i <= numberOfDays; i++)
            {
                days.Add(day);
                day = day.AddDays(1);
            }
            return days;
        }
        public List<int> GetYears()
        {
            List<int> years = tourRequestRepository.GetAll().Select(r => r.CreationDate.Year).ToList();
            return years.Distinct().ToList();
        }
        public List<TourRequest> GetRequestsInLastYear()
        {
            return tourRequestRepository.GetAll().Where(r => (DateTime.Now - r.CreationDate).TotalDays <= 365).ToList();
        }      
        public string GetMostWantedLanguageInLastYear()
        {
            if (GetRequestsInLastYear().Count() != 0)
            {
                List<String> languagesInLastYear = GetRequestsInLastYear().Select(r => r.Language.ToLower()).ToList();
                return GetMostCommonElement(languagesInLastYear);
            }
            return null;
        }
        public string GetMostWantedLocationInLastYear()
        {
            if(GetRequestsInLastYear().Count() != 0)
            {
                List<String> locationsInLastYear = GetRequestsInLastYear().Select(r => r.Location.ToLower()).ToList();
                return GetMostCommonElement(locationsInLastYear);
            }
            return null;
        }
        public string GetMostCommonElement(List<string> list)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (string element in list)
            {
                if (counts.ContainsKey(element))
                {
                    counts[element] += 1;
                }
                else
                {
                    counts.Add(element, 1);
                }
            }
            int maxCount = counts.Values.Max();
            if (counts.Where(c=> c.Value == maxCount).Count() > 1)
            {
                return null;
            }
            return counts.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; ;
        }
        public List<TourRequest> GetByGuest2Id(int guest2Id)
        {
            return tourRequestRepository.GetByGuest2Id(guest2Id);
        }
        public int GetNumberRequestsByState(List<TourRequest> requests, TourRequestState state)
        {
            int number = 0;
            foreach (TourRequest request in requests)
            {
                if(request.State == state)
                {
                    number++;
                }
            }
            return number;
        }
        public double GetAverageNumberOfPeopleOnAcceptedRequests(List<TourRequest> requests)
        {
            int numberPeople = 0;
            int numberAcceptedRequest = 0;
            foreach(TourRequest request in requests)
            {
                if(request.State == TourRequestState.Accepted)
                {
                    numberPeople += request.MaxNumberGuests;
                    numberAcceptedRequest++;
                }
            }
            if (numberAcceptedRequest == 0) return 0;
            return Math.Round((double)numberPeople/numberAcceptedRequest,2);
        }
        public int GetNumberRequestsByLanguage(List<TourRequest> requests, string language)
        {
            int number = 0;
            foreach (TourRequest request in requests)
            {
                if (request.Language == language)
                {
                    number++;
                }
            }
            return number;
        }
        public List<string> GetAllLocations(List<TourRequest> requests)
        {
            List<string> locations = new List<string>();
            foreach(TourRequest request in requests)
            {
                if(!locations.Contains(request.Location))
                    locations.Add(request.Location);
            }
            return locations;
        }
        public int GetNumberRequestsByLocation(List<TourRequest> requests, string location)
        {
            int number = 0;
            foreach (TourRequest request in requests)
            {
                if (request.Location == location)
                {
                    number++;
                }
            }
            return number;
        }
        public List<TourRequest> GetAllUnrealizedRequests()
        {
            return tourRequestRepository.GetUnrealizedRequests();

        }
        public List<TourRequest> GetAllUnrealizedRequestsToLanguage(string language)
        {
            List<TourRequest> requests = GetAllUnrealizedRequests();
            List<TourRequest> unrealizedRequests = new List<TourRequest>();
            foreach(var request in requests)
            {
                if(request.Language.ToUpper() == language)
                {
                    unrealizedRequests.Add(request);
                }
            }
            return unrealizedRequests;
        }

        public List<TourRequest> GetAllUnrealizedRequestsToLocation(string location)
        {
            List<TourRequest> requests = GetAllUnrealizedRequests();
            List<TourRequest> unrealizedRequests = new List<TourRequest>();
            foreach (var request in requests)
            {
                if (request.Location.ToUpper() == location)
                {
                    unrealizedRequests.Add(request);
                }
            }
            return unrealizedRequests;
        }

        public List<int> GetAllGuest2Ids(List<TourRequest> requests)
        {
            List<int> guest2Ids = new List<int>();
            foreach (TourRequest request in requests)
            {
                if (!guest2Ids.Contains(request.Guest2Id))
                    guest2Ids.Add(request.Guest2Id);
            }
            return guest2Ids;
        }
        public List<TourRequest> GetRequestsByYear(List<TourRequest> requests, string year)
        {
            List<TourRequest> wantedRequests = new List<TourRequest>();
            if(year == "Svih vremena")
            {
                return requests;
            }
            foreach(var request in requests)
            {
                if(request.CreationDate.Year.ToString() == year)
                {
                    wantedRequests.Add(request);
                }
            }
            return wantedRequests;
        }
        
        public void Create(TourRequest tourRequest)
        {
            tourRequestRepository.Create(tourRequest);
        }
        public void Delete(TourRequest tourRequest)
        {
            tourRequestRepository.Remove(tourRequest);
        }
        public void Update(TourRequest tourRequest)
        {
            tourRequestRepository.Update(tourRequest);
        }
        public void Subscribe(IObserver observer)
        {
            tourRequestRepository.Subscribe(observer);
        }
    }
}
