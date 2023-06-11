using ProjectSims.Domain.Model;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;
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

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for ForumComments.xaml
    /// </summary>
    public partial class ForumCommentsView : Page
    {
        public ForumCommentsView(Guest1 guest, NavigationService navigation, Forum forum)
        {
            InitializeComponent();
            DataContext = new ForumCommentsViewModel(guest, navigation, this, forum);
            BackButton.Focus();
        }
    }
}
