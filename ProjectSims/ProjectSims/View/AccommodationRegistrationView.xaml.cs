using System;
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
using ProjectSims.ModelDAO;
using ProjectSims.FileHandler;

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly AccommodationController _accommodationController;
        private readonly OwnerController _ownerController;
        private AccommodationDAO _accommodationDAO;
        private OwnerDAO _ownerDAO;
        public AccommodationRegistrationView()
        {
            InitializeComponent();
            DataContext = this;

            _accommodationController = new AccommodationController();
            _ownerController = new OwnerController();
            _accommodationDAO = new AccommodationDAO();
            _ownerDAO = new OwnerDAO();
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

        private int dismissalDays = 1;
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
        private string address;
        public string Address
        {
            get => address;

            set
            {
                if (value != address)
                {
                    address = value;
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
            if (IsValid && !string.IsNullOrEmpty(AccommodationNameTextBox.Text)
                        && !string.IsNullOrEmpty(LocationTextBox.Text)
                        && !string.IsNullOrEmpty(GuestsMaximumTextBox.Text)
                        && !string.IsNullOrEmpty(MinimumReservationDaysTextBox.Text)
                        && !string.IsNullOrEmpty(DismissalDaysTextBox.Text)
                        && !string.IsNullOrWhiteSpace(ImagesTextBox.Text))
            {
                _accommodationDAO.Add(Location);
                int IdLocation = _accommodationDAO.GetLocationId(Location);
                List<string> Images = new List<string>();
                foreach (string image in ImagesTextBox.Text.Split(","))
                {
                    Images.Add(image);
                }
                int idCurrentOwner = MainWindow.CurrentUserId;
                Accommodation accommodation = new Accommodation(-1, AccommodationName, IdLocation, Type, GuestsMaximum, MinimumReservationDays, DismissalDays, Images, idCurrentOwner);
                _accommodationController.Create(accommodation);
                Owner owner = _ownerDAO.FindById(idCurrentOwner);
                _ownerDAO.AddAccommodationId(owner, accommodation.Id);
                _ownerController.Update(_ownerDAO.FindById(idCurrentOwner));
                MessageBox.Show("Uspješno registrovan smještaj!", "Registracija smještaja", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            } 
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;

                if (columnName == "AccommodationName")
                {
                    if (string.IsNullOrEmpty(AccommodationName))
                    {
                        return "Unesite vrijednost!";
                    }
                    else if (!Regex.IsMatch(AccommodationName, @"^[a-zA-Z0-9,. ]*$"))
                    {
                        return "Iskljucivo: slova[a-z] i brojevi[0-9]!";
                    }
                }
                else if (columnName == "Location")
                {
                    if (string.IsNullOrWhiteSpace(Location))
                    {
                        return "Unesite vrijednost!";
                    }
                    else if (!Regex.IsMatch(LocationTextBox.Text, @"^[a-zA-Z]+[,]{1}[a-zA-Z]+$"))
                    {
                        return "Iskljucivo: slova[a-z] u formatu Grad,Drzava!";
                    }
                }
                else if (columnName == "GuestsMaximum")
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(GuestsMaximum)))
                    {
                        return "Unesite vrijednost!";
                    }
                    else if (GuestsMaximum < 1)
                    {
                        return "Iskljucivo brojevi veci od 0!";
                    }
                }
                else if (columnName == "MinimumReservationDays")
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(MinimumReservationDays)))
                    {
                        return "Unesite vrijednost!";
                    }
                    else
                    {
                        try
                        {
                            if (MinimumReservationDays < 1)
                            {
                                return "Iskljucivo brojevi veci od 0!";
                            }
                        }
                        catch (Exception e1)
                        {
                            return "Iskljucivo brojevi veci od 0!";
                        }
                    }
                }
                else if (columnName == "DismissalDays")
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(DismissalDays)))
                    {
                        return "Unesite vrijednost!";
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToInt32(DismissalDays) < 0)
                            {
                                return "Iskljucivo nenegativni brojevi!";
                            }
                        }
                        catch (Exception e1)
                        {
                            return "Iskljucivo nenegativni brojevi!";
                        }
                    }
                }
                else if (columnName == "Images")
                {
                    if (string.IsNullOrWhiteSpace(Images)) 
                    {
                        return "Unesite slike!";
                    }
                }
                    return null;
            }
        }

        private readonly string[] validatedProperties = { "AccommodationName", "Location", "GuestsMaximum", "MinimumReservationDays", "DismissalDays", "Images" };

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
    }
}