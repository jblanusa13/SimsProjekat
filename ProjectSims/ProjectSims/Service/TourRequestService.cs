using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using ProjectSims.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class TourRequestService
    {
        private ITourRequestRepository tourRequestRepository;
        private TourService tourService;
        public TourRequestService()
        {
            tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            tourService = new TourService();
        }
        public int NextId()
        {
            return tourRequestRepository.NextId();
        }
        public List<TourRequest> GetAllRequests()
        {
            return tourRequestRepository.GetAll();
        }
        public List<TourRequest> GetWaitingRequests()
        {
            return tourRequestRepository.GetWaitingRequests();
        }
        public TourRequest GetById(int id)
        {
            return tourRequestRepository.GetById(id);
        }
        public List<TourRequest> GetWantedRequests(string location,string language,string maxNumberGuests,List<DateTime> dateRange)
        {
            List<TourRequest> wantedRequests = tourRequestRepository.GetWaitingRequests();
            if (location != "")
                wantedRequests.RemoveAll(request => !tourRequestRepository.GetByLocation(location).Contains(request));
            if (language != "")
                wantedRequests.RemoveAll(request => !tourRequestRepository.GetByLanguage(language).Contains(request));
            if (maxNumberGuests != "")
                wantedRequests.RemoveAll(request => !tourRequestRepository.GetByMaxNumberGuests(int.Parse(maxNumberGuests)).Contains(request));
            if (dateRange.Count != 0)
                wantedRequests.RemoveAll(request => !tourRequestRepository.GetRequestsInDateRange(DateOnly.FromDateTime(dateRange.First()), DateOnly.FromDateTime(dateRange.Last())).Contains(request));
            return wantedRequests;
        }
        public List<TourRequest> GetRequestsInLastYear()
        {
            return tourRequestRepository.GetInLastYear();
        }

        public string GetMostWantedLanguageInLastYear()
        {
            List<String> languagesInLastYear = GetRequestsInLastYear().Select(r=>r.Language.ToLower()).ToList();
            Dictionary<string, int> languageCounts = new Dictionary<string, int>();
            foreach (string language in languagesInLastYear)
                {
                    if (languageCounts.ContainsKey(language))
                    {
                    languageCounts[language] += 1;
                    }
                    else
                    {
                    languageCounts.Add(language, 1);
                    }
                }
            return languageCounts.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
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
