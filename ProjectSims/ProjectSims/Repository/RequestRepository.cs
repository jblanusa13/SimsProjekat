using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private RequestFileHandler requestFileHandler;
        private List<Request> requests;
        private readonly List<IObserver> observers;
        public RequestRepository()
        {
            requestFileHandler = new RequestFileHandler();
            requests = requestFileHandler.Load();
            observers = new List<IObserver>();
        }
        public List<Request> GetAllByGuest(int guestId)
        {
            List<Request> guestRequests = new List<Request>();
            foreach (Request request in requests)
            {
                if (request.Reservation.GuestId == guestId)
                {
                    guestRequests.Add(request);
                }
            }
            return guestRequests;
        }

        public List<Request> GetAllByOwner(int ownerId)
        {
            List<Request> ownerRequests = new List<Request>();
            foreach (Request request in requests)
            {
                if (request.Reservation.Accommodation.IdOwner == ownerId)
                {
                    ownerRequests.Add(request);
                }
            }
            return ownerRequests;
        }

        public Request GetByReservationId(int reservationId)
        {
            return requests.Find(r => r.ReservationId == reservationId);
        }
        public List<Request> GetAll()
        {
            return requests;
        }

        public int NextId()
        {
            if (requests.Count == 0)
            {
                return 0;
            }
            return requests.Max(r => r.Id) + 1;
        }
        public void Create(Request entity)
        {
            requests.Add(entity);
            requestFileHandler.Save(requests);
            NotifyObservers();
        }

        public void Update(Request entity)
        {
            int index = requests.FindIndex(a => entity.Id == a.Id);
            if (index != -1)
            {
                requests[index] = entity;
            }
            requestFileHandler.Save(requests);
            NotifyObservers();
        }
        public void Remove(Request entity)
        {
            requests.Remove(entity);
            requestFileHandler.Save(requests);
            NotifyObservers();
        }

        public Request GetById(int key)
        {
            return requests.Find(r => r.Id == key);
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