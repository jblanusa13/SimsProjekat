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
        private AccommodationScheduleRepository accommodationScheduleRepository;
        private IRequestRepository requestRepository;
        private IAccommodationReservationRepository accommodationReservationRepository;
        public RequestService()
        {
            accommodationScheduleRepository = new AccommodationScheduleRepository();
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

        public List<Request> GetAllRequests()
        {
            InitializeReservation();
            return requestRepository.GetAll();
        }

        public void CreateRequest(int reservationId, DateOnly dateChange)
        {
            int id = requestRepository.NextId();
            Request request = new Request(id, reservationId, dateChange, RequestState.Waiting, "", false, accommodationReservationRepository.GetById(reservationId));
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
        public bool HasWaitingRequests(int ownerId)
        {
            foreach (Request request in requestRepository.GetAllByOwner(ownerId))
            {
                if (request.State.Equals(RequestState.Waiting))
                {
                    return true;
                }
            }
            return false;
        }
        private void SetReservedForRequest(Request request)
        {
            List<DateRanges> unavailableDates = accommodationScheduleRepository.GetUnavailableDates(request.Reservation.AccommodationId);
            //dateRangesService.FindUnavailableDatesForRequest(request);

            foreach (var date in accommodationScheduleRepository.FindUnavailableDatesForRequest(request))
            {
                if (accommodationScheduleRepository.IsAvailableRequestedDate(request, date.CheckIn, date.CheckOut))
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
