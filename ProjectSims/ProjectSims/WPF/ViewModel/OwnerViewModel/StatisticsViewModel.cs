using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.Validation;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.View.Guest1View.Requests;
using ProjectSims.WPF.View.OwnerView.Pages;
using Syncfusion.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ProjectSims.Commands;
using System.Windows.Navigation;


namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public MyICommand PopularLocationCommand { get; set; }
        public MyICommand UnpopularLocationCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private AccommodationReservationService accommodationReservationService;
        private AccommodationService accommodationService;
        private AccommodationRatingService accommodationRatingService;
        private RequestService requestService;
        private OwnerService ownerService;
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

        private int _mostVisitedMonth;
        public int MostVisitedMonth
        {
            get => _mostVisitedMonth;

            set
            {
                if (value != _mostVisitedMonth)
                {
                    _mostVisitedMonth = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _mostVisitedYear;
        public string MostVisitedYear
        {
            get => _mostVisitedYear;

            set
            {
                if (value != _mostVisitedYear)
                {
                    _mostVisitedYear = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _mostBusyAccommodation;
        public int MostBusyAccommodation
        {
            get => _mostBusyAccommodation;

            set
            {
                if (value != _mostBusyAccommodation)
                {
                    _mostBusyAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _leastBusyAccommodation;
        public int LeastBusyAccommodation
        {
            get => _leastBusyAccommodation;

            set
            {
                if (value != _leastBusyAccommodation)
                {
                    _leastBusyAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }

        public ChartValues<int> TotalMonthReservations { get; set; }
        public ChartValues<int> TotalReservations { get; set; }
        public StatisticsView StatisticsView { get; set; }
        public NavigationService NavService { get; set; }

        public StatisticsViewModel(Owner o, StatisticsView view, TextBlock titleTextBlock, Accommodation selectedAccommodetion, int mostVisitedMonth, string mostVisitedYear, NavigationService navService)
        {
            Owner = o;
            StatisticsView = view;
            TitleTextBlock = titleTextBlock;
            SelectedAccommodation = selectedAccommodetion;
            NavService = navService;
            accommodationReservationService = new AccommodationReservationService();
            accommodationRatingService = new AccommodationRatingService();
            requestService = new RequestService();
            ownerService = new OwnerService();
            accommodationService = new AccommodationService();

            PopularLocationCommand = new MyICommand(OnOpen);
            UnpopularLocationCommand = new MyICommand(OnClose);
            
            Pointlabel = chartPoint => String.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
            Reservations = new List<AccommodationReservation>(accommodationReservationService.GetAllReservationsByAccommodationId(SelectedAccommodation.Id));
            MostVisitedMonth = mostVisitedMonth;
            MostVisitedYear = this.MostVisitedYear;
            TotalMonthReservations = new ChartValues<int>();
            TotalReservations = new ChartValues<int>();

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

            MostVisitedYear = FindMostVisitedYear();
            DisplayLink();
        }

        public void OnOpen()
        {
            Open();
        }

        public void Open()
        {
            NavService.Navigate(new AccommodationRegistrationView(Owner, TitleTextBlock, SelectedAccommodation, NavService));
        }

        public void OnClose()
        {
            Close(SelectedAccommodation);
        }

        public void Close(Accommodation selectedAccommodation)
        {
            accommodationService.Delete(selectedAccommodation);
            ownerService.RemoveAccommodation(Owner, selectedAccommodation.Id);
            NavService.Navigate(new AccommodationsDisplayView(Owner, TitleTextBlock, NavService));
        }

        public void DisplayTheNumberOfReservationsByCriteria()
        {
            var totalReservations = new ChartValues<int>();
            var totalCanceledReservations = new ChartValues<int>();
            var totalShiftedReservations = new ChartValues<int>();
            var totalRenovationReccommendations = new ChartValues<int>();
            foreach (string year in YearLabels)
            {
                totalReservations.Add(accommodationReservationService.CountAllReservationsByYear(Reservations, year));
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
            TotalMonthReservations = totalReservations;
        }

        public int DisplayMostVisitedMonth()
        {
            double[] daysInMonths = new double[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            MostVisitedMonth = 0;
            double unavailability = TotalMonthReservations[0] / daysInMonths[0];
            for (int i = 1; i < TotalMonthReservations.Count(); i++)
            {
                if (TotalMonthReservations[i] / daysInMonths[i] > unavailability)
                {
                    MostVisitedMonth = i;
                    unavailability = TotalMonthReservations[i] / daysInMonths[i];
                }
            }
            MostVisitedMonth += 1;
            return MostVisitedMonth;
        }

        public string FindMostVisitedYear()
        {
            Dictionary<string, double[]> busynessThroughYears = CountBusynessAndReservationCountForEachYear(Reservations);
            KeyValuePair<string, double[]> mostVisited = busynessThroughYears.FirstOrDefault();
            MostVisitedYear = mostVisited.Key;
            foreach (var item in busynessThroughYears)
            {
                if (item.Value[0] > mostVisited.Value[0]) 
                {
                    mostVisited = item;
                    MostVisitedYear = mostVisited.Key;
                }
            }
            return MostVisitedYear;
        }

        public void DisplayLink() 
        {
            if (SelectedAccommodation.Id == FindMostBusyAccommodation() && SelectedAccommodation.Id == FindMaxCountAccommodation())
            {
                StatisticsView.PopularLocationTextBlock.Visibility = System.Windows.Visibility.Visible;
            }
            else if(SelectedAccommodation.Id == FindLeastBusyAccommodation() && SelectedAccommodation.Id == FindMinCountAccommodation())
            {
                StatisticsView.UnpopularLocationTextBlock.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public int FindMaxCountAccommodation()
        {
            Dictionary<int, double[]> allBusyness = FindStatisticsForAllAccommodations();
            KeyValuePair<int, double[]> max = allBusyness.FirstOrDefault();
            foreach (var item in allBusyness)
            {
                if (item.Value[1] > max.Value[1])
                {
                    max = item;
                }
            }
            return max.Key;
        }

        public int FindMinCountAccommodation()
        {
            Dictionary<int, double[]> allBusyness = FindStatisticsForAllAccommodations();
            KeyValuePair<int, double[]> min = allBusyness.FirstOrDefault();
            foreach (var item in allBusyness)
            {
                if (item.Value[1] < min.Value[1])
                {
                    min = item;
                }
            }
            return min.Key;
        }

        public int FindMostBusyAccommodation()
        {
            Dictionary<int, double[]> allBusyness = FindStatisticsForAllAccommodations();
            KeyValuePair<int, double[]> most = allBusyness.FirstOrDefault();
            foreach (var item in allBusyness)
            {
                if (item.Value[0] > most.Value[0])
                {
                    most = item;
                }
            }
            return most.Key;
        }

        public int FindLeastBusyAccommodation()
        {
            Dictionary<int, double[]> allBusyness = FindStatisticsForAllAccommodations();
            KeyValuePair<int,double[]> least = allBusyness.First();
            foreach (var item in allBusyness)
            {
                if (item.Value[0] < least.Value[0])
                {
                    least = item;
                }
            }
            return least.Key;
        }

        public Dictionary<int, double[]> FindStatisticsForAllAccommodations() 
        {
            Dictionary<int, double[]> accommodationsStatistics = new Dictionary<int, double[]>();
            List<Accommodation> accommodations = accommodationService.GetAccommodationsByOwner(Owner.Id);
            foreach (Accommodation accommodation in accommodations)
            {
                List<AccommodationReservation> reservations = accommodationReservationService.GetAllReservationsByAccommodationId(accommodation.Id);
                accommodationsStatistics.Add(accommodation.Id, SumBusynessAndReservationCountForEachYear(reservations));
            }
            return accommodationsStatistics;
        }

        public double[] SumBusynessAndReservationCountForEachYear(List<AccommodationReservation> reservations) 
        {
            double[] sumAndCount = new double[] { 0, 0 };
            Dictionary<string, double[]> busyness = CountBusynessAndReservationCountForEachYear(reservations);
            foreach (var item in busyness) 
            {
                sumAndCount[0] += item.Value[0];
                sumAndCount[1] += item.Value[1];
            }
            return sumAndCount;
        }

        public Dictionary<string, double[]> CountBusynessAndReservationCountForEachYear(List<AccommodationReservation> reservations)
        {
            Dictionary<string, double[]> bussynessAndReservationCountInYears = new Dictionary<string, double[]>();
            foreach (var year in YearLabels)
            {
                bussynessAndReservationCountInYears.Add(year, new double[] { CountBusynessAndReservationCountInOneYear(reservations, year)[0], 
                                                                             CountBusynessAndReservationCountInOneYear(reservations, year)[1] } );
            }
            return bussynessAndReservationCountInYears;
        }

        public double[] CountBusynessAndReservationCountInOneYear(List<AccommodationReservation> reservations, string year) 
        {
            double[] busyness = new double[2] { 0, 0 };
            foreach (var item in accommodationReservationService.GettAllReservationsByYear(reservations, year))         {
                busyness[0] += (item.CheckOutDate.DayNumber - item.CheckInDate.DayNumber);
                busyness[1]++;
            }
            busyness[0] = busyness[0] / 365.0;
            return busyness;
        }
    }
}
