using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ProjectSims.Commands;
using System.Windows;
using Syncfusion.Windows.Controls;
using ProjectSims.WPF.View.Guest1View.Report;
using System.Windows.Controls;
using System.Windows.Documents;
using Calendar = System.Windows.Controls.Calendar;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class DateReportViewModel
    {
        private TourService tourService;
        public NavigationService NavService { get; set; }
        public Guide Guide { get; set; }
        public RelayCommand GenerateReportCommand { get; set; }
        public RelayCommand HomeCommand { get; set; }
        public RelayCommand ScheduledToursCommand { get; set; }
        public Calendar DateRange { get; set; }
        public DateReportViewModel(Guide guide, NavigationService navigationService,Calendar dateRange)
        {
            NavService = navigationService;
            Guide = guide;
            tourService = new TourService();
            GenerateReportCommand = new RelayCommand(Execute_GenerateReportCommand);
            ScheduledToursCommand = new RelayCommand(Execute_ScheduledToursCommand);
            HomeCommand = new RelayCommand(Execute_HomeCommand);
            DateRange = dateRange;
        }
        private void Execute_GenerateReportCommand(object obj)
        {
            PrintDialog printDialog = new PrintDialog();
            Report report = new Report(Guide, DateRange.SelectedDates.First(), DateRange.SelectedDates.Last());
            FlowDocument fd = report.Document;
            DocumentPaginator documentPaginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
            printDialog.PrintDocument(documentPaginator, "Izvestaj");
        }
        private void Execute_HomeCommand(object obj)
        {
            NavService.Navigate(new HomeView(Guide, NavService));
        }
        private void Execute_ScheduledToursCommand(object obj)
        {
            NavService.Navigate(new ScheduledToursView(Guide, NavService));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
