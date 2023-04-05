using ProjectSims.Service;
using ProjectSims.Domain.Model;
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
using System.Windows.Shapes;
using ProjectSims.Observer;

namespace ProjectSims.View.GuideView
{
    /// <summary>
    /// Interaction logic for ScheduledToursView.xaml
    /// </summary>
    public partial class ScheduledToursView : Window, IObserver
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;
        private Guest2Service guest2Controller;
        private VoucherService voucherController;
        public ObservableCollection<Tour> ScheduledTours { get; set; }
        public Tour SelectedTour { get; set; }
        public Guide guide { get; set; }
        public ScheduledToursView(Guide g)
        {
            InitializeComponent();
            DataContext = this;
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            guest2Controller = new Guest2Service();
            tourService.Subscribe(this);
            guide = g;
            ScheduledTours = new ObservableCollection<Tour>(tourService.GetScheduledTours(guide.Id));
            SelectedTour = new Tour();
        }
       
        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                if (tourService.StartsInLessThan48Hours(SelectedTour))
                {
                    MessageBox.Show("Tura pocinje za manje od 48 sati i ne moze se otkazati!");
                    return;
                }

                MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da otkazete turu?", "", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    {
                        SelectedTour.State = TourState.Cancelled;
                        List<int> guestIds = reservationTourService.FindTourGuestIds(SelectedTour.Id);
                        if(guestIds.Count > 0)
                        {
                            foreach (int guest2Id in guestIds)
                            {
                                guest2Controller.GiveVoucher(guest2Id);
                            }
                        }
                        tourService.Update(SelectedTour);

                    }

                }
            }
            else
            {
                MessageBox.Show("Odaberite turu!");
            }
        }

        public void Update()
        {
            ScheduledTours.Clear();
            foreach (var tour in tourService.GetScheduledTours(guide.Id))
            {
                ScheduledTours.Add(tour);
            }
        }
    }

}
