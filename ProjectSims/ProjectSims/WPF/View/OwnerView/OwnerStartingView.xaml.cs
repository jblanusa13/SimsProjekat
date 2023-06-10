using ProjectSims.Domain.Model;
using ProjectSims.View.OwnerView;
using ProjectSims.WPF.View.Guest2View.Pages;
using ProjectSims.WPF.View.OwnerView.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Navigation;

namespace ProjectSims.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerStartingView.xaml
    /// </summary>
    public partial class OwnerStartingView : Window, INotifyPropertyChanged
    {
        public Owner Owner { get; set; }
        private GuestRatingService guestRatingService { get; set; }

        private string _title;
        public string PageTitle
        {
            get => _title;
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }
        public OwnerStartingView(Owner o)
        {
            InitializeComponent();
            this.DataContext = this;
            Owner = o;
            PageTitle = "Početna stranica";
            SelectedTab.Content = new HomePageView(Owner, TitleTextBlock, SelectedTab.NavigationService);
            guestRatingService = new GuestRatingService();
       }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            guestRatingService.NotifyOwnerAboutRating();
        }

        private void ButtonMenu(object sender, RoutedEventArgs e)
        {
            PageTitle = "";
            ChangeTab(0);
        }

        private void ButtonRequests(object sender, RoutedEventArgs e)
        {
            PageTitle = "Zahtjevi";
            ChangeTab(1);
        }

        private void ButtonNotifications(object sender, RoutedEventArgs e)
        {
            PageTitle = "Obavještenja";
            ChangeTab(2);
        }

        public void ChangeTab(int tabNum)
        {
            switch (tabNum)
            {
                case 0:
                    {
                        SelectedTab.Content = new SideMenuView(Owner, TitleTextBlock, SelectedTab.NavigationService);
                        break;
                    }
                case 1:
                    {
                        SelectedTab.Content = new RequestsView(Owner);
                        break;
                    }
                case 2:
                    {
                        break;
                    }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
