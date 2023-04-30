using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class RequestsViewModel : Page, IObserver
    {
        public Owner owner { get; set; }
        private RequestService requestService;
        private AccommodationReservationService accommodationReservationService;
        public ObservableCollection<Request> RequestList { get; set; }

        public RequestsViewModel(Owner o) 
        {
            requestService = new RequestService();
            requestService.Subscribe(this);
            accommodationReservationService = new AccommodationReservationService();
            RequestList = new ObservableCollection<Request>(requestService.GetAllRequests());
            owner = o;
        }

        public void UpdateSelectedRequest(object sender, Request SelectedRequest, string comment)
        {
            if (SelectedRequest != null)
            {
                SelectedRequest.State = Set(sender);
                SelectedRequest.OwnerComment = comment;
                requestService.Update(SelectedRequest);
                SelectedRequest.Reservation.CheckInDate = SelectedRequest.ChangeDate;
                SelectedRequest.Reservation.CheckOutDate = SelectedRequest.Reservation.CheckInDate.AddDays(SelectedRequest.Reservation.CheckOutDate.DayNumber - SelectedRequest.Reservation.CheckInDate.DayNumber);
                accommodationReservationService.Update(SelectedRequest.Reservation);
            }
            else if (SelectedRequest == null)
            {
                //Do nothing
            }
        }
        public RequestState Set(object sender)
        {
            Button clickedButton = sender as Button;

            if (clickedButton.Name == "AcceptButton" && clickedButton != null)
            {
                return RequestState.Approved;
            }

            return RequestState.Rejected;
        }
        public void Update()
        {
            RequestList.Clear();
            foreach (Request request in requestService.GetAllRequests())
            {
                RequestList.Add(request);
            }
        }

    }
}
