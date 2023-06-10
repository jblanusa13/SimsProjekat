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
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.RatingPages;
using ProjectSims.WPF.View.Guest1View;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;
using ProjectSims.View.Guest1View;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for AccommodationReservationView.xaml
    /// </summary>
    public partial class GuestAccommodationsView : Page
    {
        public Guest1 Guest { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public NavigationService NavService { get; set; }

        public GuestAccommodationsView(Guest1 guest, NavigationService navService)
        {
            InitializeComponent();

            this.DataContext = new GuestAccommodationsViewModel(guest, navService);

            Guest = guest;
            NavService = navService;

            HelpButton.Focus();
        }

        private void Accommodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAccommodation = (Accommodation)AccommodationsTable.SelectedItem;
        }

        private void OpenReservationView(object sender, KeyEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                if ((e.Key.Equals(Key.Enter)) || (e.Key.Equals(Key.Return)))
                {
                    //NavService.Navigate(new AccommodationReservationView(SelectedAccommodation, Guest, NavService));
                }
            }
        }

    }

}

