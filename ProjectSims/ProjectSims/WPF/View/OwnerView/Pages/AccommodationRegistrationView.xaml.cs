using System;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.Repository;
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
using ProjectSims.FileHandler;
using Microsoft.Win32;
using System.Security;
using System.Collections.ObjectModel;
using ProjectSims.Repository;
using System.IO;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly AccommodationService accommodationService;
        private AccommodationRepository accommodationRepository;
        private readonly OwnerService ownerService;
        private readonly OwnerRepository ownerRepository;
        private readonly LocationRepository locationRepository;
        public ObservableCollection<Accommodation> accommodations;
        public AccommodationRegistrationView()
        {
            InitializeComponent();
            DataContext = this;

            accommodationService = new AccommodationService();
            ownerService = new OwnerService();
            accommodationRepository = new AccommodationRepository();
            ownerRepository = new OwnerRepository();
            locationRepository = new LocationRepository();
            accommodations = new ObservableCollection<Accommodation>();
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

        private Image _images;
        public Image Images
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
        public List<string> paths = new List<string>();

        private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid && !string.IsNullOrEmpty(AccommodationNameTextBox.Text)
                        && !string.IsNullOrEmpty(LocationTextBox.Text)
                        && !string.IsNullOrEmpty(GuestsMaximumTextBox.Text)
                        && !string.IsNullOrEmpty(MinimumReservationDaysTextBox.Text)
                        && !string.IsNullOrEmpty(DismissalDaysTextBox.Text))
            {
                locationRepository.Add(LocationTextBox.Text);
                int IdLocation = locationRepository.GetLocationId(Location);
                List<string> Pics = new List<string>();
                foreach (string path in paths)
                {
                    Pics.Add(path);
                }
                int idCurrentOwner = MainWindow.CurrentUserId;
                Location location = new Location(IdLocation, Location.ToString().Split(",")[0], Location.ToString().Split(",")[1]);
                Accommodation accommodation = new Accommodation(-1, AccommodationName, IdLocation, location, Type, GuestsMaximum, MinimumReservationDays, DismissalDays, Pics, idCurrentOwner);
                accommodationService.Create(accommodation);
                Owner owner = ownerRepository.GetById(idCurrentOwner);
                ownerRepository.AddAccommodationId(owner, accommodation.Id);
                ownerService.Update(ownerRepository.GetById(idCurrentOwner));
                this.Close();
            }
        }

        private void LoadImages_Click(object sender, RoutedEventArgs e)
        {
            InitializeOpenFileDialog();
        }

        private void InitializeOpenFileDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                foreach (var filename in fileDialog.FileNames)
                {
                    paths.Add(filename);
                }
                foreach (var path in paths)
                {
                    AddImagesToImageList(path);
                }
            }
            else
            {
                //Didn't pick anything
            }
        }

        private void AddImagesToImageList(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.EndInit();
            Images = new Image();
            Images.Source = bitmap;
            Images.Width = 170;
            Images.Height = 100;
            ImageList.Items.Add(Images);
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
                return null;
            }
        }

        private readonly string[] validatedProperties = { "AccommodationName", "Location", "GuestsMaximum", "MinimumReservationDays", "DismissalDays" };

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