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
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    class NotificationsViewModel : INotifyPropertyChanged, IObserver
    {
        public Owner Owner { get; set; }
        public Forum SelectedForum { get; set; }
        public AccommodationReservation SelectedGuestInReservation { get; set; }
        public ObservableCollection<Forum> NewForums { get; set; }
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand RateCommand { get; set; }
        public RelayCommand CommentCommand { get; set; }
        public RelayCommand OnForumsCommand { get; set; }
        public RelayCommand OnReservationsCommand { get; set; }
        private GuestRatingService ratingService;
        private AccommodationReservationService reservationService;
        private NotificationOwnerGuestService notificationService;
        private ForumService forumService;
        private NotificationsView View { get; set; }
        private OwnerStartingView Window { get; set; }
        public TextBlock TitleTextBlock { get; set; }

        public NotificationsViewModel(Owner owner, OwnerStartingView window, NavigationService navService, NotificationsView view)
        {
            Owner = owner;
            NavService = navService;
            Window = window;
            View = view;
            ratingService = new GuestRatingService();
            ratingService.Subscribe(this);
            reservationService = new AccommodationReservationService();
            reservationService.Subscribe(this);
            notificationService = new NotificationOwnerGuestService();
            notificationService.Subscribe(this);
            forumService = new ForumService();
            Reservations = new ObservableCollection<AccommodationReservation>(ratingService.NotifyOwnerAboutRating(Owner.Id));
            NewForums = new ObservableCollection<Forum>(notificationService.GetAllForums(Owner));
            RateCommand = new RelayCommand(Execute_RateCommand, CanExecute_RateCommand);
            CommentCommand = new RelayCommand(Execute_CommentCommand, CanExecute_CommentCommand);
            OnForumsCommand = new RelayCommand(Execute_ForumCommand, CanExecute_ForumCommand);
            OnReservationsCommand = new RelayCommand(Execute_ReservationCommand, CanExecute_ReservationCommand);
        }

        private void Execute_ReservationCommand(object obj)
        {
            SelectedForum = null;
        }

        private bool CanExecute_ReservationCommand(object obj)
        {
            return SelectedForum != null;
        }

        private void Execute_ForumCommand(object obj)
        {
            SelectedGuestInReservation = null;
        }

        private bool CanExecute_ForumCommand(object obj)
        {
            return SelectedGuestInReservation != null;
        }

        private void Execute_RateCommand(object obj)
        {
            View.CommentButton.IsEnabled = false;
            NavService.Navigate(new GuestRatingView(SelectedGuestInReservation, Owner, Window, NavService));
            Window.PageTitle = "Ocjenjivanje";
        }
        private bool CanExecute_RateCommand(object obj)
        {
            return SelectedGuestInReservation != null;
        }
        private void Execute_CommentCommand(object obj)
        {
            View.RateButton.IsEnabled = false;
            NavService.Navigate(new OpenedForumView(Owner, NavService, SelectedForum));
        }
        private bool CanExecute_CommentCommand(object obj)
        {
            return SelectedForum != null;
        }
        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in ratingService.NotifyOwnerAboutRating(Owner.Id))
            {
                Reservations.Add(reservation);
            }
            NewForums.Clear();
            foreach (Forum forum in notificationService.GetAllForums(Owner))
            {
                NewForums.Add(forum);
            }
        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
