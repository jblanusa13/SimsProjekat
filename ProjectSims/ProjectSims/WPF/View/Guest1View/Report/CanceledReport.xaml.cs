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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectSims.Domain.Model;
using ProjectSims.Service;

namespace ProjectSims.WPF.View.Guest1View.Report
{
    /// <summary>
    /// Interaction logic for CanceledReport.xaml
    /// </summary>
    public partial class CanceledReport : Page
    {
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        private AccommodationReservationService reservationService;
        public CanceledReport(Guest1 guest)
        {
            InitializeComponent();
            GuestName.Text = guest.Name + " " + guest.Surname;
            reservationService = new AccommodationReservationService();
            Reservations = new ObservableCollection<AccommodationReservation>(reservationService.GetCanceledReservationsByGuest(guest.Id));
            Lista.ItemsSource = Reservations;
        }
    }
}
