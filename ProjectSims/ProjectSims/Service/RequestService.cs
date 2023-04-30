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
        private AccommodationReservationService accommodationReservationService;
        private AccommodationReservationRepository reservationRepository;
        private List<DateRanges> unavailableDates;

        private IRequestRepository requestRepository;
        public RequestService()
        {
            dateRangesService = new DateRangesService();
            accommodationReservationService = new AccommodationReservationService();
            reservationRepository = new AccommodationReservationRepository();
            unavailableDates = new List<DateRanges>();
            requestRepository = Injector.CreateInstance<IRequestRepository>();
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
            return requestRepository.GetAll().Max(r => r.Id) + 1;
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

        public void UpdateRequestsWhenCancelReservation(AccommodationReservation reservation)
        {
            Request request = requestRepository.GetByReservationId(reservation.Id);
            requestRepository.Remove(request);
        }

        private void SetReservedForRequest(Request request)
        {
            unavailableDates = accommodationReservationService.FindUnavailableDates(request);
            accommodationReservationService.SetReserved(request);
        }

        public void UpdateSelectedRequest(object sender, Request SelectedRequest, DataGrid RequestsTable, string comment)
        {
            SelectedRequest = (Request)RequestsTable.SelectedItem;

            if (SelectedRequest != null)
            {
                SelectedRequest.State = Set(sender);
                SelectedRequest.OwnerComment = comment;
                Update(SelectedRequest);
                SelectedRequest.Reservation.CheckInDate = SelectedRequest.ChangeDate;
                SelectedRequest.Reservation.CheckOutDate = SelectedRequest.Reservation.CheckInDate.AddDays(SelectedRequest.Reservation.CheckOutDate.DayNumber - SelectedRequest.Reservation.CheckInDate.DayNumber);
                accommodationReservationService.Update(SelectedRequest.Reservation);
            }
            else if (SelectedRequest == null)
            {
                //Do nothing
            }
        }

        RequestState Set(object sender)
        {
            Button clickedButton = sender as Button;

            if (clickedButton.Name == "AcceptButton" && clickedButton != null)
            {
                return RequestState.Approved;
            }

            return RequestState.Rejected;
        }
        public void Subscribe(IObserver observer)
        {
            requestRepository.Subscribe(observer);
        }
    }
}
