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

        public StatisticsView(Owner o, TextBlock titleTextBlock, Accommodation selectedAccommodetion)
        {
            InitializeComponent();
            Owner = o;
            TitleTextBlock = titleTextBlock;
            SelectedAccommodation = selectedAccommodetion;
            statisticsViewModel = new StatisticsViewModel(Owner, TitleTextBlock, SelectedAccommodation, YearComboBox, MostVisitedMonth);
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

        private void CloseAccommodation_Click(object sender, RoutedEventArgs e)
        {
            statisticsViewModel.CloseAccommodation(SelectedAccommodation);
        }

        private void RegisterNew_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AccommodationRegistrationView(Owner, TitleTextBlock, SelectedAccommodation));
            TitleTextBlock.Text = "Registracija smještaja";
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            ReportToGenerateView rtg = new ReportToGenerateView();
            /*rtg.ReportScroll.ScrollToTop();
            printDialog.PrintVisual(rtg.ReportScroll.Content as Visual, "IzvestajOZauzetostiProstorija");*/
            FlowDocument fd = rtg.Document;
            DocumentPaginator documentPaginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
            printDialog.PrintDocument(documentPaginator, "Izvestaj");

            TitleTextBlock.Text = "Početna stranica";
            this.NavigationService.Navigate(new HomePageView(Owner, TitleTextBlock));
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
                MonthChart.Visibility = Visibility.Visible;
                MostVisitedTextBox.Visibility = Visibility.Visible;
                MostVisitedLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
