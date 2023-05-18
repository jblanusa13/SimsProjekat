using System;
using System.Collections.Generic;
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
        public RequestStatisticsView(Guide g)
        {
            InitializeComponent();
            tourRequestService = new TourRequestService();
            YearComboBox.ItemsSource = tourRequestService.GetAllRequests().Select(x => x.CreationDate.Year).Distinct();
        }
        public void ShowStatistics_Click(object sender, RoutedEventArgs e)
        {
            if(YearComboBox.SelectedItem == null)
            {
                StatisticsFrame.Content = new RequestsGraphView(LocationTextBox.Text, LanguageTextBox.Text, null);
            }
            else
            {
                StatisticsFrame.Content = new RequestsGraphView(LocationTextBox.Text, LanguageTextBox.Text, YearComboBox.SelectedItem.ToString());
            } 
        }
        public void Back_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
