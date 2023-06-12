using ceTe.DynamicPDF;
using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.OwnerView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class RequestsViewModel : System.Windows.Controls.Page, IObserver, INotifyPropertyChanged
    {
        public Owner owner { get; set; }
        public RequestsView View { get; set; }
        public Request SelectedRequest { get; set; }
        private RequestService requestService;
        private NotificationOwnerGuestService notificationService;
        private Guest1Service guest1Service;
        private AccommodationReservationService accommodationReservationService;
        public ObservableCollection<Request> RequestList { get; set; }
        public RelayCommand GotKeyboardFocusCommand { get; set; }
        public RelayCommand LostKeyboardFocusCommand { get; set; }
        public RelayCommand AcceptCommand { get; set; }
        public RelayCommand RefuseCommand { get; set; }

        private string _comment = "Unesite obrazloženje ukoliko odbijate zahtjev...";
        public string Comment
        {
            get => _comment;

            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }
        public RequestsViewModel(Owner o, RequestsView view) 
        {
            View = view;
            requestService = new RequestService();
            requestService.Subscribe(this);
            notificationService = new NotificationOwnerGuestService();
            guest1Service = new Guest1Service();
            accommodationReservationService = new AccommodationReservationService();
            RequestList = new ObservableCollection<Request>(requestService.GetAllRequests().Where(r => r.State == RequestState.Waiting));
            owner = o;
            GotKeyboardFocusCommand = new RelayCommand(Execute_GotCommand, CanExecute_GotCommand);
            LostKeyboardFocusCommand = new RelayCommand(Execute_LostCommand, CanExecute_LostCommand);
            AcceptCommand = new RelayCommand(Execute_AcceptCommand, CanExecute_AcceptCommand);
            RefuseCommand = new RelayCommand(Execute_RefuseCommand, CanExecute_RefuseCommand);
        }

        private void Execute_AcceptCommand(object obj)
        {
            SelectedRequest = (Request)View.RequestsTable.SelectedItem;
            UpdateSelectedRequest(View.AcceptButton, SelectedRequest, Comment);
            Comment = "Unesite obrazloženje ukoliko odbijate zahtjev...";
        }

        private bool CanExecute_AcceptCommand(object obj)
        {
            return SelectedRequest != null;
        }

        private void Execute_RefuseCommand(object obj)
        {
            SelectedRequest = (Request)View.RequestsTable.SelectedItem;
            UpdateSelectedRequest(View.RefuseButton, SelectedRequest, Comment);
            Comment = "Unesite obrazloženje ukoliko odbijate zahtjev...";
        }

        private bool CanExecute_RefuseCommand(object obj)
        {
            return SelectedRequest != null 
                && !string.IsNullOrEmpty(Comment) 
                && Comment != "Unesite obrazloženje ukoliko odbijate zahtjev...";
        }

        private void Execute_LostCommand(object obj)
        {
            Comment = "Unesite obrazloženje ukoliko odbijate zahtjev...";
            View.CommentTextBox.Background = Brushes.White;
            View.CommentTextBox.Foreground = Brushes.Gray;
        }

        private bool CanExecute_LostCommand(object obj)
        {
            return Comment == "";
        }

        private void Execute_GotCommand(object obj)
        {
            View.CommentTextBox.Focus();
            View.CommentTextBox.Background = Brushes.MintCream;
            View.CommentTextBox.Foreground = Brushes.Black;
            View.CommentTextBox.Clear();
        }

        private bool CanExecute_GotCommand(object obj)
        {
            return Comment == "Unesite obrazloženje ukoliko odbijate zahtjev...";
        }

        public void UpdateSelectedRequest(object sender, Request SelectedRequest, string comment)
        {
            if (SelectedRequest != null)
            {
                SelectedRequest.State = Set(sender);
                string content = "";
                if (SelectedRequest.State == RequestState.Approved)
                {
                    content = "Obavjestenje o prihvacenom zahtjevu";
                }
                else 
                {
                    content = "Obavjestenje o odbijenom zahtjevu";
                }
                SelectedRequest.ForumComment = comment;
                requestService.Update(SelectedRequest);
                SelectedRequest.Reservation.CheckInDate = SelectedRequest.ChangeDate;
                SelectedRequest.Reservation.CheckOutDate = SelectedRequest.Reservation.CheckInDate.AddDays(SelectedRequest.Reservation.CheckOutDate.DayNumber - SelectedRequest.Reservation.CheckInDate.DayNumber);
                accommodationReservationService.Update(SelectedRequest.Reservation);
                notificationService.Create(new NotificationOwnerGuest(notificationService.NextId(), SelectedRequest.Reservation.GuestId, 
                    SelectedRequest.Reservation.Guest, owner.Id, owner, SelectedRequest.Id, SelectedRequest, -1, null, content));
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Update()
        {
            RequestList.Clear();
            foreach (Request request in requestService.GetAllRequests().Where(r => r.State == RequestState.Waiting))
            {
                RequestList.Add(request);
            }
        }

    }
}
