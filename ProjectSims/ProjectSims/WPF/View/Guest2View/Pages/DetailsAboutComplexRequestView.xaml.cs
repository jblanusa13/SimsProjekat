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
    /// Interaction logic for DetailsAboutComplexRequestView.xaml
    /// </summary>
    public partial class DetailsAboutComplexRequestView : Page
    {
        public RequestForComplexTour request { get; set; }
        public ObservableCollection<TourRequest> ListRequests { get; set; }

        private RequestForComplexTourService requestForComplexTourService;

        public TourRequest SelectedRequest { get; set; }

        public DetailsAboutComplexRequestView(RequestForComplexTour complexRequest)
        {
            InitializeComponent();
            this.DataContext = this;
            requestForComplexTourService = new RequestForComplexTourService();
            request = complexRequest;
            ListRequests = new ObservableCollection<TourRequest>(request.TourRequests);
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Zahtjevi_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
