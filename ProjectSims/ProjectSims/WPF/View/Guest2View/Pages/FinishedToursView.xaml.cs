using ProjectSims.Service;
using ProjectSims.Domain.Model;
using ProjectSims.View.Guest2View;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectSims.WPF.ViewModel.Guest2ViewModel;

namespace ProjectSims.WPF.View.Guest2View.Pages
{
    /// <summary>
    /// Interaction logic for FinishedToursView.xaml
    /// </summary>
    public partial class FinishedToursView : Page
    {
        private FinishedToursViewModel viewModel;
        public FinishedToursView(FinishedToursViewModel finishedToursViewModel)
        {
            InitializeComponent();
            this.DataContext = finishedToursViewModel;
            viewModel = finishedToursViewModel;
        }

        private void ButtonRatingTour(object sender, RoutedEventArgs e)
        {
            viewModel.ButtonRatingTour(sender);
        }
    }
}
