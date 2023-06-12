using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.View.GuideView;
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
using ProjectSims.Observer;
using ProjectSims.WPF.ViewModel.GuideViewModel;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourTrackingView.xaml
    /// </summary>
    public partial class TourTrackingView : Page
    {

        public TourTrackingView(Tour startedTour, Guide guide, NavigationService navigation)
        {
            InitializeComponent();
            this.DataContext = new TourTrackingViewModel(guide, startedTour, navigation);
        }

    }
    
}
