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

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Guest1_Click(object sender, RoutedEventArgs e)
        {
            Guest1View guest1 = new Guest1View();
            guest1.Show();
        }
        private void Guest2_Click(object sender, RoutedEventArgs e)
        {
            TourDisplayAndSearchView window = new TourDisplayAndSearchView();
            window.Show();
        }

        private void Guide(object sender, RoutedEventArgs e)
        {
            CreateTour createTour = new CreateTour();
            createTour.Show();
        }
    }
}
