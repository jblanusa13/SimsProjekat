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
using ProjectSims.Domain.Model;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.ForumPages
{
    /// <summary>
    /// Interaction logic for CreateForum.xaml
    /// </summary>
    public partial class CreateForum : Page
    {
        public CreateForum(Guest1 guest)
        {
            InitializeComponent();
            this.DataContext = new CreateForumViewModel(guest);
        }
    }
}