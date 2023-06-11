using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using ceTe.DynamicPDF;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.View.Guest1View.Report;
using ProjectSims.WPF.View.Guest1View.Requests;
using ProjectSims.Commands;
using ProjectSims.WPF.View.Guest1View.NotifAndHelp;
using ProjectSims.WPF.View.Guest1View.HelpPages;
using ProjectSims.WPF.View.Guest1View;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class MyReservationsViewModel : IObserver
    {
        public string GuestName { get; set; }
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public Guest1 Guest { get; set; }
        private AccommodationReservationService service;
        public NavigationService NavService { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand DateChangeCommand { get; set; }
        public RelayCommand MyRequestsCommand { get; set; }
        public RelayCommand CancelReservationCommand { get; set; }
        public RelayCommand ThemeCommand { get; set; }
        public RelayCommand NotifCommand { get; set; }
        public RelayCommand HelpCommand { get; set; }
        public RelayCommand GenerateActiveReservationsCommand { get; set; }
        public RelayCommand GenerateCanceledReservationsCommand { get; set; }
        public MyICommand<MyReservations> LogOutCommand { get; set; }

        public MyReservationsViewModel(Guest1 guest, NavigationService navigation)
        {
            Guest = guest;
            GuestName = guest.Name + " " + guest.Surname;
            service = new AccommodationReservationService();
            service.Subscribe(this);
            Reservations = new ObservableCollection<AccommodationReservation>(service.GetActiveReservationsByGuest(guest.Id));

            CancelCommand = new RelayCommand(Execute_CancelCommand);
            DateChangeCommand = new RelayCommand(Execute_DateChangeCommand, CanExecute_DateChangeCommand);
            MyRequestsCommand = new RelayCommand(Execute_MyRequestsCommand);
            CancelReservationCommand = new RelayCommand(Execute_CancelReservationCommand, CanExecute_CancelReservationCommand);
            ThemeCommand = new RelayCommand(Execute_ThemeCommand);
            NotifCommand = new RelayCommand(Execute_NotifCommand);
            GenerateActiveReservationsCommand = new RelayCommand(Execute_GenerateActiveReservationsCommand);
            GenerateCanceledReservationsCommand = new RelayCommand(Execute_GenerateCanceledReservationsCommand);
            LogOutCommand = new MyICommand<MyReservations>(OnLogOut);
            HelpCommand = new RelayCommand(Execute_HelpCommand);


            NavService = navigation;
        }
        private void Execute_HelpCommand(object obj)
        {
            HelpStartView helpStart = new HelpStartView();
            helpStart.SelectedTab.Content = new MyReservationsHelpView();
            helpStart.Show();
        }
        private void Execute_GenerateActiveReservationsCommand(object obj)
        {
            PrintDialog printDialog = new PrintDialog();
            ReportToGenerate rtg = new ReportToGenerate(Guest);
            FlowDocument fd = rtg.Document;
            DocumentPaginator documentPaginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
            printDialog.PrintDocument(documentPaginator, "Izvestaj");
        }
        private void Execute_GenerateCanceledReservationsCommand(object obj)
        {
            PrintDialog printDialog = new PrintDialog();
            CanceledReport rtg = new CanceledReport(Guest);
            FlowDocument fd = rtg.Document;
            DocumentPaginator documentPaginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
            printDialog.PrintDocument(documentPaginator, "Izvestaj");
        }
        private void Execute_ThemeCommand(object obj)
        {
            App app = (App)Application.Current;

            if (App.IsDark)
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                App.IsDark = false;
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                App.IsDark = true;
            }
        }
        private void Execute_NotifCommand(object obj)
        {
            NotificationsView notificationsView = new NotificationsView(Guest);
            notificationsView.Show();
        }
        private void Execute_CancelReservationCommand(object obj)
        {
            if (service.CanCancel(SelectedReservation))
            {
                service.RemoveReservation(SelectedReservation);
                MessageBox.Show("Uspesno ste otkazali rezervaciju");
            }
            else
            {
                MessageBox.Show("Rok za otkazivanje je prosao, ne mozete otkazati rezervaciju");
            }
        }
        private bool CanExecute_CancelReservationCommand(object obj)
        {
            return SelectedReservation != null;
        }

        private void Execute_MyRequestsCommand(object obj)
        {
            MyRequests myRequests = new MyRequests(Guest);
            myRequests.Show();
        }
        private void Execute_DateChangeCommand(object obj)
        {
            DateChangeRequest request = new DateChangeRequest(SelectedReservation);
            request.Show();
        }
        private bool CanExecute_DateChangeCommand(object obj)
        {
            return SelectedReservation != null;
        }
        private void Execute_CancelCommand(object obj)
        {
            NavService.GoBack();
        }
        private void OnLogOut(MyReservations page)
        {
            var login = new MainWindow();
            login.Show();
            Window parentWindow = Window.GetWindow(page);
            parentWindow.Close();
        }
        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in service.GetActiveReservationsByGuest(Guest.Id))
            {
                Reservations.Add(reservation);
            }
        }
    }
}
