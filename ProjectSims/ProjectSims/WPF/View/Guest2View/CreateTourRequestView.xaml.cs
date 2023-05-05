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
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for CreateTourRequestView.xaml
    /// </summary>
    public partial class CreateTourRequestView : Window
    {
        public CreateTourRequestView()
        {
            InitializeComponent();
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
