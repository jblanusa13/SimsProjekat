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
using ProjectSims.Service;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for AccommodationRating.xaml
    /// </summary>
    public partial class AccommodationRating : Window
    {
        public AccommodationRatingViewModel ViewModel { get; set; }
        public Guest1 Guest { get; set; }
        public AccommodationRating(AccommodationReservation accommodationReservation, Guest1 guest)
        {
            InitializeComponent();
            ViewModel = new AccommodationRatingViewModel(accommodationReservation, guest);
            Guest = guest;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDlg.ShowDialog();

            string fileName = "";
            if(result == true)
            {
                fileName = openFileDlg.SafeFileName;
            }
            else
            {
                return;
            }
            Images.Text += GetRelativePath(fileName) + ",\n";
        }

        private string GetRelativePath(string fileName)
        {
            return "../../../Resources/Images/Guest1/" + fileName;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(CleanlinessTb.Text) && !string.IsNullOrEmpty(FairnessTb.Text) && !string.IsNullOrEmpty(LocationTb.Text) && !string.IsNullOrEmpty(ValueForMoneyTb.Text))
            {
                ViewModel.Confirm(CleanlinessTb.Text, FairnessTb.Text, LocationTb.Text, ValueForMoneyTb.Text, CommentTb.Text, Images.Text);
            }
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsForRating accommodationForRating = new AccommodationsForRating(Guest);
            accommodationForRating.Show();
            Close();
        }
    }
}
