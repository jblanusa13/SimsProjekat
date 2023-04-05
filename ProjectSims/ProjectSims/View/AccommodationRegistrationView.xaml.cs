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
using Microsoft.Win32;
using System.Security;

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

        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;

            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _location;
        public string Location
        {
            get => _location;

            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private AccommodationType _type;
        public AccommodationType Type
        {
            get => _type;

            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _guestsMaximum;
        public int GuestsMaximum
        {
            get => _guestsMaximum;

            set
            {
                if (value != _guestsMaximum)
                {
                    _guestsMaximum = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _minimumReservationDays;
        public int MinimumReservationDays
        {
            get => _minimumReservationDays;

            set
            {
                if (value != _minimumReservationDays)
                {
                    _minimumReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _dismissalDays = 1;
        public int DismissalDays
        {
            get => _dismissalDays;

            set
            {
                if (value != _dismissalDays)
                {
                    _dismissalDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _images;
        public string Images
        {
            get => _images;

            set
            {
                if (value != _images)
                {
                    _images = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ownerName;
        public string OwnerName
        {
            get => _ownerName;

            set
            {
                if (value != _ownerName)
                {
                    _ownerName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _ownerSurname;
        public string _OwnerSurname
        {
            get => _ownerSurname;

            set
            {
                if (value != _ownerSurname)
                {
                    _ownerSurname = value;
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
        private string _email;
        public string Email
        {
            get => _email;

            set
            {
                if (value != _email)
                {
                    _email = value;
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
                Location location = new Location(IdLocation, Location.ToString().Split(",")[0], Location.ToString().Split(",")[1]);
                Accommodation accommodation = new Accommodation(-1, AccommodationName, IdLocation, location, Type, GuestsMaximum, MinimumReservationDays, DismissalDays, Images, idCurrentOwner);
                _accommodationController.Create(accommodation);
                Owner owner = _ownerDAO.FindById(idCurrentOwner);
                _ownerDAO.AddAccommodationId(owner, accommodation.Id);
                _ownerController.Update(_ownerDAO.FindById(idCurrentOwner));
                MessageBox.Show("Uspješno registrovan smještaj!", "Registracija smještaja", MessageBoxButton.OK, MessageBoxImage.Information);
                
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
        private void LoadImages_Click(object sender, RoutedEventArgs e)
        {
            InitializeOpenFileDialog();
        }

        private void InitializeOpenFileDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? success = fileDialog.ShowDialog();

            fileDialog.Multiselect = true;
            fileDialog.Title = "My Image Browser";

            if (success == true)
            {
                string path = fileDialog.FileName;
                ImagesTextBox.Text = path;

            }
            else
            {
                //didnt pick anything
            }

        }
    }
}