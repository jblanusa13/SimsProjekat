using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
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
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class RenovationsViewModel : IObserver, INotifyPropertyChanged
    {
        public Owner Owner { get; set; }
        public OwnerStartingView Window { get; set; }
        public RenovationsView View { get; set; }
        private RenovationScheduleService renovationService;
        public RenovationSchedule SelectedRenovation { get; set; }
        public RelayCommand QuitRenovationCommand {get; set; }
        public RelayCommand CancelCommand {get; set; }
        public ObservableCollection<RenovationSchedule> RenovationList { get; set; }
        public ObservableCollection<int> DurationList { get; set; }
        public DateOnly FirstDate { get; set; }
        public DateOnly SecondDate { get; set; }
        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }
        public NavigationService NavService { get; set; }

        public RenovationsViewModel(OwnerStartingView window, Owner owner, NavigationService navService, RenovationsView view) 
        {
            Owner = owner;
            NavService = navService;
            Window = window;
            QuitRenovationCommand = new RelayCommand(Execute_QuitCommand, CanExecute_QuitCommand);
            CancelCommand = new RelayCommand(Execute_CancelCommand);
            renovationService = new RenovationScheduleService();
            renovationService.Subscribe(this);
            RenovationList = new ObservableCollection<RenovationSchedule>(renovationService.GetPassedAndFutureRenovationsByOwner(Owner.Id));
            renovationService.SetDurationForEachRenovation(Owner);
            View = view;
        }

        private bool CanExecute_QuitCommand(object obj)
        {
            return SelectedRenovation != null;
        }

        private void Execute_QuitCommand(object obj)
        {
             SelectedRenovation = (RenovationSchedule)View.RenovationsTable.SelectedItem;
             QuitRenovation(SelectedRenovation); 
        }

        private void Execute_CancelCommand(object obj)
        {
             NavService.Navigate(new HomePageView(Owner, NavService, Window));
            Window.PageTitle = "Početna stranica";
        }

        public void QuitRenovation(RenovationSchedule renovation)
        {
            if (renovationService.CanQuitRenovation(renovation))
            {
                renovationService.RemoveRenovation(renovation);
                Update();
            }
            else
            {
                //passed date
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Update()
        {
            RenovationList.Clear();
            foreach (RenovationSchedule renovation in renovationService.GetPassedAndFutureRenovationsByOwner(Owner.Id))
            {
                RenovationList.Add(renovation);
            }
        }
    }
}
