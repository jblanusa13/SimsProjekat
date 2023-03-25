using ProjectSims.Model;
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
using ProjectSims.Controller;
using System.Collections.ObjectModel;
using ProjectSims.Observer;

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for DetailsAndReservationTourView.xaml
    /// </summary>
    public partial class DetailsAndReservationTourView : Window
    {
        private TourController tourController;

        private ReservationTourController reservationController;

        private KeyPointController keyPointController;
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
            keyPointController = new KeyPointController();
            
            NameTextBox.Text = tourSelected.Name;
            LocationTextBox.Text = tourSelected.Location;
            DescriptionTextBox.Text = tourSelected.Description;
            LanguageTextBox.Text = tourSelected.Language;
            MaxGuestsTextBox.Text = tourSelected.MaxNumberGuests.ToString();

            foreach(int id in tourSelected.KeyPointIds) 
            {
                if (id.Equals(tourSelected.KeyPointIds.Last()))
                {
                    KeyPointTextBox.Text += keyPointController.FindNameById(id);
                }
                else
                {
                    KeyPointTextBox.Text += keyPointController.FindNameById(id) + ", ";
                }

            }

            DateStartTextBox.Text = tourSelected.StartOfTheTour.ToString();
            DurationTextBox.Text = tourSelected.Duration.ToString();

            //show picture in listview
            foreach (var fullFilePath in tourSelected.Images)
            {
                BitmapImage bitmap = new BitmapImage();
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
                }
            }
           
            tourController = new TourController();
            TourList = new ObservableCollection<Tour>(tourController.GetAllToursWithSameLocation(tourSelected));

        }

        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            uint numberGuests;
            reservationController = new ReservationTourController();
            tourController = new TourController();

            if (!uint.TryParse(NumberGuestsTextBox.Text, out numberGuests))
            {
                MessageBox.Show("Wrong input! Number people on tour must be a positive number!");
                return;
            }

            if(numberGuests == 0)
            {
                MessageBox.Show("Wrong input! Number people on tour can't be a zero!");
                return;
            }

            MessageReservationBox.Text = "";
            if (numberGuests > tour.AvailableSeats)
            {
                MessageReservationBox.Text = "There are no available seats on this tour for the entered number of people. \nThe number of available seats is " + tour.AvailableSeats + "!";
            }
            else 
            {
                ReservationTour reservation = new ReservationTour(tour.Id, (int)numberGuests, guest2.Id,-1);
                reservationController.Create(reservation);
                tour.AvailableSeats -= (int)numberGuests;
                tourController.Update(tour);
                MessageBox.Show("Reservation successful! \nUser " + guest2.Name + " " + guest2.Surname + 
                    " has made a reservation for " + numberGuests.ToString() + " people on the tour " + tour.Name + ".");
                NumberGuestsTextBox.Clear();
                return;
            }

            if (tour.AvailableSeats == 0)
            {
                AlternativeTextBlock.Text = "The selected tour is fully booked, some of the alternative tours are:";
                AlternativeToursGrid.Visibility = Visibility.Visible;

                if (SelectedTour != null)
                {
                    MessageBlock.Text = "";
                    if (numberGuests > SelectedTour.AvailableSeats)
                    {
                        MessageBlock.Text = "The number of available seats is " + SelectedTour.AvailableSeats + "!";
                    }
                    else
                    {
                        ReservationTour reservationAlternative = new ReservationTour(SelectedTour.Id, (int)numberGuests, guest2.Id,-1);
                        reservationController.Create(reservationAlternative);
                        SelectedTour.AvailableSeats -= (int)numberGuests;
                        tourController.Update(SelectedTour);
                        MessageBox.Show("Reservation successful! \nUser " + guest2.Name + " " + guest2.Surname +
                                 " has made a reservation for " + numberGuests.ToString() + " people on the tour " + SelectedTour.Name + ".");
                        NumberGuestsTextBox.Clear();
                        return;
                    }
                }
            }

        }

    }
}
