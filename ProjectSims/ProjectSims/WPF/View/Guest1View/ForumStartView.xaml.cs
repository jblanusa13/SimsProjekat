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
using ProjectSims.Domain.Model;
using ProjectSims.WPF.View.Guest1View.BarPages;
using ProjectSims.WPF.View.Guest1View.ForumPages;
using ProjectSims.WPF.View.Guest1View.RatingPages;

namespace ProjectSims.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for ForumStartView.xaml
    /// </summary>
    public partial class ForumStartView : Window
    {
        public ForumStartView(Guest1 guest)
        {
            InitializeComponent();
            SelectedTab.Content = new CreateForum(guest);
            StatusBarFrame.Content = new ForumStatusBar();
            WindowBarFrame.Content = new ForumWindowBar();
        }
    }
}