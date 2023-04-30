using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using ProjectSims.Repository;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : Page, IObserver
    {
        public Owner owner;
        public Request SelectedRequest { get; set; }
        public ObservableCollection<Request> RequestList { get; set; }
        private RequestService requestService;
        private AccommodationReservationService accommodationReservationService;

        public Requests(Owner o)
        {
            InitializeComponent();
            DataContext = this;
            owner = o;
            requestService = new RequestService();
            requestService.Subscribe(this);
            RequestList = new ObservableCollection<Request>(requestService.GetAllRequests());
            accommodationReservationService = new AccommodationReservationService();
        }

        public void Update()
        {
            RequestList.Clear();
            foreach (Request request in requestService.GetAllRequests())
            {
                RequestList.Add(request);
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedRequest(sender, SelectedRequest, RequestsTable, CommentTextBox.Text);
            CommentTextBox.Text = "Unesite komentar ukoliko odbijate zahtjev...";
        }

        private void RefuseButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedRequest(sender, SelectedRequest, RequestsTable, CommentTextBox.Text);
            CommentTextBox.Text = "Unesite komentar ukoliko odbijate zahtjev...";
        }

        public void UpdateSelectedRequest(object sender, Request SelectedRequest, DataGrid RequestsTable, string comment)
        {
            SelectedRequest = (Request)RequestsTable.SelectedItem;

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
        private void CommentTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            if (CommentTextBox.Text == "Unesite komentar ukoliko odbijate zahtjev...")
            {
                source.Background = Brushes.MintCream;
                source.Foreground = Brushes.Black;
                source.Clear();
            }
        }

        private void CommentTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            if (CommentTextBox.Text == "")
            {
                CommentTextBox.Text = "Unesite komentar ukoliko odbijate zahtjev...";
                source.Background = Brushes.White;
                source.Foreground = Brushes.Gray;
            }
        }
    }
}
