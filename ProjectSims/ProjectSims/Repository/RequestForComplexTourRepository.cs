using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Repository
{
    public class RequestForComplexTourRepository : IRequestForComplexTourRepository
    {
        private RequestForComplexTourFileHandler tourRequestFile;
        private List<RequestForComplexTour> tourRequests;
        private List<IObserver> observers;

        public RequestForComplexTourRepository()
        {
            tourRequestFile = new RequestForComplexTourFileHandler();
            tourRequests = tourRequestFile.Load();
            observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (tourRequests.Count == 0)
            {
                return 0;
            }
            return tourRequests.Max(t => t.Id) + 1;
        }
        public void Create(RequestForComplexTour tourRequest)
        {
            tourRequest.Id = NextId();
            tourRequests.Add(tourRequest);
            tourRequestFile.Save(tourRequests);
            NotifyObservers();
        }
        public void Remove(RequestForComplexTour tourRequest)
        {
            tourRequests.Remove(tourRequest);
            tourRequestFile.Save(tourRequests);
            NotifyObservers();
        }
        public void Update(RequestForComplexTour tourRequest)
        {
            int index = tourRequests.FindIndex(t => t.Id == tourRequest.Id);
            if (index != -1)
            {
                tourRequests[index] = tourRequest;
            }
            tourRequestFile.Save(tourRequests);
            NotifyObservers();
        }

        public List<RequestForComplexTour> GetAll()
        {
            return tourRequests;
        }
        public RequestForComplexTour GetById(int id)
        {
            return tourRequests.Find(t => t.Id == id);
        }
        public List<RequestForComplexTour> GetByGuest2Id(int guest2Id)
        {
            return tourRequests.Where(t => t.Guest2Id == guest2Id).ToList();
        }
      
        public RequestForComplexTour GetBySimpleRequestId(int simpleRequestId)
        {
            foreach(var request in tourRequests)
            {
                if (request.RequestIds.Contains(simpleRequestId))
                    return request;
            }
            return null;
        }
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
