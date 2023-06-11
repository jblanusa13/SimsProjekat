using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        private GuestRatingService ratingService;
        private AccommodationReservationService reservationService;
        private ForumService forumService;
        public TextBlock TitleTextBlock { get; set; }

        public NotificationsViewModel(Owner owner, NavigationService navService)
        {
            Owner = owner;
            NavService = navService;
            ratingService = new GuestRatingService();
            reservationService = new AccommodationReservationService();
            forumService = new ForumService();
            Reservations = new ObservableCollection<AccommodationReservation>(ratingService.NotifyOwnerAboutRating(Owner.Id));
            //NewForums = new ObservableCollection<Forum>(forumService.GetAllNewForums(selectedForum.Id));
            RateCommand = new RelayCommand(Execute_RateCommand, CanExecute_RateCommand);
            CommentCommand = new RelayCommand(Execute_CommentCommand, CanExecute_CommentCommand);
        }

        private void Execute_RateCommand(object obj)
        {
            NavService.Navigate(new GuestRatingView(SelectedGuestInReservation, Owner, TitleTextBlock, NavService));
        }
        private bool CanExecute_RateCommand(object obj)
        {
            return SelectedGuestInReservation != null;
        }
        private void Execute_CommentCommand(object obj)
        {
        }
        private bool CanExecute_CommentCommand(object obj)
        {
            return false;
        }
        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in ratingService.NotifyOwnerAboutRating(Owner.Id))
            {
                Reservations.Add(reservation);
            }
            NewForums.Clear();
            /*foreach (Forum forum in forumService.NotifyOwnerAboutNewForum())
            {
                Guests.Add(forum);
            }*/
        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
