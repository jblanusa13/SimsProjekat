using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.RatingPages;

namespace ProjectSims.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for AccommodationsForRating.xaml
    /// </summary>
    public partial class RatingStartView : Window
    {
        public RatingStartView(Guest1 guest)
        {
            InitializeComponent();
            SelectedTab.Content = new AccommodationsForRatingView(guest, SelectedTab);
        }
    }
}
