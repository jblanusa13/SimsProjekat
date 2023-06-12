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
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Documents;
using ProjectSims.WPF.View.OwnerView;


namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public MyICommand PopularLocationCommand { get; set; }
        public MyICommand UnpopularLocationCommand { get; set; }
        public RelayCommand GenerateReportCommand { get; set; }
        public RelayCommand SelectionChangedCommand { get; set; }
        public RelayCommand MouseUpCommand { get; set; }
        public RelayCommand MouseDownCommand { get; set; }

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

        private Image _image;
        public Image Image
        {
            get => _image;

            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _year;
        public string Year
        {
            get => _year;

            set
            {
                if (value != _year)
                {
                    _year = value;
                    OnPropertyChanged();
                }
            }
        }
        public ChartValues<int> TotalMonthReservations { get; set; }
        public ChartValues<int> TotalReservations { get; set; }
        public StatisticsView View { get; set; }
        public NavigationService NavService { get; set; }
        public OwnerStartingView Window { get; set; }
        public StatisticsViewModel(Owner o, StatisticsView view, OwnerStartingView window, Accommodation selectedAccommodetion, NavigationService navService)
        {
            Owner = o;
            View = view;
            Window = window;
            SelectedAccommodation = selectedAccommodetion;
            NavService = navService;
            accommodationReservationService = new AccommodationReservationService();
            accommodationRatingService = new AccommodationRatingService();
            requestService = new RequestService();
            ownerService = new OwnerService();
            accommodationService = new AccommodationService();

            InitializeImages();

            PopularLocationCommand = new MyICommand(OnOpen);
            UnpopularLocationCommand = new MyICommand(OnClose);
            GenerateReportCommand = new RelayCommand(Execute_GenerateReportCommand);
            SelectionChangedCommand = new RelayCommand(Execute_SelectionChangedCommand);
            MouseUpCommand = new RelayCommand(Execute_MouseUpCommand);
            MouseDownCommand = new RelayCommand(Execute_MouseDownCommand);

            Pointlabel = chartPoint => String.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
            Reservations = new List<AccommodationReservation>(accommodationReservationService.GetAllReservationsByAccommodationId(SelectedAccommodation.Id));
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

            MostVisitedYear = accommodationReservationService.FindMostVisitedYear(YearLabels, Reservations);
            DisplayLink();
        }

        private void Execute_MouseDownCommand(object obj)
        {
            ShowChart();        
        }

        private void Execute_MouseUpCommand(object obj)
        {
            ShowChart();
        }

        private void Execute_SelectionChangedCommand(object obj)
        {
            ShowChart();
        }

        private void Execute_GenerateReportCommand(object obj)
        {
            PrintDialog printDialog = new PrintDialog();
            ReportToGenerateView rtg = new ReportToGenerateView(Owner);
            FlowDocument fd = rtg.Document;
            DocumentPaginator documentPaginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
            printDialog.PrintDocument(documentPaginator, "Izvještaj");
        }

        public void OnOpen()
        {
            Open();
        }

        public void Open()
        {
            NavService.Navigate(new AccommodationRegistrationView(Owner, Window, SelectedAccommodation, NavService));
        }

        public void OnClose()
        {
            Close(SelectedAccommodation);
        }

        public void Close(Accommodation selectedAccommodation)
        {
            accommodationService.Delete(selectedAccommodation);
            ownerService.RemoveAccommodation(Owner, selectedAccommodation.Id);
            NavService.Navigate(new AccommodationsDisplayView(Owner, NavService, Window));
            Window.PageTitle = "Smještaji";
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

        public void DisplayLink() 
        {
            if (SelectedAccommodation.Id == accommodationReservationService.FindMostBusyAccommodation(Owner, YearLabels) 
                && SelectedAccommodation.Id == accommodationReservationService.FindMaxCountAccommodation(Owner, YearLabels))
            {
                View.PopularLocationTextBlock.Visibility = Visibility.Visible;
            }
            else if(SelectedAccommodation.Id == accommodationReservationService.FindLeastBusyAccommodation(Owner, YearLabels) && SelectedAccommodation.Id == accommodationReservationService.FindMinCountAccommodation(Owner, YearLabels))
            {
                View.UnpopularLocationTextBlock.Visibility = Visibility.Visible;
            }
        }

        public void ShowChart()
        {
            if (!string.IsNullOrEmpty(Year))
            {
                DisplayTheNumberOfMonthReservationsByCriteria(Year);
                MostVisitedMonth = accommodationReservationService.DisplayMostVisitedMonth(TotalMonthReservations, MostVisitedMonth);
                MostVisitedYear = accommodationReservationService.FindMostVisitedYear(YearLabels, Reservations);
                View.MonthChart.Visibility = Visibility.Visible;
                View.MostVisitedMonthTextBox.Visibility = Visibility.Visible;
                View.MostVisitedYearTextBox.Visibility = Visibility.Visible;
                View.MostVisitedMonthLabel.Visibility = Visibility.Visible;
                View.MostVisitedYearLabel.Visibility = Visibility.Visible;
            }
        }

        public void InitializeImages()
        {
            foreach (string fileName in SelectedAccommodation.Images)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fileName, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                Image = new Image();
                Image.Source = bitmap;
                Image.Stretch = Stretch.Fill;
                View.ImageList.Items.Add(Image);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
