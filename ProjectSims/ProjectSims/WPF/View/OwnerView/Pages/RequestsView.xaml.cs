using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using ProjectSims.Repository;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.ViewModel.GuideViewModel;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
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
    public partial class RequestsView : Page
    {
        public Owner owner;
        public Request SelectedRequest { get; set; }
        public RequestsViewModel requestsViewModel { get; set; }

        public RequestsView(Owner o)
        {
            InitializeComponent();
            owner = o;
            requestsViewModel = new RequestsViewModel(o);
            this.DataContext = requestsViewModel;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            SelectedRequest = (Request)RequestsTable.SelectedItem;
            requestsViewModel.UpdateSelectedRequest(sender, SelectedRequest, CommentTextBox.Text);
            CommentTextBox.Text = "Unesite komentar ukoliko odbijate zahtjev...";
        }

        private void Refuse_Click(object sender, RoutedEventArgs e)
        {
            SelectedRequest = (Request)RequestsTable.SelectedItem;
            requestsViewModel.UpdateSelectedRequest(sender, SelectedRequest, CommentTextBox.Text);
            CommentTextBox.Text = "Unesite komentar ukoliko odbijate zahtjev...";
        }

        private void CommentTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            if (CommentTextBox.Text == "Unesite komentar ukoliko odbijate zahtjev...")
            {
                source.Background = Brushes.MintCream;
                source.Foreground = Brushes.Black;
                source.Clear();
            }
        }

        private void CommentTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            if (CommentTextBox.Text == "")
            {
                CommentTextBox.Text = "Unesite komentar ukoliko odbijate zahtjev...";
                source.Background = Brushes.White;
                source.Foreground = Brushes.Gray;
            }
        }
    }
}
