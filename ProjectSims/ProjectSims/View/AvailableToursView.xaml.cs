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
    public partial class AvailableToursView : Window, IObserver
    {
        private TourController tourController;
        public ObservableCollection<Tour> AvailableTours { get; set; }
        public Tour SelectedTour { get; set; }
        public Guide guide { get; set; }
        public AvailableToursView(Guide g)
        {
            InitializeComponent();
            DataContext = this;

            tourController = new TourController();
            tourController.Subscribe(this);
            guide = g;
            AvailableTours = new ObservableCollection<Tour>(tourController.GetAvailableTours(guide.Id));
        }
        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour != null)
            {
                Tour selectedTour = (Tour)SelectedTour;
                if (tourController.StartTour(SelectedTour))
                {
                    TourTrackingView tourTrackingView = new TourTrackingView(selectedTour,guide);
                    tourTrackingView.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Već postoji započeta tura");                   
                }
            }
            else
            {
                MessageBox.Show("Odaberite turu!");
            }
        }
        private void StartedTour_Click(Object sender, RoutedEventArgs e)
        {
            Tour startedTour = tourController.FindStartedTour();
            if(startedTour != null)
            {
                TourTrackingView tourTrackingView = new TourTrackingView(startedTour,guide);
                tourTrackingView.Show();
            }
            else
            {
                MessageBox.Show("Nijedna tura nije zapoceta!");
            }

        }
        private void UpdateAvailableTours()
        {
            AvailableTours.Clear();
            foreach (var tour in tourController.GetAvailableTours(guide.Id))
            {
                AvailableTours.Add(tour);
            }
        }
        public void Update()
        {
            UpdateAvailableTours();
        }
    }
}
