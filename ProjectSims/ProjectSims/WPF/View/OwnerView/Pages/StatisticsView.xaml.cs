using ceTe.DynamicPDF;
using ceTe.DynamicPDF.IO;
using ceTe.DynamicPDF.LayoutEngine;
using ceTe.DynamicPDF.Merger;
using LiveCharts;
using ProjectSims.Domain.Model;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.View.Guest2View;
using ProjectSims.WPF.ViewModel.Guest2ViewModel;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : System.Windows.Controls.Page
    {
        public StatisticsView(Owner owner, OwnerStartingView window, Accommodation selectedAccommodetion, NavigationService navService)
        {
            InitializeComponent();
            DataContext = new StatisticsViewModel(owner, this, window, selectedAccommodetion, navService);
        }
    }
}
