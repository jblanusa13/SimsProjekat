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
       public FinishedToursStatisticsViewModel finishedToursStatisticsViewModel;
        public FinishedToursStatisticsView(Guide guide,NavigationService NavService)
        {
            InitializeComponent();
            finishedToursStatisticsViewModel = new FinishedToursStatisticsViewModel(guide, NavService);
            this.DataContext = finishedToursStatisticsViewModel;
        }
        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            finishedToursStatisticsViewModel.ViewDetails(((FrameworkElement)sender).DataContext as Tour);
        }
    }
}
