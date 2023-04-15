using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Interop;
using ProjectSims.Service;
using System.Collections.ObjectModel;
using ProjectSims.Observer;
using ProjectSims.WPF.View.Guest2View;

namespace ProjectSims.View.Guest2View
{
    /// <summary>
    /// Interaction logic for DetailsAndReservationTourView.xaml
    /// </summary>
    public partial class DetailsAndReservationTourView : Window
    {
        private TourService tourService;

        private ReservationTourService reservationService;

        private KeyPointService keyPointService;
        public Tour tour { get; set; }

        public ObservableCollection<Tour> TourList { get; set; }
        public Tour SelectedTour { get; set; }
        public Image image { get; set; }
        public Guest2 guest2 { get; set; }
        public DetailsAndReservationTourView(Tour tourSelected, Guest2 g)
        {
            InitializeComponent();
            DataContext = this;
            tour = tourSelected;
            guest2 = g;
            keyPointService = new KeyPointService();

            NameTextBox.Text = tourSelected.Name;
            LocationTextBox.Text = tourSelected.Location;
            DescriptionTextBox.Text = tourSelected.Description;
            LanguageTextBox.Text = tourSelected.Language;
            MaxGuestsTextBox.Text = tourSelected.MaxNumberGuests.ToString();

            foreach (int id in tourSelected.KeyPointIds)
            {
                if (id.Equals(tourSelected.KeyPointIds.Last()))
                {
                    KeyPointTextBox.Text += keyPointService.GetKeyPointById(id).Name;
                }
                else
                {
                    KeyPointTextBox.Text += keyPointService.GetKeyPointById(id).Name + ", ";
                }

            }

            DateStartTextBox.Text = tourSelected.StartOfTheTour.ToString();
            DurationTextBox.Text = tourSelected.Duration.ToString();

            //show picture in listview
            foreach (var fullFilePath in tourSelected.Images)
            {
                /*BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                if(Uri.IsWellFormedUriString(fullFilePath, UriKind.Absolute))
                {
                    bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                    bitmap.EndInit();

                    image = new Image();

                    image.Source = bitmap;

                    image.Width = 350;
                    image.Height = 200;

                    ImageList.Items.Add(image);
                }
                else
                {
                    ImageList.Items.Add("The format of the URL could not be determined.");
                }*/

                //kad se ubaci filepicker kod kreiranja tura i kad se u tour.csv budu cuvale relativne putanje

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                if (Uri.IsWellFormedUriString(fullFilePath, UriKind.RelativeOrAbsolute))
                {
                    bitmap.UriSource = new Uri(fullFilePath, UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();

                    image = new Image();

                    image.Source = bitmap;

                    image.Width = 350;
                    image.Height = 200;

                    ImageList.Items.Add(image);
                }
                else
                {
                    ImageList.Items.Add("The format of the URL could not be determined.");
                }
            }
            
            tourService = new TourService();
            reservationService = new ReservationTourService();
            TourList = new ObservableCollection<Tour>(tourService.GetAllToursWithSameLocation(tourSelected));
        }

        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            if (tour.State != 0)
            {
                MessageBox.Show("Za turu koju ste odabrali ne mozete izvrisiti rezervaciju jer je zapoceta,zavrsena ili otkazna!");
                return;
            }
            int numberGuests = CheckNumberGuestTextBox(NumberGuestsTextBox.Text);
            if (numberGuests == -1) return;

            MessageReservationBox.Text = "";
            if (numberGuests > tour.AvailableSeats)
            {
                MessageReservationBox.Text = "There are no available seats on this tour for the entered number of people. " +
                    "\nThe number of available seats is " + tour.AvailableSeats + "!";
            }
            else 
            {
                var useVoucherView = new UseVoucherView(guest2,tour,(int)numberGuests);
                useVoucherView.Show();

            }
            if (tour.AvailableSeats == 0)
            {
                AlternativeTextBlock.Text = "The selected tour is fully booked, some of the alternative tours are:";
                AlternativeToursGrid.Visibility = Visibility.Visible;

                if (SelectedTour != null)
                {
                    if (SelectedTour.State != 0)
                    {
                        MessageBox.Show("Za turu koju ste odabrali ne mozete izvrisiti rezervaciju jer je zapoceta,zavrsena ili otkazna!");
                        return;
                    }
                    MessageBlock.Text = "";
                    if (numberGuests > SelectedTour.AvailableSeats)
                    {
                        MessageBlock.Text = "The number of available seats is " + SelectedTour.AvailableSeats + "!";
                    }
                    else
                    {
                        var useVoucherView = new UseVoucherView(guest2, SelectedTour,(int)numberGuests);
                        useVoucherView.Show();
                    }
                }
            }

        }

        private int CheckNumberGuestTextBox(string text)
        {
            uint numberGuests;

            if (!uint.TryParse(text, out numberGuests))
            {
                MessageBox.Show("Wrong input! Number people on tour must be a positive number!");
                return -1;
            }

            if (numberGuests == 0)
            {
                MessageBox.Show("Wrong input! Number people on tour can't be a zero!");
                return -1;
            }
            return (int)numberGuests;
        }

    }
}
