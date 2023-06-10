using ProjectSims.Service;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.WPF.View.Guest2View;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for SearchTourView.xaml
    /// </summary>
    public partial class SearchTourView : Page, IObserver, INotifyPropertyChanged
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;

        public ObservableCollection<Tour> ListTour { get; set; }
        public Tour SelectedTour { get; set; }

        public Guest2 guest2 { get; set; }

        private int _numberGuest;
        public int NumberGuests 
        { 
            get => _numberGuest;
            set
            {
                if (value != _numberGuest)
                {
                    _numberGuest = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SearchTourView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;

            tourService = new TourService();
            tourService.Subscribe(this);

            reservationTourService = new ReservationTourService();
            ListTour = new ObservableCollection<Tour>(tourService.GetAllTours());
            guest2 = g;

        }
        private void UpdateTourList()
        {
            ListTour.Clear();
            foreach (var tour in tourService.GetAllTours())
            {
                ListTour.Add(tour);
            }
        }

        public void Update()
        {
            UpdateTourList();
        }

        private void See_More_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                var see_more = new DetailsAndReservationTourView(SelectedTour, guest2);
                see_more.Show();
            }
            else
            {
                MessageBox.Show("Morate selektovati turu da bi ste vidjeli vise detalja o njoj!");
            }

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            String location = LocationTextBox.Text;
            String language = LanguageTextBox.Text;

            double durationStart = tourService.ConvertToDouble(DurationStartTextBox.Text);
            if (durationStart == -2) return;

            double durationEnd = tourService.ConvertToDouble(DurationEndTextBox.Text);
            if (durationEnd == -2) return;

            //both duration fields must be entered
            if ((durationStart == -1 && durationEnd != -1) || (durationStart != -1 && durationEnd == -1))
            {
                MessageBox.Show("Oba polja za trajanje ture moraju biti popunjena da bi se izvrsila pretraga tura!");
                return;
            }
            else if (durationStart != -1 && durationEnd != -1)
            //if both fields are entered the first must be less than the second
            {
                if (durationStart > durationEnd)
                {
                    MessageBox.Show("Prvo polje kod trajanja ture mora imati manju vrijednost nego drugo!");
                    return;
                }
            }

            //int numberGuests = NumberGuests;//tourService.ConvertToInt(NumberGuestsTextBox.Text);

            if (location == "" && durationStart == -1 && language == "" && NumberGuests == 0)         //16. case (nothing entered)
            {
                MessageBox.Show("Morate unijeti neke informacije za pretragu!");
                ListTour.Clear();
                foreach (var tour in tourService.GetAllTours())
                {
                    ListTour.Add(tour);
                }
                return;
            }
            List<Tour> wantedTours = tourService.SearchTours(location, durationStart, durationEnd, language, NumberGuests);
            ListTour.Clear();
            foreach (Tour tour in wantedTours)
            {
                ListTour.Add(tour);
            }
        }
        private void Home_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new StartView(guest2));
        }
    }
}
