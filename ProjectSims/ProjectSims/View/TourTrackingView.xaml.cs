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
        public ObservableCollection<KeyPoint> UnFinishedKeyPoints { get; set; }
        public ObservableCollection<KeyPoint> FinishedKeyPoints { get; set; }
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
            tour = startedTour;
            UnFinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointController.FindUnFinishedKeyPointsByIds(tour.KeyPointIds));
            FinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointController.FindFinishedKeyPointsByIds(tour.KeyPointIds));
            expectedId = tour.KeyPointIds[1];
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

        public void Update()
        {
            UpdateKeyPointList();
        }

    }
}
