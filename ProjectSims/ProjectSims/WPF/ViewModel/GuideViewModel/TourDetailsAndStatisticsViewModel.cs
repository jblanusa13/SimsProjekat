using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public partial class TourDetailsAndStatisticsViewModel
    {
        private KeyPointService keyPointService;
        private ReservationTourService reservationTourService;
        public int NumberOfPresentGuestsUnder18 { get; set; }
        public int NumberOfPresentGuestsBetween18And50 { get; set; }
        public int NumberOfPresentGuestsOver50 { get; set; }
        public int NumberOfPresentGuests { get; set; }
        public double PercentageOfGuestsWithVoucher { get; set; }
        public double PercentageOfGuestsWithoutVoucher { get; set; }
        public Tour SelectedTour { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public TourDetailsAndStatisticsViewModel(Tour selectedTour)
        {
            keyPointService = new KeyPointService();
            reservationTourService = new ReservationTourService();
            SelectedTour = selectedTour;
            NumberOfPresentGuestsUnder18 = reservationTourService.GetNumberOfPresentGuestsByAgeLimit(SelectedTour, 0);
            NumberOfPresentGuestsBetween18And50 = reservationTourService.GetNumberOfPresentGuestsByAgeLimit(SelectedTour, 18);
            NumberOfPresentGuestsOver50 = reservationTourService.GetNumberOfPresentGuestsByAgeLimit(SelectedTour, 50);
            NumberOfPresentGuests = NumberOfPresentGuestsUnder18 + NumberOfPresentGuestsBetween18And50 + NumberOfPresentGuestsOver50;
            PercentageOfGuestsWithVoucher = reservationTourService.GetPercentageOfPresentGuestsWithVoucher(SelectedTour);
            PercentageOfGuestsWithoutVoucher = 100 - reservationTourService.GetPercentageOfPresentGuestsWithVoucher(SelectedTour);
            KeyPoints = new List<KeyPoint>();
            foreach (int keyPointId in SelectedTour.KeyPointIds)
            {
                KeyPoints.Add(keyPointService.GetKeyPointById(keyPointId));
            }
        }
    }
}
