using ProjectSims.Commands;
using ProjectSims.Domain.Model;
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
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public Owner Owner { get; set; }
        public NavigationService NavService { get; set; }
        public OwnerStartingView Window { get; set; }
        public RelayCommand AccommodationsCommand { get; set; }
        public RelayCommand RatingsCommand { get; set; }
        public RelayCommand ForumsCommand { get; set; }

        public HomePageViewModel(Owner owner, NavigationService navService, OwnerStartingView view)
        {
            Owner = owner;
            NavService = navService;
            AccommodationsCommand = new RelayCommand(Execute_AccommodationsCommand);
            RatingsCommand = new RelayCommand(Execute_RatingsCommand);
            ForumsCommand = new RelayCommand(Execute_ForumsCommand);
            Window = view;
        }

        private void Execute_ForumsCommand(object obj)
        {
            NavService.Navigate(new ForumsDisplayView(Owner, Window, NavService));
            Window.PageTitle = "Forumi";
        }

        private void Execute_RatingsCommand(object obj)
        {
            NavService.Navigate(new OwnerRatingsDisplayView(Owner, NavService));
            Window.PageTitle = "Recenzije";
        }

        private void Execute_AccommodationsCommand(object obj)
        {
            NavService.Navigate(new AccommodationsDisplayView(Owner, NavService, Window));
            Window.PageTitle = "Smještaji";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
