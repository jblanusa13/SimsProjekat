using ProjectSims.Model;
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

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for RatingTourView.xaml
    /// </summary>
    public partial class RatingTourView : Window
    {
        public Guide guide { get; set; }
        public RatingTourView(Tour tour)
        {
            InitializeComponent();
            DataContext = this;

        }
    }
}
