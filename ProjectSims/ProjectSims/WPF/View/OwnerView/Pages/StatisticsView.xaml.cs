using ProjectSims.Domain.Model;
using ProjectSims.WPF.View.Guest2View;
using ProjectSims.WPF.ViewModel.Guest2ViewModel;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
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

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : Page
    {
        public Owner Owner { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public StatisticsViewModel statisticsViewModel { get; set; }
        public StatisticsView(Owner o, TextBlock titleTextBlock, Accommodation selectedAccommodetion)
        {
            InitializeComponent();
            Owner = o;
            TitleTextBlock = titleTextBlock;
            SelectedAccommodation = selectedAccommodetion;
            statisticsViewModel = new StatisticsViewModel(Owner, TitleTextBlock, SelectedAccommodation);
            this.DataContext = statisticsViewModel;
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
        
        }
    }
}
