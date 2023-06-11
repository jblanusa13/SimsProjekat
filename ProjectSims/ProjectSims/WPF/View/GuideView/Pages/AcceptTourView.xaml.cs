using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.ViewModel.GuideViewModel;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ProjectSims.Commands;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for AcceptTourView.xaml
    /// </summary>
    public partial class AcceptTourView : Page
    {
        private TourRequestService tourRequestService;
        private AppointmentService appointmentService;
        private GuideService guideService;
        public TourRequest SelectedTourRequest { get; set; }
        public Guide Guide { get; set; }
        public List<double> availableDurations { get; set; }
        public RelayCommand AcceptTourCommand { get; set; }
        public DateTime SelectedDate { get; set; }
        public double Duration { get; set; }
        public AcceptTourView(TourRequest tourRequest,Guide guide)
        {
            InitializeComponent();
            appointmentService = new AppointmentService();
            tourRequestService = new TourRequestService();
            guideService = new GuideService();
            DataContext = this;
            Guide = guide;
            SelectedTourRequest=tourRequest;
            DateComboBox.ItemsSource = tourRequestService.GetDaysBetweenRange(tourRequest);
            AcceptTourCommand = new RelayCommand(Execute_AcceptTourCommand, CanExecute_AcceptTourCommand);
            TimeComboBox.ItemsSource = appointmentService.GetAll().Select(t => t.Start).ToList().Distinct().ToList();
            availableDurations = new List<double>();


        }
        private bool CanExecute_AcceptTourCommand(object obj)
        {
            return (DateComboBox.SelectedItem != null) && (TimeComboBox.SelectedItem != null) && (DurationComboBox.SelectedItem != null);
        }
        private void Execute_AcceptTourCommand(object obj)
        {
            DateOnly date = (DateOnly)DateComboBox.SelectedItem;
            TimeOnly time = (TimeOnly)TimeComboBox.SelectedItem;
            double duration = (double)DurationComboBox.SelectedItem;
            SelectedDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
            this.NavigationService.Navigate(new CreateTourView(Guide, SelectedTourRequest, null, null,SelectedDate,(int)duration));
        }
        public void TimeSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateComboBox.SelectedItem != null && TimeComboBox.SelectedItem != null)
            {
                DurationComboBox.ItemsSource = appointmentService.CalculateAvailableDurations(Guide, (DateOnly)DateComboBox.SelectedItem, (TimeOnly)TimeComboBox.SelectedItem, SelectedTourRequest);
            }
        }
       public void Back_Click(object sender, RoutedEventArgs e) 
        { 
            this.NavigationService.GoBack();
        }
    }
}
