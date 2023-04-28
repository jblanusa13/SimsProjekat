using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class TourRequestsViewModel : IObserver
    {
        private TourRequestService tourRequestService;
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public Guide Guide { get; set; }
        public TourRequestsViewModel(Guide guide)
        {
            tourRequestService = new TourRequestService();
            tourRequestService.Subscribe(this);
            Guide = guide;
            TourRequests = new ObservableCollection<TourRequest>(tourRequestService.GetAllRequests());
        }
        public void Update()
        {
            TourRequests.Clear();
            foreach (var tourRequest in tourRequestService.GetAllRequests())
            {
                TourRequests.Add(tourRequest);
            }
        }
    }
}
