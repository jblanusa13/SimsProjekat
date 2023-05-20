using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Collections.ObjectModel;
using ProjectSims.WPF.ViewModel.Guest2ViewModel;

namespace ProjectSims.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for RequestStatisticsView.xaml
    /// </summary>
    public partial class RequestStatisticsView : Window
    {
        public RequestStatisticsView(RequestStatisticsViewModel requestStatisticsViewModel)
        {
            InitializeComponent();
            this.DataContext = requestStatisticsViewModel; 
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
