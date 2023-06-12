using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest2View;
using ProjectSims.WPF.View.Guest2View.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class ShowTourRequestsViewModel : IObserver
    {
        private TourRequestService tourRequestService;
        private RequestForComplexTourService requestForComplexTourService;
        public ObservableCollection<TourRequest> ListRequests { get; set; }
        public TourRequest SelectedRequest { get; set; }
        public ObservableCollection<RequestForComplexTour> ListRequestForComplextTour { get; set; }
        public RequestForComplexTour SelectedComplexRequest { get; set; }
        public Guest2 guest2 { get; set; }
        public ShowTourRequestsViewModel(Guest2 g)
        {
            guest2 = g;
            tourRequestService = new TourRequestService();
            tourRequestService.UpdateTourRequestsFile();
            tourRequestService.Subscribe(this);
            requestForComplexTourService = new RequestForComplexTourService();
            requestForComplexTourService.UpdateRequestForComplexTour();
            requestForComplexTourService.Subscribe(this);
            ListRequests = new ObservableCollection<TourRequest>(tourRequestService.GetByGuest2Id(guest2.Id));
            ListRequestForComplextTour = new ObservableCollection<RequestForComplexTour>(requestForComplexTourService.GetByGuest2Id(guest2.Id));
        }
        private void UpdateListRequest()
        {
            ListRequests.Clear();
            foreach (var request in tourRequestService.GetByGuest2Id(guest2.Id))
            {
                ListRequests.Add(request);
            }
        }
        private void UpdateListRequestForComplextTour()
        {
            ListRequestForComplextTour.Clear();
            foreach (var request in requestForComplexTourService.GetByGuest2Id(guest2.Id))
            {
                ListRequestForComplextTour.Add(request);
            }
        }

        public void Update()
        {
            UpdateListRequest();
            UpdateListRequestForComplextTour();
        }
        public void CreateRequest()
        {
            var createRequest = new CreateTourRequestView(guest2);
            createRequest.Show();
        }
        public void CreateRequestForComplexTour()
        {
            var createRequestForComplexTour = new CreateRequestForComplexTourView(guest2);
            createRequestForComplexTour.Show();
        }
        public void ShowHelp()
        {
            var helpWindow = new HelpForTourRequest();
            helpWindow.Show();
        }
        public void ShowStatistics()
        {
            RequestStatisticsViewModel requestStatisticsViewModel = new RequestStatisticsViewModel(guest2);
            var statisticWindow = new RequestStatisticsView(requestStatisticsViewModel);
            statisticWindow.Show();
        }
    }
}
