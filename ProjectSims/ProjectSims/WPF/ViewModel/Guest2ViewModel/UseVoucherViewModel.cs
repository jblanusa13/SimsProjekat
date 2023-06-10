using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class UseVoucherViewModel
    {
        public Guest2 guest2 { get; set; }
        public Tour tour { get; set; }
        public int numberGuests { get; set; }
        public Voucher SelectedVoucher { get; set; }
        public ObservableCollection<Voucher> ListVoucher { get; set; }

        private VoucherService voucherService;

        private TourService tourService;

        private ReservationTourService reservationTourService;
        private Guest2Service guest2Service;

        public UseVoucherViewModel(Guest2 g, Tour t, int numGuests)
        {
            guest2 = g;
            tour = t;
            numberGuests = numGuests;
            voucherService = new VoucherService();
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            guest2Service = new Guest2Service();
            ListVoucher = new ObservableCollection<Voucher>(voucherService.GetActiveVouchersWithIds(guest2.VoucherIds));

        }

        public string ReservationClick(object sender)
        {
            ReservationTour reservation = new ReservationTour();
            int guestAgeOnTour = guest2Service.GetAgeOnTour(guest2, tour);
            if (SelectedVoucher != null)
            {
                SelectedVoucher.Used = true;
                SelectedVoucher.ValidVoucher = false;
                voucherService.Update(SelectedVoucher);
                reservation = new ReservationTour(tour.Id, numberGuests, guest2.Id, -1, true, false, guestAgeOnTour);
            }
            else
            {
                reservation = new ReservationTour(tour.Id, numberGuests, guest2.Id, -1, false, false, guestAgeOnTour);
            }
            reservationTourService.Create(reservation);
            tour.AvailableSeats -= numberGuests;
            tourService.Update(tour);
            return "Rezervacija uspjesna! \nKorisnik " + guest2.Name + " " + guest2.Surname +
                " je izvrsio rezervaciju za " + numberGuests.ToString() + ". ljudi na turi: " + tour.Name + "." ;
            
        }

    }
}
