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
using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Service;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.RatingPages
{
    /// <summary>
    /// Interaction logic for RenovationRecommendationView.xaml
    /// </summary>
    public partial class RenovationRecommendationView : Page
    {
        private AccommodationRatingViewModel viewModel;
        public RenovationRecommendationView(AccommodationRatingViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            SkipButton.Focus();
        }

        private void RateAcommodation_Click(object sender, RoutedEventArgs e)
        {
            int urgency = FindUrgencyLevel();
            if(urgency != 0 && !string.IsNullOrEmpty(TbRecommendation.Text))
            {
                viewModel.AddRecommendation(urgency, TbRecommendation.Text);
            }

            NavigationService.Navigate(new FinishRatingView(viewModel));
        }

        public int FindUrgencyLevel()
        {
            if ((bool)Rb1.IsChecked)
            {
                return 1;
            }
            else if ((bool)Rb2.IsChecked)
            {
                return 2;
            }
            else if ((bool)Rb3.IsChecked)
            {
                return 3;
            }
            else if ((bool)Rb4.IsChecked)
            {
                return 4;
            }
            else if ((bool)Rb5.IsChecked)
            {
                return 5;
            }
            return 0;
        }

        private void SkipRecommendation_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SkipRecommendation();
            NavigationService.Navigate(new FinishRatingView(viewModel));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
