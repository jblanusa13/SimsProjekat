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
    /// Interaction logic for TourDetailsAndCancelling.xaml
    /// </summary>
    public partial class TourDetailsAndCancelling : Page
    {
        public Tour SelectedTour { get; set; }
        private TourService tourService;
        private ReservationTourService reservationService;
        private Guest2Service guest2Service;
        private KeyPointService keyPointService;
        public List<KeyPoint> KeyPoints { get; set; }
        public TourDetailsAndCancelling(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;
            tourService = new TourService();
            reservationService = new ReservationTourService();
            guest2Service = new Guest2Service();
            KeyPoints = new List<KeyPoint>();
            keyPointService = new KeyPointService();
            foreach (int keyPointId in selectedTour.KeyPointIds)
            {
                KeyPoints.Add(keyPointService.GetKeyPointById(keyPointId));
            }
            if( (SelectedTour.StartOfTheTour - DateTime.Now).TotalHours <= 48)
            {
                Cancel_Button.IsEnabled = false;
            }
            TitleLabel.Content = SelectedTour.Name + "," + SelectedTour.StartOfTheTour.ToString("dd.MM.yyyy HH:mm");
        }
        public void CancelTour_Click(object sender, EventArgs e)
        {
            tourService.UpdateTourState(SelectedTour, TourState.Cancelled);
            List<int> guestIds = reservationService.GetGuestIdsByTourAndState(SelectedTour, Guest2State.InactiveTour);
            if (guestIds.Count > 0)
                guestIds.ForEach(id => guest2Service.GiveVoucher(id,1));
            this.NavigationService.GoBack();
        }
        public void Back_Click(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
