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
        public AvailableToursView()
        {
            InitializeComponent();
            DataContext = this;

            tourController = new TourController();
            tourController.Subscribe(this);
            AvailableTours = new ObservableCollection<Tour>(tourController.GetAvailableTours());
        }
        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour != null)
            {
                Tour startedTour = (Tour)SelectedTour;
                if (tourController.StartTour(SelectedTour))
                {
                    TourTrackingView tourTrackingView = new TourTrackingView(startedTour);
                    tourTrackingView.Show();
                }
                else
                {
                    MessageBox.Show("Već postoji započeta tura!");
                }
            }
            else
            {
                MessageBox.Show("Odaberite turu!");
            }
        }
        private void UpdateAvailableTours()
        {
            AvailableTours.Clear();
            foreach (var tour in tourController.GetAvailableTours())
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
