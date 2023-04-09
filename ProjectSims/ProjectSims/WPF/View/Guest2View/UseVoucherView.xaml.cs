using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Service;
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

namespace ProjectSims.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for UseVoucherView.xaml
    /// </summary>
    public partial class UseVoucherView : Window
    {
        public Guest2 guest2 { get; set; }
        public Tour tour { get; set; }
        public int numberGuests { get; set; }
        public Voucher SelectedVoucher { get; set; }
        public ObservableCollection<Voucher> ListVoucher { get; set; }

        private VoucherRepository voucherRepository;

        private TourService tourService;

        private ReservationTourService reservationTourService;
        public UseVoucherView(Guest2 g, Tour t, int numGuests)
        {
            InitializeComponent();
            DataContext = this;
            guest2 = g;
            tour = t;
            numberGuests = numGuests;
            voucherRepository = new VoucherRepository();
            tourService = new TourService();
            reservationTourService = new ReservationTourService();

            ListVoucher = new ObservableCollection<Voucher>(voucherRepository.GetActiveVouchersWithIds(guest2.VoucherIds));
        }

        private void ReservationClick(object sender, RoutedEventArgs e)
        {
            ReservationTour reservation = new ReservationTour();
            if (SelectedVoucher != null)
            {
                SelectedVoucher.Used = true;
                voucherRepository.Update(SelectedVoucher);
                reservation = new ReservationTour(tour.Id, numberGuests, guest2.Id, -1, true, false);
            }
            else
            {
                reservation = new ReservationTour(tour.Id, numberGuests, guest2.Id, -1, false, false);
            }
            reservationTourService.Create(reservation);
            tour.AvailableSeats -= numberGuests;
            tourService.Update(tour);
            MessageBox.Show("Reservation successful! \nUser " + guest2.Name + " " + guest2.Surname +
                " has made a reservation for " + numberGuests.ToString() + " people on the tour " + tour.Name + ".");
            Close();

        }
    }
}
