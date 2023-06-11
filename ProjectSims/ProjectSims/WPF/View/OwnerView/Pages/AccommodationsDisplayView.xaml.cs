using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tulpep.NotificationWindow;
using ProjectSims.WPF.View.OwnerView;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
using ProjectSims.WPF.View.GuideView.Pages;
using ProjectSims.WPF.View.OwnerView.Pages;
using System.Windows.Navigation;

namespace ProjectSims.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class AccommodationsDisplayView : Page
    {
        public AccommodationsDisplayViewModel accommodationsDisplayViewModel;
        public Owner Owner { get; set; }

        public AccommodationsDisplayView(Owner o, TextBlock titleTextBlock, NavigationService navService)
        {
            InitializeComponent();
            Owner = o;
            accommodationsDisplayViewModel = new AccommodationsDisplayViewModel(Owner, navService, this, titleTextBlock);
            DataContext = accommodationsDisplayViewModel;
            NotifyAboutRequest();
            accommodationsDisplayViewModel.UpdateAccommodationsIfRenovated();
        }

        public void NotifyAboutRequest()
        {
            if (accommodationsDisplayViewModel.HasWaitingRequests(Owner))
            {
                MessageBox.Show("Imate zahteve na čekanju!");
            }
        }
    }
}
