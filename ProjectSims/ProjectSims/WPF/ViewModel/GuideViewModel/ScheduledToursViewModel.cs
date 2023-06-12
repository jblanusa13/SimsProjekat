using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ProjectSims.WPF.View.GuideView.Pages;
using System.Reflection;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class ScheduledToursViewModel : Page, IObserver,INotifyPropertyChanged
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;
        private Guest2Service guest2Service;
        public NavigationService NavService;
        public RelayCommand ViewDetailsCommand { get; set; }
        public RelayCommand GenerateReportCommand { get; set; }
        public ObservableCollection<Tour> ScheduledTours { get; set; }
       
        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guide Guide { get; set; }
      
        public ScheduledToursViewModel(Guide guide,NavigationService navigationService)
        {
            NavService = navigationService;
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            guest2Service = new Guest2Service();
            tourService.Subscribe(this);
            Guide = guide;
            ScheduledTours = new ObservableCollection<Tour>(tourService.GetToursByStateAndGuideId(TourState.Inactive, Guide.Id));
            ViewDetailsCommand = new RelayCommand(Executed_ViewDetailsCommand);
            GenerateReportCommand = new RelayCommand(Executed_GenerateReportCommand);
        }
        public void Executed_ViewDetailsCommand(object obj)
        {
            NavService.Navigate(new TourDetailsAndCancelling(SelectedTour));
        }
        public void Executed_GenerateReportCommand(object obj)
        {
            NavService.Navigate(new DatesReport(NavService,Guide));
        }
        public void Update()
        {
            ScheduledTours.Clear();
            foreach (var tour in tourService.GetToursByStateAndGuideId(TourState.Inactive, Guide.Id))
            {
                ScheduledTours.Add(tour);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}