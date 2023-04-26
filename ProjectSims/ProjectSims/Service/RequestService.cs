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
        public RequestService()
        {
            requestRepository = new RequestRepository();
        }

        public List<Request> GetAllRequestByGuest(int guestId)
        {
            return requestRepository.GetAllByGuest(guestId);
        }
        public List<Request> GetAllRequestByOwner(int ownerId)
        {
            return requestRepository.GetAllByOwner(ownerId);
        }
        
        public void CreateRequest(int reservationId, DateOnly dateChange)
        {
            int id = requestRepository.NextId();
            Request request = new Request(id, reservationId, dateChange, RequestState.Waiting, "");
            requestRepository.Add(request);
        }

        public void UpdateRequestsWhenCancelReservation(AccommodationReservation reservation)
        {
            Request request = requestRepository.GetByReservationId(reservation.Id);
            requestRepository.Remove(request);
        }
        public void Subscribe(IObserver observer)
        {
            requestRepository.Subscribe(observer);
        }
    }
}
