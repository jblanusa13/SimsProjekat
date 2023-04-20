using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class RequestService
    {
        private RequestRepository requestRepository;
        private List<Request> requests;
        public RequestService()
        {
            requestRepository = new RequestRepository();
            requests = requestRepository.GetAll();
        }

        public List<Request> GetAllRequestByGuest(int guestId)
        {
            return requestRepository.GetAllByGuest(guestId);
        }
        public List<Request> GetAllRequestByOwner(int ownerId)
        {
            return requestRepository.GetAllByOwner(ownerId);
        }
        public List<Request> GetAllRequests()
        {
            return requestRepository.GetAll();
        }
        public int NextId()
        {
            return requests.Max(r => r.Id) + 1;
        }
        public void CreateRequest(int reservationId, DateOnly dateChange)
        {
            int id = NextId();
            Request request = new Request(id, reservationId, dateChange, RequestState.Waiting, "");
            requestRepository.Add(request);
        }
        public void Update(Request request)
        {
            requestRepository.Update(request);
        }
        public void Delete(Request request)
        {
            requestRepository.Remove(request);
        }
        public void Subscribe(IObserver observer)
        {
            requestRepository.Subscribe(observer);
        }
    }
}