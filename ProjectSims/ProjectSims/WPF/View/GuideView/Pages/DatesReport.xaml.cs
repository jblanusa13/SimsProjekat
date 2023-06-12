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
using ProjectSims.Domain.Model;
using System.Windows.Shapes;
using ProjectSims.WPF.ViewModel.GuideViewModel;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for DatesReport.xaml
    /// </summary>
    public partial class DatesReport : Page
    {
        public DatesReport(NavigationService navigationService,Guide guide)
        {
            InitializeComponent();
            this.DataContext = new DateReportViewModel(guide, navigationService,DateRange);
        }
    }
}
