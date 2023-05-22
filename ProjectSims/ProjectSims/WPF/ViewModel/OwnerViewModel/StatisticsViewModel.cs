using LiveCharts;
using LiveCharts.Defaults;
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
        private AccommodationRatingService accommodationRatingService;
        private RequestService requestService;
        public List<AccommodationReservation> Reservations { get; set; }
        public Owner Owner { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public Func<int, string> Values { get; set; }
        public Func<ChartPoint, string> Pointlabel { get; set; }
        public string[] YearLabels { get; set; }
        public string[] MonthLabels { get; set; }
        public ObservableCollection<string> LabelsForYears { get; set; }
        public ObservableCollection<string> LabelsForMonths { get; set; }
        public SeriesCollection NumberOfReservationsByCriteria { get; set; }
        public SeriesCollection NumberOfMonthReservationsByCriteria { get; set; }
        public StatisticsViewModel(Owner o, TextBlock titleTextBlock, Accommodation selectedAccommodetion, ComboBox yearComboBox)
        {
            Owner = o;
            TitleTextBlock = titleTextBlock;
            SelectedAccommodation = selectedAccommodetion;
            accommodationReservationService = new AccommodationReservationService();
            accommodationRatingService = new AccommodationRatingService();
            requestService = new RequestService();
            Pointlabel = chartPoint => String.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
            Reservations = new List<AccommodationReservation>(accommodationReservationService.GetAllReservationsByAccommodationId(SelectedAccommodation.Id));

            NumberOfReservationsByCriteria = new SeriesCollection();
            YearLabels = new[] { "2019", "2020", "2021", "2022", "2023" };
            LabelsForYears = new ObservableCollection<string>();
            foreach (var l in YearLabels)
            {
                LabelsForYears.Add(l);
            }
            DisplayTheNumberOfReservationsByCriteria();

            NumberOfMonthReservationsByCriteria = new SeriesCollection();
            MonthLabels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            LabelsForMonths = new ObservableCollection<string>();
            foreach (var l in MonthLabels)
            {
                LabelsForMonths.Add(l);
            }
        }
    
        public void DisplayTheNumberOfReservationsByCriteria()
        {
            var totalReservations = new ChartValues<int>();
            var totalCanceledReservations = new ChartValues<int>();
            var totalShiftedReservations = new ChartValues<int>();
            var totalRenovationReccommendations = new ChartValues<int>();
            foreach (string year in YearLabels)
            {
                totalReservations.Add(accommodationReservationService.GetAllReservationsByYear(Reservations, year));
                totalCanceledReservations.Add(accommodationReservationService.GetAllCanceledReservationsByYear(Reservations, year));
                totalShiftedReservations.Add(requestService.GetAllShiftedReservationsByYear(year, Owner.Id));
                totalRenovationReccommendations.Add(accommodationRatingService.GetAllRenovationReccommendationsByYear(year, Owner.Id));
            }
            NumberOfReservationsByCriteria.Add(new ColumnSeries { Values = totalReservations, Title = "Rezervacija" });
            NumberOfReservationsByCriteria.Add(new ColumnSeries { Values = totalCanceledReservations, Title = "Otkazane" });
            NumberOfReservationsByCriteria.Add(new ColumnSeries { Values = totalShiftedReservations, Title = "Pomjerene" });
            NumberOfReservationsByCriteria.Add(new ColumnSeries { Values = totalRenovationReccommendations, Title = "Prijedlozi za renovaciju" });
            Values = value => value.ToString("D");
        }

        public void DisplayTheNumberOfMonthReservationsByCriteria(string year)
        {
            NumberOfMonthReservationsByCriteria.Clear();
            var totalReservations = new ChartValues<int>();
            var totalCanceledReservations = new ChartValues<int>();
            var totalShiftedReservations = new ChartValues<int>();
            var totalRenovationReccommendations = new ChartValues<int>();
            foreach (string month in MonthLabels)
            {
                totalReservations.Add(accommodationReservationService.GetAllReservationsByMonth(Reservations, month, year));
                totalCanceledReservations.Add(accommodationReservationService.GetAllCanceledReservationsByMonth(Reservations, month, year));
                totalShiftedReservations.Add(requestService.GetAllShiftedReservationsByMonth(month, year, Owner.Id));
                totalRenovationReccommendations.Add(accommodationRatingService.GetAllRenovationReccommendationsByMonth(month, year, Owner.Id));
            }
            NumberOfMonthReservationsByCriteria.Add(new ColumnSeries { Values = totalReservations, Title = "Rezervacija" });
            NumberOfMonthReservationsByCriteria.Add(new ColumnSeries { Values = totalCanceledReservations, Title = "Otkazane" });
            NumberOfMonthReservationsByCriteria.Add(new ColumnSeries { Values = totalShiftedReservations, Title = "Pomjerene" });
            NumberOfMonthReservationsByCriteria.Add(new ColumnSeries { Values = totalRenovationReccommendations, Title = "Prijedlozi za renovaciju" });
            Values = value => value.ToString("D");
        }

        public void CloseAccommodation(Accommodation SelectedAccommodation)
        {
            accommodationReservationService.CloseAccommodation(SelectedAccommodation);
        }
    }
}
