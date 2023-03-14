using ProjectSims.Controller;
using ProjectSims.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window, INotifyPropertyChanged
    {
        private readonly AccommodationController accommodationController;
        private readonly OwnerController ownerController;
        public AccommodationRegistrationView()
        {
            InitializeComponent();
            DataContext = this;

            accommodationController = new AccommodationController();
            ownerController = new OwnerController();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string accommodationName;
        public string AccommodationName
        {
            get => accommodationName;

            set
            {
                if (value != accommodationName)
                {
                    accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string location;
        public string Location
        {
            get => location;

            set
            {
                if (value != location)
                {
                    location = value;
                    OnPropertyChanged();
                }
            }
        }

        private AccommodationType type;
        public AccommodationType Type
        {
            get => type;

            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }

        private int guestsMaximum;
        public int GuestsMaximum
        {
            get => guestsMaximum;

            set
            {
                if (value != guestsMaximum)
                {
                    guestsMaximum = value;
                    OnPropertyChanged();
                }
            }
        }

        private int minimumReservationDays;
        public int MinimumReservationDays
        {
            get => minimumReservationDays;

            set
            {
                if (value != minimumReservationDays)
                {
                    minimumReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private int dismissalDays;
        public int DismissalDays
        {
            get => dismissalDays;

            set
            {
                if (value != dismissalDays)
                {
                    dismissalDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private string images;
        public string Images
        {
            get => images;

            set
            {
                if (value != images)
                {
                    images = value;
                    OnPropertyChanged();
                }
            }
        }

        private string ownerName;
        public string OwnerName
        {
            get => ownerName;

            set
            {
                if (value != ownerName)
                {
                    ownerName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string ownerSurname;
        public string OwnerSurname
        {
            get => ownerSurname;

            set
            {
                if (value != ownerSurname)
                {
                    ownerSurname = value;
                    OnPropertyChanged();
                }
            }
        }
        private string adress;
        public string Adress
        {
            get => adress;

            set
            {
                if (value != adress)
                {
                    adress = value;
                    OnPropertyChanged();
                }
            }
        }
        private string email;
        public string Email
        {
            get => email;

            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }
        private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            Accommodation accommodation = new Accommodation(-1, AccommodationName, Location, Type, GuestsMaximum, MinimumReservationDays, DismissalDays, Images, null, -1);
            accommodationController.Create(accommodation);
            MessageBox.Show("Uspješno registrovan smještaj!", "Registracija smještaja", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        /*public string Error => null;
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;

                if (columnName == "AccommodationName")
                {
                    if (string.IsNullOrEmpty(AccommodationName))
                    {
                        result = "Naziv je potreban!";
                        return result;
                    }
                    else if (!Regex.IsMatch(AccommodationName, @"^[a-zA-Z][a-zA-Z0-9\,\.]+$"))
                    {
                        result = "Samo latinicna slova!";
                        return result;
                    }
                }

                else if (columnName == "GuestsMaximum")
                {
                    if (GuestsMaximum < 1)
                        return "Nevalidna vrijednost!";
                    try
                    {
                        Int32.Parse("GuestsMaximum");
                    }
                    catch (Exception e1)
                    {
                        return "Vrijednost mora biti veca od 0!";
                    }
                }
                else if (columnName == "MinimumReservationDays")
                {
                    if (MinimumReservationDays < 1)
                        return "Nevalidna vrijednost!";
                    try
                    {
                        Int32.Parse("MinimumReservationDays");
                    }
                    catch (Exception e2)
                    {
                        return "Vrijednost mora biti veca od 0!";
                    }
                }
                else if (columnName == "DismissalDays")
                {
                    if (DismissalDays < 0)
                        return "Nevalidna vrijednost!";
                    try
                    {
                        Int32.Parse("DismissalDays");
                    }
                    catch (Exception e3)
                    {
                        return "Vrijednost mora biti 0 ili veca od 0!";
                    }
                }
                return null;
            }
        }

        private readonly string[] validatedProperties = { "AccommodationName", "GuestsMaximum", "MinimumReservationDays", "DismissalDays" };

        public bool IsValid
        {
            get
            {
                foreach (var property in validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
        */
    }
}