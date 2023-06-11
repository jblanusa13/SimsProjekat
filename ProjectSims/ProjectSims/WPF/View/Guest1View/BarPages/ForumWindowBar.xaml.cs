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

namespace ProjectSims.WPF.View.Guest1View.BarPages
{
    /// <summary>
    /// Interaction logic for ForumWindowBar.xaml
    /// </summary>
    public partial class ForumWindowBar : Page
    {
        public ForumWindowBar()
        {
            InitializeComponent();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            ForumStartView startView = (ForumStartView)Window.GetWindow(this);
            startView.Close();
        }
    }
}