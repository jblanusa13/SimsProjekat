using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.Requests
{
    /// <summary>
    /// Interaction logic for DateChangeRequest.xaml
    /// </summary>
    public partial class DateChangeRequest : Window
    {
        public DateChangeRequestViewModel viewModel { get; set; }
        public DateChangeRequest(AccommodationReservation selectedReservation)
        {
            InitializeComponent();

            viewModel = new DateChangeRequestViewModel(selectedReservation);
            this.DataContext = viewModel;
        }

        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (DateChangePicker.SelectedDate != null)
            {
                DateOnly dateChange = DateOnly.FromDateTime((DateTime)DateChangePicker.SelectedDate);
                viewModel.SendRequest(dateChange);
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}