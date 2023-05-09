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
        public int GetNextId()
        {
            return tourRequestRepository.GetNextId();
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
        public List<TourRequest> GetWantedRequests(string location,string language,string maxNumberGuests,DateTime dateRangeStart,DateTime dateRangeEnd)
        {
            List<TourRequest> wantedRequests = new List<TourRequest>();
            List<TourRequest> tourRequestsOnLocation = tourRequestRepository.GetAll();
            List<TourRequest> tourRequestsOnLanguage = tourRequestRepository.GetAll();
            List<TourRequest> tourRequestsWithMaxNumberGuests = tourRequestRepository.GetAll();
            List<TourRequest> tourRequestsInDateRange = tourRequestRepository.GetAll();
            if (location != "")
                tourRequestsOnLocation = tourRequestRepository.GetByLocation(location);
            if (language != "")
                tourRequestsOnLanguage = tourRequestRepository.GetByLanguage(language);
            if(maxNumberGuests != "")
                tourRequestsWithMaxNumberGuests = tourRequestRepository.GetByMaxNumberGuests(int.Parse(maxNumberGuests));
            tourRequestsInDateRange = tourRequestRepository.GetRequestsInDateRange(DateOnly.FromDateTime(dateRangeStart),DateOnly.FromDateTime(dateRangeEnd.Date));
            foreach(TourRequest request in tourRequestRepository.GetAll())
            {
                if(tourRequestsOnLocation.Contains(request) && tourRequestsOnLanguage.Contains(request) && tourRequestsWithMaxNumberGuests.Contains(request) && tourRequestsInDateRange.Contains(request))
                    wantedRequests.Add(request);
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
