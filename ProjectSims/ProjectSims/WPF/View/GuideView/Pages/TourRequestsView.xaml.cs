using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
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
    /// <summary>
    /// Interaction logic for TourRequestsView.xaml
    /// </summary>
    public partial class TourRequestsView : Page
    {
        public TourRequestsViewModel tourRequestViewModel { get; set; }
        public Guide Guide { get; set; }
        public TourRequestsView(Guide guide)
        {
            InitializeComponent();
            tourRequestViewModel = new TourRequestsViewModel(guide);
            this.DataContext = tourRequestViewModel;
            Guide = guide;
        }
        private void Details_Click(object sender, RoutedEventArgs e)
        {
            TourRequest SelectedTourRequest = ((FrameworkElement)sender).DataContext as TourRequest;
            if (SelectedTourRequest != null)
            {
                this.NavigationService.Navigate(new AcceptTourView(SelectedTourRequest,Guide));
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
                this.NavigationService.Navigate(this.Parent);
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            tourRequestViewModel.SearchRequests(LocationTextBox.Text,LanguageTextBox.Text,MaxNumberGuestsTextBox.Text,DateRange.SelectedDates.Cast<DateTime>().ToList());
        }

    }
}
