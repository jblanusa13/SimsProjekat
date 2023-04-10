using ProjectSims.Domain.Model;
using ProjectSims.Repository;
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
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for Guest2TrackingTourView.xaml
    /// </summary>
    public partial class Guest2TrackingTourView : Window
    {
        private KeyPointService keyPointService;
        public GuideRepository guideRepository;
        public ObservableCollection<KeyPoint> UnFinishedKeyPoints { get; set; }
        public ObservableCollection<KeyPoint> FinishedKeyPoints { get; set; }
        public string CurentlyActiveStation { get; set; }
        public Tour tour { get; set; }
        public Guest2TrackingTourView(Tour t)
        {
            InitializeComponent();
            DataContext = this;
            tour = t;
            keyPointService = new KeyPointService();
            guideRepository = new GuideRepository();
            FinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.FindFinishedKeyPointsByIds(tour.KeyPointIds));
            UnFinishedKeyPoints = new ObservableCollection<KeyPoint>(keyPointService.FindUnFinishedKeyPointsByIds(tour.KeyPointIds));
            CurentlyActiveStation = UnFinishedKeyPoints.First().Name;
            Guide guide = guideRepository.FindById(tour.GuideId);
            GuideTextBox.Text = guide.Name + " " + guide.Surname;
        }

        private void ButtonBackStartWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
