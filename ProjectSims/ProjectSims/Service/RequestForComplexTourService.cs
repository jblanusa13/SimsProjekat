using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public  class RequestForComplexTourService
    {
        private IRequestForComplexTourRepository complexTourRequestRepository;
        private IGuest2Repository guest2Repository;
        private ITourRequestRepository tourRequestRepository;
        public RequestForComplexTourService()
        {
            complexTourRequestRepository = Injector.CreateInstance<IRequestForComplexTourRepository>();
            guest2Repository = Injector.CreateInstance<IGuest2Repository>();
            tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            InitializeGuest();
            InitializeTourRequests();
        }
        private void InitializeGuest()
        {
            foreach (var item in complexTourRequestRepository.GetAll())
            {
                if(item.Guest2 != null)
                {
                    return;
                }
                item.Guest2 = guest2Repository.GetById(item.Guest2Id);
            }
        }
        private void InitializeTourRequests()
        {
            foreach (var item in complexTourRequestRepository.GetAll())
            {
                foreach(var requestId in item.RequestIds)
                {
                    if (!item.TourRequests.Contains(tourRequestRepository.GetById(requestId)))
                        item.TourRequests.Add(tourRequestRepository.GetById(requestId));
                }
            }
        }

        public void UpdateRequestForComplexTour()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today).AddDays(2);
            List<RequestForComplexTour> requests = new List<RequestForComplexTour>(GetAllRequests());
            foreach (RequestForComplexTour request in requests)
            {
                if (today >= request.TourRequests.First().DateRangeStart && request.State == TourRequestState.Waiting)
                {
                    request.State = TourRequestState.Invalid;
                    Update(request);
                }
            }
        }

        public int NextId()
        {
            return complexTourRequestRepository.NextId();
        }
        public List<RequestForComplexTour> GetAllRequests()
        {
            return complexTourRequestRepository.GetAll();
        }
        public List<RequestForComplexTour> GetByGuest2Id(int guest2Id)
        {
            return complexTourRequestRepository.GetByGuest2Id(guest2Id);
        }

        public RequestForComplexTour GetById(int id)
        {
            return complexTourRequestRepository.GetById(id);
        }
        public void Create(RequestForComplexTour tourRequest)
        {
            complexTourRequestRepository.Create(tourRequest);
        }
        public void Delete(RequestForComplexTour tourRequest)
        {
            complexTourRequestRepository.Remove(tourRequest);
        }
        public void Update(RequestForComplexTour tourRequest)
        {
            complexTourRequestRepository.Update(tourRequest);
        }
        public void Subscribe(IObserver observer)
        {
            complexTourRequestRepository.Subscribe(observer);
        }
    }
}
