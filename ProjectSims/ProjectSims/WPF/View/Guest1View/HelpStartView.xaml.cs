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
using ProjectSims.WPF.View.Guest1View.BarPages;
using ProjectSims.WPF.View.Guest1View.HelpPages;

namespace ProjectSims.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for HelpStartView.xaml
    /// </summary>
    public partial class HelpStartView : Window
    {
        public HelpStartView()
        {
            InitializeComponent();
            StatusBarFrame.Content = new HelpStatusBar();
            SelectedTab.Content = new MainHelpView();
            WindowBarFrame.Content = new HelpWindowBar();
            BackButton.Focus();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
