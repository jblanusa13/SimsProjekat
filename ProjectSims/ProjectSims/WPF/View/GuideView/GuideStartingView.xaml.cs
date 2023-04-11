using ProjectSims.Domain.Model;
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

namespace ProjectSims.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuideStartingView.xaml
    /// </summary>
    public partial class GuideStartingView : Window
    {
        public Guide guide { get; set; }
        public GuideStartingView(Guide g)
        {
            InitializeComponent();
            guide = g;
        }
        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            CreateTourView createTourView = new CreateTourView(guide);
            createTourView.Show();
            Close();
        }
        private void TrackTour_Click(object sender, RoutedEventArgs e)
        {
            AvailableToursView availableToursView = new AvailableToursView(guide);
            availableToursView.Show();
            Close();
        }

        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            ScheduledToursView scheduledToursView = new ScheduledToursView(guide);
            scheduledToursView.Show();
            Close();
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
