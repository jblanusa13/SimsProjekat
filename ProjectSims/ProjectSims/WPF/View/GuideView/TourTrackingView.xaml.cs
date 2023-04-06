using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectSims.Service;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;

namespace ProjectSims.View.GuideView
{
    /// <summary>
    /// Interaction logic for TourTrackingView.xaml
    /// </summary>
    public partial class TourTrackingView : Window, IObserver
    {
        private KeyPointService keyPointService;
        private TourService tourService;
        private Guest2Service guest2Controller;
        private ReservationTourService reservationTourService;
        public ObservableCollection<KeyPoint> UnFinishedKeyPoints { get; set; }
        public ObservableCollection<KeyPoint> FinishedKeyPoints { get; set; }
        public List<Guest2> WaitingGuests { get; set; }
        public List<Guest2> PresentGuests { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        public Guest2 SelectedGuest { get; set; }
        private Tour tour { get; set; }
        public Guide guide { get; set; }
        private int expectedId { get; set; }
        public TourTrackingView(Tour startedTour, Guide g)
        {
            InitializeComponent();
            DataContext = this;

            tourService = new TourService();
            keyPointService = new KeyPointService();
            keyPointService.Subscribe(this);
            guest2Controller = new Guest2Service();
            reservationTourService = new ReservationTourService();
            reservationTourService.Subscribe(this);
            tour = startedTour;
            guide = g;
            UnFinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.FindUnFinishedKeyPointsByIds(tour.KeyPointIds));
            FinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.FindFinishedKeyPointsByIds(tour.KeyPointIds));
            WaitingGuests = new List<Guest2>();
            PresentGuests = new List<Guest2>();
            expectedId = keyPointService.FindUnFinishedKeyPointsByIds(tour.KeyPointIds).First().Id;
            reservationTourService.InviteGuests(tour.Id);

            foreach (int id in reservationTourService.FindWaitingAndInvitedGuestIdsByTourId(tour.Id))
            {
                WaitingGuests.Add(guest2Controller.FindGuest2ById(id));
            }
            foreach (int id in reservationTourService.FindPresentGuestIdsByTourId(tour.Id))
            {
                PresentGuests.Add(guest2Controller.FindGuest2ById(id));
            }
            TourInfoTextBox.Text = tour.Name + "," + tour.StartOfTheTour.ToString("dd.MM.yyyy HH:mm");
        }
        private void KeyPointSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var keyPoint = KeyPointListView.SelectedItem as KeyPoint;
            if (keyPoint != null)
            {
                if(keyPoint.Id == expectedId)
                {
                    if(keyPoint.Type != KeyPointType.Last)
                    {
                        expectedId++;
                        keyPointService.Finish(SelectedKeyPoint);
                        tourService.UpdateActiveKeyPoint(tour.Id, expectedId);
                    }
                    else
                    {
                        keyPointService.Finish(SelectedKeyPoint);
                        tourService.FinishTour(tour, PresentGuests);
                        reservationTourService.FinishTour(tour.Id);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Preskočena stanica!");
                }

            }
        }
        private void GuestSelect_Click(object sender, RoutedEventArgs e)
        {
            Guest2 guest = (Guest2)SelectedGuest;
            if (guest != null)
            {
                reservationTourService.NotifyGuest(guest.Id, tour.Id);             
            }
            else
            {
                MessageBox.Show("Odaberite gosta!");
            }
        }

        private void FinishTour_Click(object sender, RoutedEventArgs e)
        {
            tourService.FinishTour(tour,PresentGuests);
            reservationTourService.FinishTour(tour.Id);
            Close();

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
            AvailableToursView availableToursView = new AvailableToursView(guide);
            availableToursView.Show();
        }
        private void Forward_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Close();
            GuideStartingView guideStartingView = new GuideStartingView(guide);
            guideStartingView.Show();
        }
        private void UpdateKeyPointList()
        {
            FinishedKeyPoints.Clear();
            foreach (var keyPoint in keyPointService.FindFinishedKeyPointsByIds(tour.KeyPointIds))
            {
                FinishedKeyPoints.Add(keyPoint);
            }
            UnFinishedKeyPoints.Clear();
            foreach (var keyPoint in keyPointService.FindUnFinishedKeyPointsByIds(tour.KeyPointIds))
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
