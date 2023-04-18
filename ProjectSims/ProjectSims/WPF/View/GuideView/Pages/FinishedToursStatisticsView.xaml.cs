using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest2View.Pages;
using ProjectSims.WPF.ViewModel.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    public partial class FinishedToursStatisticsView : Page
    {
        private Tour SelectedTour { get; set; }
        public FinishedToursStatisticsView(Guide guide)
        {
            InitializeComponent();
            this.DataContext = new FinishedToursStatisticsViewModel(guide);
        }
        private void TourInfo_Click(object sender, RoutedEventArgs e)
        {
            SelectedTour = ((FrameworkElement)sender).DataContext as Tour;
            if (SelectedTour != null)
            {
                this.NavigationService.Navigate(new TourDetailsAndStatisticsView(SelectedTour));
            }
        }
    }
}
