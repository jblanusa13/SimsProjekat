using ceTe.DynamicPDF;
using ceTe.DynamicPDF.IO;
using ceTe.DynamicPDF.LayoutEngine;
using ceTe.DynamicPDF.Merger;
using LiveCharts;
using ProjectSims.Domain.Model;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.View.Guest2View;
using ProjectSims.WPF.ViewModel.Guest2ViewModel;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : System.Windows.Controls.Page, INotifyPropertyChanged
    {
        public Owner Owner { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public Frame SelectedTab { get; set; }
        public StatisticsViewModel statisticsViewModel { get; set; }

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
        public NavigationService NavService { get; set; }
        public StatisticsView(Owner o, TextBlock titleTextBlock, Accommodation selectedAccommodetion, NavigationService navService)
        {
            InitializeComponent();
            Owner = o;
            TitleTextBlock = titleTextBlock;
            SelectedAccommodation = selectedAccommodetion;
            NavService = navService;
            statisticsViewModel = new StatisticsViewModel(Owner, this, TitleTextBlock, SelectedAccommodation, MostVisitedMonth, MostVisitedYear, NavService);
            InitializeImages();
            this.DataContext = statisticsViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitializeImages()
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
                ImageList.Items.Add(Image);
            }
        }

        private void RegisterNew_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new AccommodationRegistrationView(Owner, TitleTextBlock, SelectedAccommodation, NavService));
            TitleTextBlock.Text = "Registracija smještaja";
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            ReportToGenerateView rtg = new ReportToGenerateView(Owner);
            FlowDocument fd = rtg.Document;
            DocumentPaginator documentPaginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
            printDialog.PrintDocument(documentPaginator, "Izvještaj");

            TitleTextBlock.Text = "Početna stranica";
            NavService.Navigate(new HomePageView(Owner, TitleTextBlock, NavService));
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowChart();
        }

        private void YearComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ShowChart();
        }

        private void ShowChart()
        {
            if (!string.IsNullOrEmpty(YearComboBox.Text))
            {
                statisticsViewModel.DisplayTheNumberOfMonthReservationsByCriteria(YearComboBox.Text);
                MostVisitedMonth = statisticsViewModel.DisplayMostVisitedMonth();
                MostVisitedYear = statisticsViewModel.FindMostVisitedYear();
                MonthChart.Visibility = Visibility.Visible;
                MostVisitedMonthTextBox.Visibility = Visibility.Visible;
                MostVisitedYearTextBox.Visibility = Visibility.Visible;
                MostVisitedMonthLabel.Visibility = Visibility.Visible; 
                MostVisitedYearLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
