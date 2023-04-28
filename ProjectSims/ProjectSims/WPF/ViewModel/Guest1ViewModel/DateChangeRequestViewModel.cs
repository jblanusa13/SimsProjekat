using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public partial class DateChangeRequestViewModel
    {
        private RequestService requestService;
        private int reservationId;
        public Accommodation Accommodation { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int GuestNumber { get; set; }
        public DateOnly DateChange { get; set; }

        public DateChangeRequestViewModel(AccommodationReservation selectedReservation)
        {
            requestService = new RequestService();

            reservationId = selectedReservation.Id;
            Accommodation = selectedReservation.Accommodation;
            CheckInDate = selectedReservation.CheckInDate;
            CheckOutDate = selectedReservation.CheckOutDate;
            GuestNumber = selectedReservation.GuestNumber;
        }
        public void SendRequest(DateOnly dateChange)
        {
            DateChange = dateChange;
            requestService.CreateRequest(reservationId, DateChange, "");
        }
    }
}
