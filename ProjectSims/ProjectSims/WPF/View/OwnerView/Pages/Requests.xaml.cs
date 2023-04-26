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
    public partial class Requests : Page, INotifyPropertyChanged, IObserver
    {
        public Owner owner;
        public Request SelectedRequest { get; set; }
        public ObservableCollection<Request> RequestList { get; set; }
        private RequestService requestService;
        private RequestRepository requestRepository;
        private AccommodationReservationService accommodationReservationService;

        public Requests(Owner o)
        {
            InitializeComponent();
            DataContext = this;
            owner = o;
            requestService = new RequestService();
            requestService.Subscribe(this);
            RequestList = new ObservableCollection<Request>(requestService.GetAllRequests());
            requestRepository = new RequestRepository();
            accommodationReservationService = new AccommodationReservationService();

            SetReserved();
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _reserved = true;
        public bool Reserved
        {
            get => _reserved = true;

            set
            {
                if (value != _reserved)
                {
                    _reserved = value;
                    OnPropertyChanged();
                }
            }
        }

        private void SetReserved()
        {
            foreach (var item in RequestList)
            {
                if (IsReserved(item))
                {
                    Reserved = false;
                }
                else
                {
                    Reserved = true;
                }
                requestRepository.NotifyObservers();
            }
        }

        private bool IsReserved(Request request)
        {
            foreach (var item in accommodationReservationService.GetAllReservations())
            {
                if (request.Reservation.AccommodationId == item.AccommodationId && IsInRange(item.CheckInDate, item.CheckOutDate, request.ChangeDate))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsInRange(DateOnly firstDate, DateOnly lastDate, DateOnly requestedDate)
        {
            return requestedDate >= firstDate && requestedDate <= lastDate;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            Button clickedButton = sender as Button;
            UpdateSelectedRequest(clickedButton);
        }

        private void RefuseButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            UpdateSelectedRequest(clickedButton);
        }

        private void UpdateSelectedRequest(Button clickedButton) 
        {
            if (SelectedRequest != null)
            {
                SelectedRequest.State = RequestState.Approved;
                //SetRequestState(clickedButton);
                requestService.Update(SelectedRequest);
                accommodationReservationService.Update(SelectedRequest.Reservation);
                requestService.Delete(SelectedRequest);
                UpdateLayout();
                Update();
            }
            else if (SelectedRequest == null)
            {
                //Do nothing
            }
        }

        private void SetRequestState(Button clickedButton)
        {
            if (clickedButton == AcceptButton)
            {
                SelectedRequest.State = RequestState.Approved;
            }
            else if (clickedButton == RefuseButton)
            {
                SelectedRequest.State = RequestState.Rejected;
            }
        }
    }
}
