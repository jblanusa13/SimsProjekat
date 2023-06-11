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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.Requests;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Page
    {
        public MyReservations(Guest1 guest, NavigationService navigation)
        {
            InitializeComponent();
            DataContext = new MyReservationsViewModel(guest, navigation);
            BackButton.Focus();
        }


        private void SelectAccommodation_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key.Equals(Key.S)))
            {
                ChangeButton.Focus();
            }
        }
    }
}
