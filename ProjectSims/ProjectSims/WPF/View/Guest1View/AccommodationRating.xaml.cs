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

namespace ProjectSims.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for AccommodationRating.xaml
    /// </summary>
    public partial class AccommodationRating : Window
    {
        public AccommodationReservation AccommodationReservation { get; set; }
        public Guest1 Guest { get; set; }
        public int Cleanliness { get; set; }
        public int Fairness { get; set; }
        public int Location { get; set; }
        public int ValueForMoney { get; set; }
        public string Comment { get; set; }

        private AccommodationRatingService ratingService;
        private AccommodationReservationService reservationService;
        public AccommodationRating(AccommodationReservation accommodationReservation, Guest1 guest)
        {
            InitializeComponent();

            AccommodationReservation = accommodationReservation;
            Guest = guest;

            ratingService = new AccommodationRatingService();
            reservationService = new AccommodationReservationService();
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
                Cleanliness = Convert.ToInt32(CleanlinessTb.Text);
                Fairness = Convert.ToInt32(FairnessTb.Text);
                Location = Convert.ToInt32(LocationTb.Text);
                ValueForMoney = Convert.ToInt32(ValueForMoneyTb.Text);

                string images = Images.Text.Remove(Images.Text.Length - 2, 2);
                List<string> imageList = new List<string>();
                foreach(string image in images.Split(",\n"))
                {
                    imageList.Add(image);
                }

                if (string.IsNullOrEmpty(CommentTb.Text))
                {
                    Comment = "";
                }
                else
                {
                    Comment = CommentTb.Text;
                }

                ratingService.CreateRating(Guest.Id, Guest, AccommodationReservation.Accommodation.Id, AccommodationReservation.Accommodation, Cleanliness, Fairness, Location, ValueForMoney, Comment, imageList);
                reservationService.ChangeReservationRatedState(AccommodationReservation);
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
