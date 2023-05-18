using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for RequestsGraphView.xaml
    /// </summary>
    public partial class RequestsGraphView : Page
    {
        private TourRequestService tourRequestService;
        public RequestsGraphView(string language,string location,string year)
        {
            InitializeComponent();
            tourRequestService = new TourRequestService();

            DataContext = this;
            if(year == null)
            {
                Column.Header = "Godina";
                StatisticsListView.ItemsSource =  tourRequestService.GetNumberOfRequestsByYear(Convert.ToInt32(year));

            }
            else
            {
                Column.Header = "Mesec";
                StatisticsListView.ItemsSource = tourRequestService.GetNumberOfRequestsByYearAndMonth(Convert.ToInt32(year));
            }
        }
    }
}
