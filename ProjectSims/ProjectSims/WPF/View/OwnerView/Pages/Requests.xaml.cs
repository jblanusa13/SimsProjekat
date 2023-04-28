using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using ProjectSims.Repository;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : Page, IObserver
    {
        public Owner owner;
        public Request SelectedRequest { get; set; }
        public ObservableCollection<Request> RequestList { get; set; }
        private RequestService requestService;
        private AccommodationReservationService accommodationReservationService;

        public Requests(Owner o)
        {
            InitializeComponent();
            DataContext = this;
            owner = o;
            requestService = new RequestService();
            requestService.Subscribe(this);
            RequestList = new ObservableCollection<Request>(requestService.GetAllRequests());
            accommodationReservationService = new AccommodationReservationService();
        }

        public void Update()
        {
            RequestList.Clear();
            foreach (Request request in requestService.GetAllRequests())
            {
                RequestList.Add(request);
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            requestService.UpdateSelectedRequest(sender, SelectedRequest, RequestsTable);
        }

        private void RefuseButton_Click(object sender, RoutedEventArgs e)
        {
            requestService.UpdateSelectedRequest(sender, SelectedRequest, RequestsTable);
        }

        private void CommentTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (CommentTextBox.Text == "Unesite komentar ukoliko odbijate zahtjev...")
            {
                CommentTextBox.Text = "";
            }

            CommentTextBox.SelectionTextBrush = Brushes.Black;
        }

        private void CommentTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (CommentTextBox.Text == "")
            {
                CommentTextBox.Text = "Unesite komentar ukoliko odbijate zahtjev...";
            }

            CommentTextBox.SelectionTextBrush = Brushes.Black;
        }
    }
}
