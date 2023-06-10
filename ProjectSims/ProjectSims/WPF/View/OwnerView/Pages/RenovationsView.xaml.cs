using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using ProjectSims.Repository;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.ViewModel.GuideViewModel;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class RenovationsView : Page
    {
        public Owner Owner;
        public TextBlock TitleTextBlock { get; set; }
        public RenovationSchedule SelectedRenovation { get; set; }
        public NavigationService NavService { get; set; }
        public RenovationsViewModel renovationViewModel { get; set; }
        private RenovationScheduleService renovationService;

        public RenovationsView(Owner o, TextBlock titleTextBlock, NavigationService navService)
        {
            InitializeComponent();
            Owner = o;
            TitleTextBlock = titleTextBlock;
            NavService = navService;
            renovationService = new RenovationScheduleService();
            renovationViewModel = new RenovationsViewModel(SelectedRenovation, Owner, NavService);
            this.DataContext = renovationViewModel;
        }

        private void QuitRenovation_Click(object sender, RoutedEventArgs e)
        {
            SelectedRenovation = (RenovationSchedule)RenovationsTable.SelectedItem;
            renovationViewModel.QuitRenovation(SelectedRenovation);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavService.Navigate(new HomePageView(Owner, TitleTextBlock, NavService));
        }
    }
}
