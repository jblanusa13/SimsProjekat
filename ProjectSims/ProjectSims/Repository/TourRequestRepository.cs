using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;

namespace ProjectSims.Repository
{
    public class TourRequestRepository : ITourRequestRepository
    {
        private TourRequestFileHandler tourRequestFile;
        private List<TourRequest> tourRequests;
        private List<IObserver> observers;

        public TourRequestRepository()
        {
            tourRequestFile = new TourRequestFileHandler();
            tourRequests = tourRequestFile.Load();
            observers = new List<IObserver>();
        }
        public int GetNextId()
        {
            if (tourRequests.Count == 0)
            {
                return 0;
            }
            return tourRequests.Max(t => t.Id) + 1;
        }
        public void Create(TourRequest tourRequest)
        {
            tourRequests.Add(tourRequest);
            tourRequestFile.Save(tourRequests);
            NotifyObservers();
        }
        public void Remove(TourRequest tourRequest)
        {
            tourRequests.Remove(tourRequest);
            tourRequestFile.Save(tourRequests);
            NotifyObservers();
        }
        public void Update(TourRequest tourRequest)
        {
            int index = tourRequests.FindIndex(t => t.Id == tourRequest.Id);
            if (index != -1)
            {
                tourRequests[index] = tourRequest;
            }
            tourRequestFile.Save(tourRequests);
            NotifyObservers();
        }
        public List<TourRequest> GetAll()
        {
            return tourRequests;
        }
        public List<TourRequest> GetWaitingRequests()
        {
            return tourRequests.Where(r=> r.State == TourRequestState.Waiting).ToList();
        }
        public TourRequest GetById(int id)
        {
            return tourRequests.Find(t => t.Id == id);
        }
        public List<TourRequest> GetByLocation(string location)
        {
            return tourRequests.Where(t => t.Location.ToLower().Contains(location)).ToList();
        }
        public List<TourRequest> GetByLanguage(string language)
        {
            return tourRequests.Where(t => t.Language.ToLower().Contains(language)).ToList();
        }
        public List<TourRequest> GetByMaxNumberGuests(int maxNumberGuests)
        {
            return tourRequests.Where(t => t.MaxNumberGuests == maxNumberGuests).ToList();
        }
        public List<TourRequest> GetRequestsInDateRange(DateOnly dateRangeStart,DateOnly dateRangeEnd)
        {
            return tourRequests.Where(t => t.DateRangeStart.CompareTo(dateRangeStart) >= 0 && t.DateRangeEnd.CompareTo(dateRangeEnd) <=0).ToList();
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
