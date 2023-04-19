using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest2View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class ActivatedToursViewModel
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;
        public ObservableCollection<Tour> ListTour { get; set; }
        public Tour SelectedTour { get; set; }
        public Guest2 guest2 { get; set; }
        public ActivatedToursViewModel(Guest2 g)
        {
            guest2 = g;
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            ListTour = new ObservableCollection<Tour>(tourService.GetToursWhichActivatedWhereGuestPresent(guest2.Id));
        }

        public void ButtonTrackingTour(object sender)
        {
            if (SelectedTour != null)
            {
                Guest2TrackingTourViewModel guest2TrackingTourViewModel = new Guest2TrackingTourViewModel(SelectedTour);
                var ratingTourWindow = new Guest2TrackingTourView(guest2TrackingTourViewModel);
                ratingTourWindow.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati turu koju zelite da pratite!");
            }
        }
    }
}
