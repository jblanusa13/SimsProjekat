using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using ProjectSims.Observer;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourTrackingView.xaml
    /// </summary>
    public partial class TourTrackingView : Page,IObserver
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
        private Tour Tour { get; set; }
        public Guide Guide { get; set; }
        public TourTrackingView(Tour startedTour, Guide guide)
        {
            InitializeComponent();
            DataContext = this;
            tourService = new TourService();
            keyPointService = new KeyPointService();
            keyPointService.Subscribe(this);
            guest2Controller = new Guest2Service();
            reservationTourService = new ReservationTourService();
            reservationTourService.Subscribe(this);
            Tour = startedTour;
            Guide = guide;
            UnFinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.GetKeyPointsByStateAndIds(Tour.KeyPointIds,false));
            FinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.GetKeyPointsByStateAndIds(Tour.KeyPointIds, true));
            WaitingGuests = new List<Guest2>();
            PresentGuests = new List<Guest2>();
            foreach (int id in reservationTourService.GetGuestIdsByTourAndState(Tour,Guest2State.Waiting))
            {
                WaitingGuests.Add(guest2Controller.GetGuestById(id));
            }
            foreach (int id in reservationTourService.GetGuestIdsByTourAndState(Tour, Guest2State.ActiveTour))
            {
                WaitingGuests.Add(guest2Controller.GetGuestById(id));
            }
            foreach (int id in reservationTourService.GetGuestIdsByTourAndState(Tour, Guest2State.Present))
            {
                PresentGuests.Add(guest2Controller.GetGuestById(id));
            }
            TourInfoLabel.Content = Tour.Name + "," + Tour.StartOfTheTour.ToString("dd.MM.yyyy HH:mm");
        }
        private void KeyPointSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyPoint keyPoint = KeyPointListView.SelectedItem as KeyPoint;
            if (keyPoint != null)
            {
                if (keyPoint.Id == Tour.ActiveKeyPointId)
                {
                    if (keyPoint.Type != KeyPointType.Last)
                    {
                        keyPointService.Finish(SelectedKeyPoint);
                        tourService.UpdateActiveKeyPoint(Tour, keyPoint.Id + 1);
                    }
                    else
                    {
                        keyPointService.Finish(SelectedKeyPoint);
                        tourService.UpdateTourState(Tour, TourState.Finished);
                        foreach(Guest2 guest in WaitingGuests)
                        {
                            reservationTourService.UpdateGuestState(guest,Tour,Guest2State.NotPresent);
                        }
                        NavigationService.Navigate(null);
                    }
                }
                else
                {
                    MessageBox.Show("Preskočena stanica!");
                }

            }
        }
        private void NotifyGuest_Click(object sender, RoutedEventArgs e)
        {
            SelectedGuest = ((FrameworkElement)sender).DataContext as Guest2;
            if (SelectedGuest != null)
            {
                reservationTourService.UpdateGuestState(SelectedGuest, Tour,Guest2State.Waiting);
            }
            else
            {
                MessageBox.Show("Odaberite gosta!");
            }
        }
        private void FinishTour_Click(object sender, RoutedEventArgs e)
        {
            tourService.UpdateTourState(Tour, TourState.Finished);
            foreach (Guest2 guest in WaitingGuests)
            {
                reservationTourService.UpdateGuestState(guest, Tour, Guest2State.NotPresent);
            }
            NavigationService.Navigate(null);

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(this.Parent);
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
