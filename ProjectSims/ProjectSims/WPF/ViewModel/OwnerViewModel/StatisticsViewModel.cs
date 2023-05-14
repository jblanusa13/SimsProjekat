using LiveCharts;
using LiveCharts.Wpf;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.Requests;
using ProjectSims.WPF.View.OwnerView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class StatisticsViewModel
    {
        private AccommodationReservationService accommodationReservationService;
        List<AccommodationReservation> Reservations { get; set; }
        public Owner Owner { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public Func<int, string> Values { get; set; }
        public Func<ChartPoint, string> Pointlabel { get; set; }
        public string[] YearLabels { get; set; }
        public string[] LabelsLocation { get; set; }
        public ObservableCollection<string> LabelsForYears { get; set; }
        public ObservableCollection<string> LabelsForLocations { get; set; }
        public SeriesCollection NumberOfReservationsByCriteria { get; set; }
        public SeriesCollection NumberRequestForLocationSeriesCollection { get; set; }

        public StatisticsViewModel(Owner o, TextBlock titleTextBlock, Accommodation selectedAccommodetion)
        {
            Owner = o;
            TitleTextBlock = titleTextBlock;
            SelectedAccommodation = selectedAccommodetion;
            accommodationReservationService = new AccommodationReservationService();
            Pointlabel = chartPoint => String.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
            Reservations = new List<AccommodationReservation>(accommodationReservationService.GetActiveAndCanceledByOwnerId(Owner.Id, SelectedAccommodation.Id));

            NumberOfReservationsByCriteria = new SeriesCollection();
            NumberRequestForLocationSeriesCollection = new SeriesCollection();
            
            YearLabels = new[] { "2019", "2020", "2021", "2022", "2023"};
            LabelsForYears = new ObservableCollection<string>();
            foreach (var l in YearLabels)
            {
                LabelsForYears.Add(l);
            }

            DisplayTheNumberOfReservationsByCriteria();
        }

        public void DisplayTheNumberOfReservationsByCriteria()
        {
            var totalReservations = new ChartValues<int>();
            var totalCanceledReservations = new ChartValues<int>();
            var totalShiftedReservations = new ChartValues<int>();
            foreach (string year in YearLabels)
            {
                totalReservations.Add(accommodationReservationService.GetAllReservationsByYear(Reservations, year));
                totalCanceledReservations.Add(accommodationReservationService.GetAllCanceledReservationsByYear(Reservations, year));
                totalShiftedReservations.Add(accommodationReservationService.GetAllShiftedReservationsByYear(Reservations, year));
            }
            NumberOfReservationsByCriteria.Add(new ColumnSeries { Values = totalReservations, Title = "Rezervacija" });
            NumberOfReservationsByCriteria.Add(new ColumnSeries { Values = totalCanceledReservations, Title = "Otkazane" });
            NumberOfReservationsByCriteria.Add(new ColumnSeries { Values = totalShiftedReservations, Title = "Pomjerene" });
                Values = value => value.ToString("D");
        }
    }
}
