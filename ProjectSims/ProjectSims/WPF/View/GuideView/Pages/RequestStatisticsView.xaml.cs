using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
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
using LiveCharts;
using LiveCharts.Wpf;
using ProjectSims.Domain.Model;
using ProjectSims.Service;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for RequestStatisticsView.xaml
    /// </summary>
    public partial class RequestStatisticsView : Page
    {
        private TourRequestService tourRequestService { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public RequestStatisticsView(Guide g)
        {
            InitializeComponent();
            DataContext = this;
            tourRequestService = new TourRequestService();
            YearsComboBox.ItemsSource = tourRequestService.GetYears();
            Column.Header = "Godina";

            foreach (var item in tourRequestService.GetStatisticsData(LocationTextBox.Text, LanguageTextBox.Text, -1))
            {
                StatisticsListView.Items.Add(item);
            }
            SeriesCollection = new SeriesCollection();
            foreach (var item in tourRequestService.GetStatisticsData(LocationTextBox.Text, LanguageTextBox.Text, -1))
            {
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = item.Key.ToString(),
                    Values = new ChartValues<double> { item.Value }
                });
            }
        }
        public void ShowStatistics_Click(object sender, RoutedEventArgs e)
        {
            YearsComboBox.ItemsSource = tourRequestService.GetYears();
            if (YearsComboBox.SelectedItem != null)
            {
                Column.Header = "Mesec";
                StatisticsListView.Items.Clear();
                foreach(var item in tourRequestService.GetStatisticsData(LocationTextBox.Text, LanguageTextBox.Text, Convert.ToInt32(YearsComboBox.SelectedItem)))
                {
                    StatisticsListView.Items.Add(item);
                }
                /*  foreach (var item in MonthStatisticsData)
                  {
                      SeriesCollection.Add(new ColumnSeries
                      {
                          Title = item.Key.ToString(),
                          Values = new ChartValues<double> { item.Value }
                      });
                  }*/
            }
            else
            {
                StatisticsListView.Items.Clear();
                foreach (var item in tourRequestService.GetStatisticsData(LocationTextBox.Text, LanguageTextBox.Text, -1))
                {
                    StatisticsListView.Items.Add(item);
                }
              /*  foreach (var item in YearStatisticsData)
                {
                    SeriesCollection.Add(new ColumnSeries
                    {
                        Title = item.Key.ToString(),
                        Values = new ChartValues<double> { item.Value }
                    });
                }*/
            }
        }
        public void Back_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
