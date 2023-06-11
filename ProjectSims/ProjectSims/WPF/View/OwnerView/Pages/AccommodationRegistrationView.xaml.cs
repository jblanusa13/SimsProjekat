using System;
using ProjectSims.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ProjectSims.FileHandler;
using Microsoft.Win32;
using System.Security;
using System.Collections.ObjectModel;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
using ProjectSims.WPF.View.OwnerView;
using System.IO;
using System.Windows.Media;
using System.Windows.Navigation;

namespace ProjectSims.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Page
    {
        public Owner Owner { get; set; }
        public AccommodationRegistrationViewModel accommodationRegistrationViewModel { get; set; }
        public NavigationService NavService { get; set; }
        public AccommodationRegistrationView(Owner o, TextBlock titleTextBlock, Accommodation selectedAccommodation, NavigationService navService)
        {
            InitializeComponent();
            Owner = o;
            NavService = navService;
            accommodationRegistrationViewModel = new AccommodationRegistrationViewModel(Owner, selectedAccommodation, NavService, this);
            this.DataContext = accommodationRegistrationViewModel;
        }
        private void DeleteImage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Image SelectedImage = (Image)ImageList.SelectedItem;
            if (SelectedImage != null && e.Key.Equals(Key.Delete))
            {
                accommodationRegistrationViewModel.DeleteImage(SelectedImage);
            }
        }
    }
}