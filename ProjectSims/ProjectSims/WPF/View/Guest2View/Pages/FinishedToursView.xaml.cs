using ProjectSims.Service;
using ProjectSims.Domain.Model;
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

namespace ProjectSims.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for FinishedToursView.xaml
    /// </summary>
    public partial class FinishedToursView : Page
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;

        public ObservableCollection<Tour> ListTour { get; }
        public Tour SelectedTour { get; set; }
        public Guest2 guest2 { get; set; }

        public FinishedToursView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            guest2 = g;
            tourService = new TourService();
            reservationTourService = new ReservationTourService();

            

            ListTour = new ObservableCollection<Tour>(GetToursWhichFinishedWhereGuestPresent());
        }

        public List<Tour> GetToursWhichFinishedWhereGuestPresent()
        {
            List<Tour> toursFinished = new List<Tour>();
            foreach (int id in reservationTourService.FindTourIdsWhereGuestPresent(guest2.Id))
            {
                Tour tour = tourService.GetFinishedTourById(id);
                if (tour != null)
                {
                    toursFinished.Add(tour);
                }
            }
            return toursFinished;
        }

        private void ButtonRatingTour(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                var ratingTourWindow = new RatingTourView(SelectedTour,guest2);
                ratingTourWindow.Show();
            }
            else
            {
                MessageBox.Show("You must select a tour for rating!");
            }
        }
    }
}
