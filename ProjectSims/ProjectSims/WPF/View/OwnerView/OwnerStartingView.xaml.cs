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

namespace ProjectSims.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerStartingView.xaml
    /// </summary>
    public partial class OwnerStartingView : Window, INotifyPropertyChanged
    {
        public Owner owner { get; set; }
        private GuestRatingService guestRatingService { get; set; }

        private string _title;
        public string Title
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
            owner = o;
            TitleTextBlock.Text = "Početna stranica";
            SelectedTab.Content = new HomePage(owner);
            guestRatingService = new GuestRatingService();
       }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            guestRatingService.NotifyOwnerAboutRating();
        }

        private void ButtonMenu(object sender, RoutedEventArgs e)
        {
            TitleTextBlock.Text = "";
            ChangeTab(0);
        }

        private void ButtonRequests(object sender, RoutedEventArgs e)
        {
            TitleTextBlock.Text = "Zahtjevi";
            ChangeTab(1);
        }

        private void ButtonNotifications(object sender, RoutedEventArgs e)
        {
            TitleTextBlock.Text = "Obavještenja";
            ChangeTab(2);
        }

        public void ChangeTab(int tabNum)
        {
            switch (tabNum)
            {
                case 0:
                    {
                        SelectedTab.Content = new SideMenu(owner, TitleTextBlock);
                        break;
                    }
                case 1:
                    {
                        SelectedTab.Content = new Requests(owner);
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
