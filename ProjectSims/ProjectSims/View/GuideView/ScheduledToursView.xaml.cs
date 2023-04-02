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
using System.Windows.Shapes;
using ProjectSims.Observer;

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for ScheduledToursView.xaml
    /// </summary>
    public partial class ScheduledToursView : Window, IObserver
    {
        private TourController tourController;
        private ReservationTourController reservationTourController;
        private Guest2Controller guest2Controller;
        private VoucherController voucherController;
        public ObservableCollection<Tour> ScheduledTours { get; set; }
        public Tour SelectedTour { get; set; }
        public Guide guide { get; set; }
        public ScheduledToursView(Guide g)
        {
            InitializeComponent();
            DataContext = this;
            tourController = new TourController();
            reservationTourController = new ReservationTourController();
            guest2Controller = new Guest2Controller();
            tourController.Subscribe(this);
            guide = g;
            ScheduledTours = new ObservableCollection<Tour>(tourController.GetScheduledTours(guide.Id));
            SelectedTour = new Tour();
        }
       
        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                if (tourController.StartsInLessThan48Hours(SelectedTour))
                {
                    MessageBox.Show("Tura pocinje za manje od 48 sati i ne moze se otkazati!");
                    return;
                }

                MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da otkazete turu?", "", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    {
                        SelectedTour.State = TourState.Cancelled;
                        List<int> guestIds = reservationTourController.FindTourGuestIds(SelectedTour.Id);
                        if(guestIds.Count > 0)
                        {
                            foreach (int guest2Id in guestIds)
                            {
                                guest2Controller.GiveVoucher(guest2Id);
                            }
                        }
                        tourController.Update(SelectedTour);

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
            foreach (var tour in tourController.GetScheduledTours(guide.Id))
            {
                ScheduledTours.Add(tour);
            }
        }
    }

}
