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
using System.Data;
using System.Xml.Linq;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for AvailableToursView.xaml
    /// </summary>
    public partial class AvailableToursView : Page, IObserver
    {
        private TourService tourService;
        public ObservableCollection<Tour> AvailableTours { get; set; }
        public Tour SelectedTour { get; set; }
        public Guide guide { get; set; }
        public AvailableToursView(Guide g)
        {
            InitializeComponent();
            DataContext = this;

            tourService = new TourService();
            tourService.Subscribe(this);
            guide = g;
            AvailableTours = new ObservableCollection<Tour>(tourService.GetAvailableTours(guide.Id));
        }
        private void StartTour_Click(object sender, RoutedEventArgs e)
        {
            Tour selectedTour = ((FrameworkElement)sender).DataContext as Tour;
            if (tourService.StartTour(selectedTour))
            {
                //TourTrackingView tourTrackingView = new TourTrackingView(sel, guide);
                //tourTrackingView.Show();
                //Close();
            }
            else
            {
                MessageBox.Show("Već postoji započeta tura");
            }
                 
        }
        private void StartedTour_Click(Object sender, RoutedEventArgs e)
        {
            Tour startedTour = tourService.FindStartedTour();
            if (startedTour != null)
            {
                TourTrackingView tourTrackingView = new TourTrackingView(startedTour, guide);
                //tourTrackingView.Show();
               // Close();
            }
            else
            {
                MessageBox.Show("Nijedna tura nije zapoceta!");
            }

        }
        private void UpdateAvailableTours()
        {
            AvailableTours.Clear();
            foreach (var tour in tourService.GetAvailableTours(guide.Id))
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
