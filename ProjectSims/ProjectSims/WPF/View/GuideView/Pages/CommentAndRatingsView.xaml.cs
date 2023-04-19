using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.ViewModel.GuideViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for CommentAndRatingsView.xaml
    /// </summary>
    public partial class CommentAndRatingsView : Page
    {
        private CommentAndRatingsViewModel viewModel { get; set; }
        public Tour Tour { get; set; }
        public CommentAndRatingsView(TourAndGuideRating tourRating,Tour tour)
        {
            InitializeComponent();
            viewModel = new CommentAndRatingsViewModel(tourRating, tour);
            Tour = tour;
            this.DataContext = viewModel;
        }
        public void ReportComment_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ReportComment();
            this.NavigationService.Navigate(new TourDetailsAndRatingsView(Tour));
        }
        public void AcceptComment_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
