using ProjectSims.Service;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.View.Guest2View;
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

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for SearchTourView.xaml
    /// </summary>
    public partial class SearchTourView : Page, IObserver
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;

        public ObservableCollection<Tour> ListTour { get; set; }
        public Tour SelectedTour { get; set; }

        public Guest2 guest2 { get; set; }

        public SearchTourView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;

            tourService = new TourService();
            tourService.Subscribe(this);

            reservationTourService = new ReservationTourService();
            ListTour = new ObservableCollection<Tour>(tourService.GetAllTours());
            guest2 = g;

        }
        private void UpdateTourList()
        {
            ListTour.Clear();
            foreach (var tour in tourService.GetAllTours())
            {
                ListTour.Add(tour);
            }
        }

        public void Update()
        {
            UpdateTourList();
        }

        private void See_More_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                var see_more = new DetailsAndReservationTourView(SelectedTour, guest2);
                see_more.Show();
            }
            else
            {
                MessageBox.Show("You must select a tour for more details!");
            }

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            String location = LocationTextBox.Text;
            String language = LanguageTextBox.Text;

            double durationStart = tourService.ConvertToDouble(DurationStartTextBox.Text);
            if (durationStart == -2) return;

            double durationEnd = tourService.ConvertToDouble(DurationEndTextBox.Text);
            if (durationEnd == -2) return;

            //both duration fields must be entered
            if ((durationStart == -1 && durationEnd != -1) || (durationStart != -1 && durationEnd == -1))
            {
                MessageBox.Show("Both duration fields must be entered for search tours!");
                return;
            }
            else if (durationStart != -1 && durationEnd != -1)
            //if both fields are entered the first must be less than the second
            {
                if (durationStart > durationEnd)
                {
                    MessageBox.Show("The first duration fields must be less than the second!");
                    return;
                }
            }

            int numberGuests = tourService.ConvertToInt(NumberGuestsTextBox.Text);
            if (numberGuests == -2) return;

            if (location == "" && durationStart == -1 && language == "" && numberGuests == -1)         //16. case (nothing entered)
            {
                MessageBox.Show("You must enter some information for search!");
                ListTour.Clear();
                foreach (var tour in tourService.GetAllTours())
                {
                    ListTour.Add(tour);
                }
                return;
            }
            List<Tour> wantedTours = tourService.SearchTours(location, durationStart, durationEnd, language, numberGuests);
            ListTour.Clear();
            foreach (Tour tour in wantedTours)
            {
                ListTour.Add(tour);
            }
        }
        
    }
}
