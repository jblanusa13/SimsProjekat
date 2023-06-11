using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
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
using ProjectSims.Validation;
using ProjectSims.WPF.View.Guest1View;
using ProjectSims.WPF.View.Guest1View.RatingPages;
using ProjectSims.WPF.ViewModel.Guest1ViewModel;

namespace ProjectSims.WPF.View.Guest1View.MainPages
{
    /// <summary>
    /// Interaction logic for AccommodationReservation.xaml
    /// </summary>
    public partial class AccommodationReservationView : Page
    {
        private AccommodationReservationViewModel viewModel;
        public AccommodationReservationView(Accommodation SelectedAccommodation, Guest1 guest, NavigationService navigation)
        {
            InitializeComponent();
            viewModel = new AccommodationReservationViewModel(guest, SelectedAccommodation, this, navigation);
            DataContext = viewModel;

            LoadImages(SelectedAccommodation.Images);

            BackButton.Focus();
        }

        private void LoadImages(List<string> pathList)
        {
            foreach(string path in pathList)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();

                Image image = new Image();
                image.Source = bitmapImage;
                image.Height = 150;
                image.Width = 220;
                ImageList.Items.Add(image);
            }
        }

        private void Reserve(object sender, KeyEventArgs e)
        {
            if ((e.Key.Equals(Key.Enter)) || (e.Key.Equals(Key.Return)))
            {
                ReserveBtn.Focus();
            }
        }

    }
}
