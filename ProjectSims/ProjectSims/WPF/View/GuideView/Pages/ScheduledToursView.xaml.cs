using ProjectSims.Domain.Model;
using ProjectSims.Service;
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
    /// Interaction logic for ScheduledToursView.xaml
    /// </summary>
    public partial class ScheduledToursView : Page
    {
        public Tour SelectedTour { get; set; }
        public ScheduledToursViewModel scheduledToursViewModel{get; set;}
        public ScheduledToursView(Guide guide)
        {
            InitializeComponent();
            scheduledToursViewModel = new ScheduledToursViewModel(guide);
            this.DataContext = scheduledToursViewModel;
        }

        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            SelectedTour = ((FrameworkElement)sender).DataContext as Tour;
            if (!scheduledToursViewModel.IsLessThan48Hours(SelectedTour))
            {
                MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da otkazete turu?", "", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                    scheduledToursViewModel.CancelTour(SelectedTour);
            }
            else
                MessageBox.Show("Tura pocinje za manje od 48h i ne moze se otkazati!");
        }
    }
}

