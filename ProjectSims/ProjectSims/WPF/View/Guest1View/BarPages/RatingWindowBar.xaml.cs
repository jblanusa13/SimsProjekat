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
using ProjectSims.View.Guest1View;

namespace ProjectSims.WPF.View.Guest1View.BarPages
{
    /// <summary>
    /// Interaction logic for RatingWindowBar.xaml
    /// </summary>
    public partial class RatingWindowBar : Page
    {
        public RatingWindowBar()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            RatingStartView startView = (RatingStartView)Window.GetWindow(this);
            startView.Close();
        }
    }
}
