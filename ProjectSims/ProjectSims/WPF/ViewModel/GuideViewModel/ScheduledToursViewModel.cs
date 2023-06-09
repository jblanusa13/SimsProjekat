﻿using ProjectSims.Domain.Model;
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

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class ScheduledToursViewModel : Page, IObserver
    {
        private TourService tourService;
        private ReservationTourService reservationTourService;
        private Guest2Service guest2Service;
        public NavigationService navigationService;
        public RelayCommand ViewDetailsCommand { get; set; }
        public ObservableCollection<Tour> ScheduledTours { get; set; }
        public Tour SelectedTour { get; set; }
        public Guide Guide { get; set; }
      
        public ScheduledToursViewModel(Guide guide,NavigationService ns)
        {
            navigationService = ns;
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            guest2Service = new Guest2Service();
            tourService.Subscribe(this);
            Guide = guide;
            ScheduledTours = new ObservableCollection<Tour>(tourService.GetToursByStateAndGuideId(TourState.Inactive, Guide.Id));
            ViewDetailsCommand = new RelayCommand(Executed_ViewDetailsCommand);
        }
        public void Executed_ViewDetailsCommand(object obj)
        {
            SelectedTour = ((FrameworkElement)obj).DataContext as Tour;
            NavigationService.Navigate(new TourDetailsAndCancelling(SelectedTour));
            MessageBox.Show("rr");
        }
        public void CancelTour(Tour SelectedTour)
        {
            tourService.UpdateTourState(SelectedTour, TourState.Cancelled);
            List<int> guestIds = reservationTourService.GetGuestIdsByTourAndState(SelectedTour, Guest2State.InactiveTour);
            if (guestIds.Count > 0)
                guestIds.ForEach(id => guest2Service.GiveVoucher(id,1));
        }
        public void Update()
        {
            ScheduledTours.Clear();
            foreach (var tour in tourService.GetToursByStateAndGuideId(TourState.Inactive, Guide.Id))
            {
                ScheduledTours.Add(tour);
            }
        }
    }
}