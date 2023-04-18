using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public partial class CommentAndRatingsViewModel
    {
        private KeyPointService keyPointService;
        private ReservationTourService reservationTourService;
        private Guest2Service guestService; 
        private TourRatingService ratingService;
        public TourAndGuideRating TourRating { get; set; }
        public Guest2 Guest { get; set; }
        public ReservationTour ReservationTour { get; set; }
        public KeyPoint KeyPoint { get; set; }
        private Tour SelectedTour { get; set; }
        public CommentAndRatingsViewModel(TourAndGuideRating tourRating, Tour tour)
        {
            keyPointService = new KeyPointService();
            guestService = new Guest2Service();
            reservationTourService = new ReservationTourService();
            ratingService = new TourRatingService();
            TourRating = tourRating;
            SelectedTour = tour;
            Guest = guestService.GetGuestById(tourRating.GuestId);
            ReservationTour = reservationTourService.GetReservationByGuestAndTour(SelectedTour, Guest);
            KeyPoint = keyPointService.GetKeyPointById(ReservationTour.KeyPointWhereGuestArrivedId);
        }
        public void ReportComment()
        {
            ratingService.ReportRating(TourRating);
        }
    }
}
