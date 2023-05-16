using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using ProjectSims.Repository;
using ProjectSims.WPF.View.OwnerView.Pages;

namespace ProjectSims.Service
{
    public class RequestService
    {
        private DateRangesService dateRangesService;
        private IRequestRepository requestRepository;
        private IAccommodationReservationRepository accommodationReservationRepository;
        public RequestService()
        {
            dateRangesService = new DateRangesService();
            requestRepository = Injector.CreateInstance<IRequestRepository>();
            accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();

            InitializeReservation();    
        }

        private void InitializeReservation()
        {
            foreach(var request in requestRepository.GetAll())
            {
                request.Reservation = accommodationReservationRepository.GetById(request.ReservationId);
            }
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

        public void CreateRequest(int reservationId, DateOnly dateChange)
        {
            int id = requestRepository.NextId();
            Request request = new Request(id, reservationId, dateChange, RequestState.Waiting, "", false);
            SetReservedForRequest(request);
            requestRepository.Create(request);
        }
        public void Update(Request request)
        {
            requestRepository.Update(request);
        }
        public void Delete(Request request)
        {
            requestRepository.Remove(request);
        }


        private void SetReservedForRequest(Request request)
        {
            List<DateRanges> unavailableDates = dateRangesService.FindUnavailableDatesForRequest(request);

            foreach (var date in dateRangesService.FindUnavailableDatesForRequest(request))
            {
                if (dateRangesService.IsInRange(request, date.CheckIn, date.CheckOut))
                {
                    request.Reserved = true;
                }
                else
                {
                    request.Reserved = false;
                }
            }
        }

        public void Subscribe(IObserver observer)
        {
            requestRepository.Subscribe(observer);
        }
    }
}
