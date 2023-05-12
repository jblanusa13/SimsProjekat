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
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.RatingPages
{
    /// <summary>
    /// Interaction logic for FinishRatingView.xaml
    /// </summary>
    public partial class FinishRatingView : Page
    {
        private AccommodationRatingViewModel viewModel;
        public FinishRatingView(AccommodationRatingViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
        }

        private void FinishRating_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RateAcommodation();
            RatingStartView startView = (RatingStartView)Window.GetWindow(this);
            startView.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
