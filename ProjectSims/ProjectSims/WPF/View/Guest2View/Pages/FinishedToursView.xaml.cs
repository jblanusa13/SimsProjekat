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
using ProjectSims.Observer;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for FinishedToursView.xaml
    /// </summary>
    public partial class FinishedToursView : Page, IObserver
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;

        public ObservableCollection<Tour> ListTour { get; set; }
        public Tour SelectedTour { get; set; }
        public Guest2 guest2 { get; set; }

        public FinishedToursView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            guest2 = g;
            tourService = new TourService();
            tourService.Subscribe(this);
            reservationTourService = new ReservationTourService();
            reservationTourService.Subscribe(this);
            ListTour = new ObservableCollection<Tour>(tourService.GetToursWhichFinishedWhereGuestPresent(guest2.Id));
        }

        private void ButtonRatingTour(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                var ratingTourWindow = new RatingTourView(SelectedTour,guest2,reservationTourService);
                ratingTourWindow.Show();
            }
            else
            {
                MessageBox.Show("You must select a tour for rating!");
            }
        }
        private void UpdateListTour()
        {
            ListTour.Clear();
            foreach (var tour in tourService.GetToursWhichFinishedWhereGuestPresent(guest2.Id))
            {
                ListTour.Add(tour);
            }
        }
        public void Update()
        {
            UpdateListTour();
        }
    }
}
