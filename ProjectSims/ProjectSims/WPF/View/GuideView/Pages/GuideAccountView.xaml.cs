using ProjectSims.Domain.Model;
using ProjectSims.WPF.ViewModel.GuideViewModel;
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

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for GuideAccountView.xaml
    /// </summary>
    public partial class GuideAccountView : Page
    {
        public Guide Guide { get; set; }
        public GuideAccountView(Guide guide)
        {
            InitializeComponent();
            DataContext = this;
            Guide = guide;
        }
        public void Dismissal_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Logout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

