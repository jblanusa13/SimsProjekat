﻿using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.ViewModel.GuideViewModel;
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

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourRequestsView.xaml
    /// </summary>
    public partial class TourRequestsView : Page
    {
        public TourRequestsViewModel tourRequestViewModel { get; set; }
        public TourRequest SelectedTourRequest { get; set; }
        public TourRequestsView(Guide guide)
        {
            InitializeComponent();
            tourRequestViewModel = new TourRequestsViewModel(guide);
            this.DataContext = tourRequestViewModel;
        }

        private void AcceptRequest_Click(object sender, RoutedEventArgs e)
        {
            SelectedTourRequest = ((FrameworkElement)sender).DataContext as TourRequest;
        }

    }
}
