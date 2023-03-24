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
        public ObservableCollection<Guest2> WaitingGuests { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        public Guest2 SelectedGuest { get; set; }
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
            reservationTourController= new ReservationTourController();
            reservationTourController.Subscribe(this);
            
            tour = startedTour;

           
            UnFinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointController.FindUnFinishedKeyPointsByIds(tour.KeyPointIds));
            FinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointController.FindFinishedKeyPointsByIds(tour.KeyPointIds));
            WaitingGuests = new ObservableCollection<Guest2>();
            expectedId = tour.KeyPointIds[1];
            reservationTourController.InviteGuests(tour.Id);

            foreach (int id in reservationTourController.FindGuestIdsByTourIdAndState(tour.Id,Guest2State.Invited))
            {
                WaitingGuests.Add(guest2Controller.FindGuest2ById(id));
            }
            foreach (int id in reservationTourController.FindGuestIdsByTourIdAndState(tour.Id, Guest2State.Waiting))
            {
                WaitingGuests.Add(guest2Controller.FindGuest2ById(id));
            }
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
                        keyPointController.Finish(SelectedKeyPoint);
                        tourController.FinishTour(tour);
                        reservationTourController.FinishTour(tour.Id);
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
                reservationTourController.NotifyGuest(guest.Id, tour.Id);             
            }
            else
            {
                MessageBox.Show("Odaberite gosta!");
            }
        }

        private void FinishTour_Click(object sender, RoutedEventArgs e)
        {
            tourController.FinishTour(tour);
            reservationTourController.FinishTour(tour.Id);
            Close();

        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var startView = new MainWindow();
            startView.Show();
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
            foreach (int id in reservationTourController.FindGuestIdsByTourIdAndState(tour.Id,Guest2State.Waiting))
            {
                WaitingGuests.Add(guest2Controller.FindGuest2ById(id));
            }
            foreach (int id in reservationTourController.FindGuestIdsByTourIdAndState(tour.Id, Guest2State.Invited))
            {
                WaitingGuests.Add(guest2Controller.FindGuest2ById(id));
            }
        }

        public void Update()
        {
            UpdateKeyPointList();
            //UpdateGuestList();
        }

    }
}
