using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Service;
using ProjectSims.WPF.ViewModel.Guest2ViewModel;
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
        public Guest2TrackingTourView(Guest2TrackingTourViewModel trackingTourViewModel)
        {
            InitializeComponent();
            this.DataContext = trackingTourViewModel;
        }

        private void ButtonBackStartWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
