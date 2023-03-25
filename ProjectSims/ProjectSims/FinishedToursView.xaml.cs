using ProjectSims.Controller;
using ProjectSims.Model;
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

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for FinishedToursView.xaml
    /// </summary>
    public partial class FinishedToursView : Page
    {
        private TourController tourController;
        private ReservationTourController reservationTourController;

        public ObservableCollection<Tour> ListTour { get; }
        public Tour SelectedTour { get; set; }
        public Guest2 guest2 { get; set; }

        public FinishedToursView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            guest2 = g;
            tourController = new TourController();
            reservationTourController = new ReservationTourController();

            

            ListTour = new ObservableCollection<Tour>(GetToursWhichFinishedWhereGuestPresent());
        }

        public List<Tour> GetToursWhichFinishedWhereGuestPresent()
        {
            List<Tour> toursFinished = new List<Tour>();
            foreach (int id in reservationTourController.FindTourIdsWhereGuestPresent(guest2.Id))
            {
                Tour tour = tourController.GetFinishedTourById(id);
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
                var ratingTourWindow = new RatingTourView(SelectedTour);
                ratingTourWindow.Show();
            }
            else
            {
                MessageBox.Show("You must select a tour for rating!");
            }
        }
    }
}
