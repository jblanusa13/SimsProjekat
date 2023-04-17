using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
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

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for CommentAndRatingsView.xaml
    /// </summary>
    public partial class CommentAndRatingsView : Page
    {
        private KeyPointService keyPointService;
        private ReservationTourService reservationTourService;
        private TourRatingService ratingService;
        private Guest2Service guestService;
        private TourAndGuideRating TourRating { get; set; }
        private Guest2 Guest { get; set; }
        private ReservationTour ReservationTour { get; set; }
        private KeyPoint KeyPoint { get; set; }

        public CommentAndRatingsView(TourAndGuideRating tourRating)
        {
            InitializeComponent();
            DataContext = this;
            keyPointService = new KeyPointService();
            guestService = new Guest2Service();
            reservationTourService = new ReservationTourService();
            ratingService = new TourRatingService();
            TourRating = tourRating;
            ReservationTour = reservationTourService.GetReservationByGuestAndTourId(TourRating.TourId, TourRating.GuestId);
            Guest = guestService.GetGuestById(tourRating.GuestId);
            KeyPoint = keyPointService.GetKeyPointById(ReservationTour.KeyPointWhereGuestArrivedId);

            UserTextBox.Text = Guest.Name + " " + Guest.Surname;
            KeyPointTextBox.Text = KeyPoint.Name;
            KnowledgeTextBox.Text = TourRating.KnowledgeGuide.ToString();
            LanguageTextBox.Text = TourRating.LanguageGuide.ToString();
            InterestingTextBox.Text = TourRating.InterestingTour.ToString();
            CommentTextBox.Text = TourRating.AddedComment;


        }
        public void ReportComment_Click(object sender, RoutedEventArgs e)
        {

        }
        public void AcceptComment_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
