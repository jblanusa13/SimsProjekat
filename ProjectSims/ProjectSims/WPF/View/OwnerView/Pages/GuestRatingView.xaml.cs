using Microsoft.VisualBasic;
using ProjectSims.Service;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using ProjectSims.WPF.View.OwnerView;
using ProjectSims.WPF.View.OwnerView.Pages;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
using System.Windows.Navigation;

namespace ProjectSims.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for GuestRatingtView.xaml
    /// </summary>
    public partial class GuestRatingView : Page
    {
        public GuestRatingView(AccommodationReservation selectedAccommodationReservation, Owner o, OwnerStartingView window, NavigationService navService)
        {
            InitializeComponent();
            DataContext = new GuestRatingViewModel(selectedAccommodationReservation, this, window, o, navService);
        }
    }
}