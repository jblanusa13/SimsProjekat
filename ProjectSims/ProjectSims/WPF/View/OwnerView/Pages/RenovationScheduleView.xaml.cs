using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for Renovation.xaml
    /// </summary>
    public partial class RenovationScheduleView : Page
    {
        public RenovationScheduleView(Owner owner, OwnerStartingView window, Accommodation selectedAccommodation, NavigationService navService)
        {
            InitializeComponent();
            DataContext = new RenovationScheduleViewModel(window, this, selectedAccommodation, owner, navService);
        }
    }
}
