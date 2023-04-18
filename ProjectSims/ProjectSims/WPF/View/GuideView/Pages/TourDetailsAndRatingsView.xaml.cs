using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ProjectSims.WPF.ViewModel.GuideViewModel;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourDetailsAndRatingsView.xaml
    /// </summary>
    public partial class TourDetailsAndRatingsView : Page
    {
        public TourAndGuideRating TourRating { get; set; }
        public Tour SelectedTour;
        public TourDetailsAndRatingsView(Tour selectedTour)
        {
            InitializeComponent();
            SelectedTour = selectedTour;
            this.DataContext = new TourDetailsAndRatingsViewModel(selectedTour);

        }
        public void ViewComment_Click(object sender, EventArgs e)
        {
            TourRating = ((FrameworkElement)sender).DataContext as TourAndGuideRating;
            if (TourRating != null)
            {
                this.NavigationService.Navigate(new CommentAndRatingsView(TourRating,SelectedTour));
            }
        }
    }
}
