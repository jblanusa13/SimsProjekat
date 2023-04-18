using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.View.Guest2View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class FinishedToursViewModel:IObserver
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;

        public ObservableCollection<Tour> ListTour { get; set; }
        public Tour SelectedTour { get; set; }
        public Guest2 guest2 { get; set; }
        public FinishedToursViewModel(Guest2 g)
        {
            guest2 = g;
            tourService = new TourService();
            tourService.Subscribe(this);
            reservationTourService = new ReservationTourService();
            reservationTourService.Subscribe(this);
            ListTour = new ObservableCollection<Tour>(tourService.GetToursWhichFinishedWhereGuestPresent(guest2.Id));
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
        public void ButtonRatingTour(object sender)
        {
            if (SelectedTour != null)
            {
                var ratingTourWindow = new RatingTourView(SelectedTour, guest2, reservationTourService);
                ratingTourWindow.Show();
            }
            else
            {
                MessageBox.Show("You must select a tour for rating!");
            }
        }
    }
}
