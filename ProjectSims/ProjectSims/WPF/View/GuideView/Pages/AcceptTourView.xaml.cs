using ProjectSims.Domain.Model;
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
    /// Interaction logic for AcceptTourView.xaml
    /// </summary>
    public partial class AcceptTourView : Page
    {
        private AcceptTourViewModel acceptTourViewModel;
        public AcceptTourView(TourRequest tourRequest,Guide guide)
        {
            InitializeComponent();
            acceptTourViewModel = new AcceptTourViewModel(tourRequest,guide);
            this.DataContext = acceptTourViewModel;
        }
       public void AcceptTour_Click(object sender, RoutedEventArgs e) { }
       public void Back_Click(object sender, RoutedEventArgs e) { }
       public void DurationTextBox_TextChanged(object sender, TextChangedEventArgs e)
       {
            acceptTourViewModel.ShowAvailableDays(DaysComboBox);
       }
        public void DaysComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
            DateOnly SelectedDate = (DateOnly)DaysComboBox.SelectedItem;
            acceptTourViewModel.ShowAvailableTimes(SelectedDate, AppointmentsComboBox, Convert.ToDouble(DurationTextBox.Text));
       }
    }
}
