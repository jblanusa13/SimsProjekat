using ProjectSims.Domain.Model;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePageView : Page
    {
        public HomePageView(Owner owner, NavigationService navService, OwnerStartingView view)
        {
            InitializeComponent();
            DataContext = new HomePageViewModel(owner, navService, view);
        }
    }
}
