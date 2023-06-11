using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.View.OwnerView;
using ProjectSims.WPF.View.OwnerView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class AccommodationsDisplayViewModel : IObserver
    {
        public Owner Owner { get; set; }
        public ObservableCollection<Accommodation> AccommodationsForDisplay { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        public RelayCommand RenovateCommand { get; set; }
        public RelayCommand RegistrateCommand { get; set; }
        public RelayCommand StatisticsCommand { get; set; }
        private AccommodationService accommodationService;
        private RequestService requestService;
        private AccommodationReservationService accommodationReservationService;
        private RenovationScheduleService renovationScheduleService;
        public AccommodationsDisplayView View { get; set; }
        public NavigationService NavService { get; set; }

        public AccommodationsDisplayViewModel(Owner o, NavigationService navService, AccommodationsDisplayView view, TextBlock titleTextBlock) {
            Owner = o;
            NavService = navService;
            accommodationService = new AccommodationService();
            accommodationService.Subscribe(this);
            accommodationReservationService = new AccommodationReservationService();
            AccommodationsForDisplay = new ObservableCollection<Accommodation>(accommodationService.GetAccommodationsByOwner(Owner.Id));
            requestService = new RequestService();
            renovationScheduleService = new RenovationScheduleService();
            RegistrateCommand = new RelayCommand(Execute_RegistrateCommand, CanExecute_RegistrateCommand);
            RenovateCommand = new RelayCommand(Execute_RenovateCommand, CanExecute_RenovateCommand);
            StatisticsCommand = new RelayCommand(Execute_StatisticsCommand, CanExecute_StatisticsCommand);
            View = view;
            SelectedAccommodation = (Accommodation)View.AccommodationsTable.SelectedItem;
            TitleTextBlock = titleTextBlock;

        }

        private void Execute_RegistrateCommand(object obj)
        {
            NavService.Navigate(new AccommodationRegistrationView(Owner, TitleTextBlock, SelectedAccommodation, NavService));
            TitleTextBlock.Text = "Registracija smještaja";
        }

        private bool CanExecute_RegistrateCommand(object obj)
        {
            return SelectedAccommodation == null;
        }

        private void Execute_RenovateCommand(object obj)
        {
            if (SelectedAccommodation != null)
            {
                NavService.Navigate(new RenovationScheduleView(Owner, TitleTextBlock, SelectedAccommodation, NavService));
                TitleTextBlock.Text = "Renovacija smještaja";
            }
        }

        private bool CanExecute_RenovateCommand(object obj)
        {
            return SelectedAccommodation != null;
        }

        private void Execute_StatisticsCommand(object obj)
        {
            if (SelectedAccommodation != null)
            {
                NavService.Navigate(new StatisticsView(Owner, TitleTextBlock, SelectedAccommodation, NavService));
                TitleTextBlock.Text = "Statistika smještaja";
            }
        }

        private bool CanExecute_StatisticsCommand(object obj)
        {
            return SelectedAccommodation != null;
        }

        public bool HasWaitingRequests(Owner owner)
        {
            return requestService.HasWaitingRequests(owner.Id);
        }

        public void UpdateAccommodationsIfRenovated()
        {
            renovationScheduleService.UpdateIfRenovated(accommodationService.GetAccommodationsByOwner(Owner.Id));
        }

        public void Update()
        {
            AccommodationsForDisplay.Clear();
            foreach (Accommodation accommodation in accommodationService.GetAccommodationsByOwner(Owner.Id))
            {
                AccommodationsForDisplay.Add(accommodation);
            }
        }
    }
}
