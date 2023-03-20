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
using System.Windows.Shapes;
using ProjectSims.Controller;
using ProjectSims.Model;
using ProjectSims.Observer;

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for TourTrackingView.xaml
    /// </summary>
    public partial class TourTrackingView : Window, IObserver
    {
        private KeyPointController keyPointController;
        private TourController tourController;
        private Guest2Controller guest2Controller;
        private ReservationTourController reservationTourController;
        public ObservableCollection<KeyPoint> UnFinishedKeyPoints { get; set; }
        public ObservableCollection<KeyPoint> FinishedKeyPoints { get; set; }
        public ObservableCollection<Guest2> PresentGuests { get; set; }
        public ObservableCollection<Guest2> WaitingGuests { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        private Tour tour { get; set; }
        private int expectedId { get; set; }
        public TourTrackingView(Tour startedTour)
        {
            InitializeComponent();
            DataContext = this;

            tourController = new TourController();
            keyPointController = new KeyPointController();
            keyPointController.Subscribe(this);
            guest2Controller= new Guest2Controller();
            guest2Controller.Subscribe(this);
            reservationTourController= new ReservationTourController();
            
            tour = startedTour;
            expectedId = tour.KeyPointIds[1];

            UnFinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointController.FindUnFinishedKeyPointsByIds(tour.KeyPointIds));
            FinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointController.FindFinishedKeyPointsByIds(tour.KeyPointIds));
            List<int> GuestIds=reservationTourController.FindGuestIdsByTourId(tour.Id);
            WaitingGuests = new ObservableCollection<Guest2>();
            foreach(int id in GuestIds)
            {
                WaitingGuests.Add(guest2Controller.FindGuest2ById(id));
            }

            PresentGuests= new ObservableCollection<Guest2>();            
            TourInfoTextBox.Text = tour.Name + "," + tour.StartOfTheTour.ToString("dd.MM.yyyy HH:mm");
        }
        
        
        private void KeyPointSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var keyPoint = KeyPointListView.SelectedItem as KeyPoint;
            if (keyPoint != null)
            {
                if(expectedId == keyPoint.Id)
                {
                    if(keyPoint.Type != KeyPointType.Last)
                    {
                        keyPointController.Finish(SelectedKeyPoint);
                        expectedId++;
                    }
                    else
                    {
                        tourController.FinishTour(tour);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Preskočena stanica!");
                }

            }
        }
        private void GuestSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var guest2 = WaitingGuestsListView.SelectedItem as Guest2;
            if (guest2 != null)
            {
                guest2Controller.CheckGuest(guest2);
            }
        }

        private void FinishTour_Click(object sender, RoutedEventArgs e)
        {
            tourController.FinishTour(tour);
            Close();
        }
        private void UpdateKeyPointList()
        {
            FinishedKeyPoints.Clear();
            foreach (var keyPoint in keyPointController.FindFinishedKeyPointsByIds(tour.KeyPointIds))
            {
                FinishedKeyPoints.Add(keyPoint);
            }
            UnFinishedKeyPoints.Clear();
            foreach (var keyPoint in keyPointController.FindUnFinishedKeyPointsByIds(tour.KeyPointIds))
            {
                UnFinishedKeyPoints.Add(keyPoint);
            }
        }
        private void UpdateGuestList()
        {
            WaitingGuests.Clear();
            PresentGuests.Clear();
            List<int> GuestIds = reservationTourController.FindGuestIdsByTourId(tour.Id);
            WaitingGuests = new ObservableCollection<Guest2>();
            foreach(int id in GuestIds)
            {
                var guest2 = guest2Controller.FindGuest2ById(id);
                if(guest2.State == Guest2State.Present)
                {
                    PresentGuests.Add(guest2);
                }
                else
                {
                    WaitingGuests.Add(guest2);
                }
            }
        }

        public void Update()
        {
            UpdateKeyPointList();
            UpdateGuestList();
        }

    }
}
