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

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for ShowTourRequestsView.xaml
    /// </summary>
    public partial class ShowTourRequestsView : Page
    {
        private TourRequestService tourRequestService;

        public ObservableCollection<TourRequest> ListRequests { get; set; }
        public ShowTourRequestsView(Guest2 guest2)
        {
            InitializeComponent();
            DataContext = this;
            tourRequestService = new TourRequestService();
            ListRequests = new ObservableCollection<TourRequest>(tourRequestService.GetByGuest2Id(guest2.Id));

        }

        private void ButtonCreateRequest(object sender, RoutedEventArgs e)
        {
            var createRequest = new CreateTourRequestView();
            createRequest.Show();
        }
    }
}
