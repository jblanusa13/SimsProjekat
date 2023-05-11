using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest2View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class ShowTourRequestsViewModel : IObserver
    {
        private TourRequestService tourRequestService;
        public ObservableCollection<TourRequest> ListRequests { get; set; }
        public Guest2 guest2 { get; set; }
        public ShowTourRequestsViewModel(Guest2 g)
        {
            UpdateTourRequestsFile();
            guest2 = g;
            tourRequestService = new TourRequestService();
            tourRequestService.Subscribe(this);
            ListRequests = new ObservableCollection<TourRequest>(tourRequestService.GetByGuest2Id(guest2.Id));
        }

        private void UpdateTourRequestsFile()
        {
            TourRequestService requestService = new TourRequestService();
            DateOnly today = DateOnly.FromDateTime(DateTime.Today).AddDays(2);
            List<TourRequest> requests = new List<TourRequest>(requestService.GetAllRequests());
            foreach (TourRequest request in requests)
            {
                if (today >= request.DateRangeStart && request.State == TourRequestState.Waiting)
                {
                    request.State = TourRequestState.Invalid;
                    requestService.Update(request);
                }
            }
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
        public void ButtonCreateRequest(object sender)
        {
            var createRequest = new CreateTourRequestView(guest2);
            createRequest.Show();
        }
        public void ImageAndLabel_MouseLeftButtonDown(object sender)
        {
            var helpWindow = new HelpForTourRequest();
            helpWindow.Show();
        }
        public void Statistic_MouseLeftButtonDown(object sender)
        {
            RequestStatisticsViewModel requestStatisticsViewModel = new RequestStatisticsViewModel(guest2);
            var statisticWindow = new RequestStatisticsView(requestStatisticsViewModel);
            statisticWindow.Show();
        }
    }
}
