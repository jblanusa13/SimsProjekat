using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.GuideView.Pages;
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
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class TourTrackingViewModel : INotifyPropertyChanged, IObserver
    {
        private TourService tourService;
        private KeyPointService keyPointService;
        private Guest2Service guest2Service;
        private ReservationTourService reservationTourService;
        public NavigationService NavService { get; set; }
  
        public ObservableCollection<KeyPoint> UnFinishedKeyPoints { get; set; }
        public ObservableCollection<KeyPoint> FinishedKeyPoints { get; set; }
        public List<Guest2> WaitingGuests { get; set; }
        public List<Guest2> PresentGuests { get; set; }
        private Tour Tour { get; set; }
        public Guide Guide { get; set; }
        public RelayCommand FinishTourCommand { get; set; }
        public RelayCommand  BackCommand { get; set; }
        public RelayCommand SelectKeyPointCommand{ get; set; }
        public RelayCommand NotifyGuestCommand { get; set; }
        
        private KeyPoint _selectedKeyPoint;
        public KeyPoint SelectedKeyPoint
        {
            get => _selectedKeyPoint;
            set
            {
                if (value != _selectedKeyPoint)
                {
                    _selectedKeyPoint = value;
                    OnPropertyChanged();
                }
            }
        }
        private Guest2 _selectedGuest;
        public Guest2 SelectedGuest
        {
            get => _selectedGuest;
            set
            {
                if (value != _selectedGuest)
                {
                    _selectedGuest = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _imgSrc;
        public string ImgSrc
        {
            get => _imgSrc;
            set
            {
                if (value != _imgSrc)
                {
                    _imgSrc = value;
                    OnPropertyChanged();
                }
            }
        }
        public TourTrackingViewModel(Guide guide,Tour tour, NavigationService navigationService)
        {
            NavService = navigationService;
            Guide = guide;
            Tour = tour;
            tourService = new TourService();
            keyPointService = new KeyPointService();
            guest2Service = new Guest2Service();
            reservationTourService = new ReservationTourService();
            FinishTourCommand = new RelayCommand(Execute_FinishTourCommand);
            BackCommand = new RelayCommand(Execute_BackCommand);
            SelectKeyPointCommand = new RelayCommand(Execute_SelectKeyPointCommand);
            if (tour != null)
            {
                keyPointService.Subscribe(this);
                reservationTourService.Subscribe(this);
                UnFinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.GetKeyPointsByStateAndIds(Tour.KeyPointIds, false));
                FinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.GetKeyPointsByStateAndIds(Tour.KeyPointIds, true));
                WaitingGuests = new List<Guest2>();
                PresentGuests = new List<Guest2>();
                tourService.UpdateActiveKeyPoint(Tour, keyPointService.GetKeyPointsByStateAndIds(Tour.KeyPointIds, false).First().Id);
               
                foreach (int id in reservationTourService.GetGuestIdsByTourAndState(Tour, Guest2State.ActiveTour))
                {
                    WaitingGuests.Add(guest2Service.GetGuestById(id));
                }
                foreach (int id in reservationTourService.GetGuestIdsByTourAndState(Tour, Guest2State.Present))
                {
                    PresentGuests.Add(guest2Service.GetGuestById(id));
                }
            }
        }
        private void Execute_SelectKeyPointCommand(object obj)
        {
            if (SelectedKeyPoint != null)
            {
                if(SelectedKeyPoint.Id == Tour.ActiveKeyPointId)
                {
                    if (SelectedKeyPoint.Type != KeyPointType.Last)
                    {
                        tourService.UpdateActiveKeyPoint(Tour, SelectedKeyPoint.Id + 1);
                        keyPointService.Finish(SelectedKeyPoint);
                    }
                    else
                    {

                        keyPointService.Finish(SelectedKeyPoint);
                        tourService.UpdateTourState(Tour, TourState.Finished);
                        foreach (Guest2 guest in WaitingGuests)
                        {
                            reservationTourService.UpdateGuestState(guest.Id, Tour, Guest2State.NotPresent);
                        }
                        NavService.Navigate(new AvailableToursView(Guide));
                    }
                }
            }
        }
        private void Execute_FinishTourCommand(object obj)
        {
            tourService.UpdateTourState(Tour, TourState.Finished);
            foreach (Guest2 guest in WaitingGuests)
            {
                reservationTourService.UpdateGuestState(guest.Id, Tour, Guest2State.NotPresent);
            }
            NavService.Navigate(new AvailableToursView(Guide));
        }
        private void Execute_BackCommand(object obj)
        {
            NavService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void UpdateKeyPointList()
        {
            FinishedKeyPoints.Clear();
            foreach (var keyPoint in keyPointService.GetKeyPointsByStateAndIds(Tour.KeyPointIds, true))
            {
                FinishedKeyPoints.Add(keyPoint);
            }
            UnFinishedKeyPoints.Clear();
            foreach (var keyPoint in keyPointService.GetKeyPointsByStateAndIds(Tour.KeyPointIds, false))
            {
                UnFinishedKeyPoints.Add(keyPoint);
            }
        }
        public void Update()
        {
            UpdateKeyPointList();
        }
    }
}

