﻿using ProjectSims.Domain.Model;
using ProjectSims.WPF.View.GuideView.Pages;
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
using System.Windows.Shapes;
using ProjectSims.Service;
using ProjectSims.Observer;

namespace ProjectSims.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuideStartingView.xaml
    /// </summary>
    public partial class GuideStartingView : Window, IObserver
    {
        public TourService tourService { get; set; }
        public Guide Guide { get; set; }
        public Tour ActiveTour { get; set; }
        public GuideStartingView(Guide g)
        {
            InitializeComponent();
            tourService = new TourService();
            Guide = g;
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Window login = new MainWindow();
            login.Show();
        }
        private void TrackTour_Click(object sender, RoutedEventArgs e)
        {
            //ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
            if (ActiveTour != null)
            {
                Page tourTrackingPage = new TourTrackingView(ActiveTour,Guide);
                GuideFrame.Content = tourTrackingPage;
            }
        }
        private void AvailableTours_Click(object sender, RoutedEventArgs e)
        {
            Page availableToursPage = new AvailableToursView(Guide);
            GuideFrame.Content = availableToursPage;
        }
        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            Page createTourPage = new CreateTourView(Guide);
            GuideFrame.Content = createTourPage;
        }
        private void ScheduledTours_Click(object sender, RoutedEventArgs e)
        {
            Page scheduledToursView = new ScheduledToursView(Guide);
            GuideFrame.Content = scheduledToursView;
        }
        private void FinishedTour_Click(object sender, RoutedEventArgs e)
        {
            Page finishedToursView = new FinishedToursStatisticsView(Guide);
            GuideFrame.Content = finishedToursView;
        }
        public void Update()
        {
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
        }
    }
}
