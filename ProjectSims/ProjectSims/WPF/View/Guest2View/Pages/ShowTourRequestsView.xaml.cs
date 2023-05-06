using ProjectSims.Domain.Model;
using ProjectSims.Observer;
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

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for ShowTourRequestsView.xaml
    /// </summary>
    public partial class ShowTourRequestsView : Page, IObserver
    {
        private TourRequestService tourRequestService;

        public ObservableCollection<TourRequest> ListRequests { get; set; }
        public Guest2 guest2 { get; set; }
        public ShowTourRequestsView(Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            guest2 = g;
            tourRequestService = new TourRequestService();
            tourRequestService.Subscribe(this);
            ListRequests = new ObservableCollection<TourRequest>(tourRequestService.GetByGuest2Id(guest2.Id));

        }

        private void ButtonCreateRequest(object sender, RoutedEventArgs e)
        {
            var createRequest = new CreateTourRequestView(guest2);
            createRequest.Show();
        }

        private void UpdateListRequest()
        {
            ListRequests.Clear();
            foreach (var request in tourRequestService.GetByGuest2Id(guest2.Id))
            {
                ListRequests.Add(request);
            }
        }
        public void Update()
        {
            UpdateListRequest();
        }

        private void ImageAndLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var helpWindow = new HelpForTourRequest();
            helpWindow.Show();
        }

        private void Statistic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var statisticWindow = new RequestStatisticsView();
            statisticWindow.Show();
        }
    }
}
