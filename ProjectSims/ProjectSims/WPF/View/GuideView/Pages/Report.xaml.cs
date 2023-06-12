using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        public ObservableCollection<Tour> Tours { get; set; }
        private TourService tourService;
        public Report(Guide guide,DateTime start,DateTime end)
        {
            InitializeComponent();
            tourService = new TourService();
            Tours = new ObservableCollection<Tour>(tourService.GetScheduledToursInDateRange(guide.Id,start,end));
            ScheduledTours.ItemsSource = Tours;
        }
    }
}
