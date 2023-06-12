using Microsoft.Win32;
using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Repository;
using ProjectSims.Service;
using ProjectSims.View.OwnerView.Pages;
using ProjectSims.WPF.View.OwnerView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class AccommodationRegistrationViewModel : Page, INotifyPropertyChanged, IDataErrorInfo
    {
        private AccommodationService accommodationService;
        private LocationService locationService;
        private OwnerService ownerService;
        private NavigationService NavService { get; set; }
        public AccommodationRegistrationView View { get; set; }
        public OwnerStartingView Window { get; set; }
        public RelayCommand DismissCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }
        public RelayCommand LoadImagesCommand { get; set; }
        public Owner Owner { get; set; }
        public Image SelectedImage { get; set; }
        public List<string> relativePaths { get; set; }

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

        private string _guestsMaximum;
        public string GuestsMaximum
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

        private string _minimumReservationDays;
        public string MinimumReservationDays
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

        private string _dismissalDays = "1";
        public string DismissalDays
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
        public AccommodationRegistrationViewModel(Owner o, OwnerStartingView window, Accommodation selectedAccommodation, NavigationService navService, AccommodationRegistrationView view)
        {
            accommodationService = new AccommodationService();
            locationService = new LocationService();
            ownerService = new OwnerService();
            Owner = o;
            Window = window;
            NavService = navService;
            View = view;
            relativePaths = new List<string>();
            DismissCommand = new RelayCommand(Execute_DismissCommand);
            LoadImagesCommand = new RelayCommand(Execute_LoadImagesCommand);
            RegisterCommand = new RelayCommand(Execute_RegisterCommand, CanExecute_RegisterCommand);
            if (selectedAccommodation != null)
            {
                Location = selectedAccommodation.Location.City + "," + selectedAccommodation.Location.Country;
                View.LocationTextBox.IsEnabled = false;
            }
        }

        private void Execute_RegisterCommand(object obj)
        {
            List<string> Pics = new List<string>();
            foreach (string path in relativePaths)
            {
                Pics.Add(path);
            }
            RegisterAccommodation(Location, Pics, AccommodationName, Type, Convert.ToInt32(GuestsMaximum), Convert.ToInt32(MinimumReservationDays), Convert.ToInt32(DismissalDays));
            NavService.Navigate(new AccommodationsDisplayView(Owner, NavService, Window));
            Window.PageTitle = "Smještaji";
        }

        private bool CanExecute_RegisterCommand(object obj)
        {
            return IsValid;
        }

        private void Execute_LoadImagesCommand(object obj)
        {
            InitializeOpenFileDialog();
        }

        private void Execute_DismissCommand(object obj)
        {
            NavService.Navigate(new AccommodationsDisplayView(Owner, NavService, Window));
            Window.PageTitle = "Smještaji";
        }

        public void RegisterAccommodation(string Location, List<string> Pics, string AccommodationName, AccommodationType Type, int GuestsMaximum, int MinimumReservationDays, int DismissalDays)
        {
            locationService.Add(Location);
            int IdLocation = locationService.GetIdByLocation(Location);
            Location location = new Location(IdLocation, Location.ToString().Split(",")[0], Location.ToString().Split(",")[1]);
            AccommodationSchedule schedule = new AccommodationSchedule(-1, new List<DateRanges>());
            Accommodation accommodation = new Accommodation(-1, AccommodationName, IdLocation, location, Type, GuestsMaximum, MinimumReservationDays, DismissalDays, Pics, Owner.Id, -1, schedule, false);
            accommodationService.Create(accommodation);
            ownerService.AddAccommodation(Owner, accommodation.Id);
            ownerService.Update(Owner);
        }

        private void InitializeOpenFileDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                foreach (var fileName in fileDialog.SafeFileNames)
                {
                    AddRelativePath(fileName);
                    string path = GetRelativePath(fileName);
                    InitializeImages(path);
                }
            }
            else
            {
                return;
            }
        }

        private void InitializeImages(string filename)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filename, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            Images = new Image();
            Images.Source = bitmap;
            Images.Stretch = Stretch.Fill;
            View.ImageList.Items.Add(Images);
        }

        private void AddRelativePath(string fileName)
        {
            relativePaths.Add(GetRelativePath(fileName));
        }

        private string GetRelativePath(string fileName)
        {
            return "/Resources/Images/Owner/Accommodations/" + fileName;
        }

        public void DeleteImage(Image selectedImage) {
            BitmapImage image = (BitmapImage)selectedImage.Source;
            View.ImageList.Items.Remove(selectedImage);
            relativePaths.Remove(image.UriSource.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public String Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "AccommodationName")  
                {
                    if (string.IsNullOrEmpty(AccommodationName))
                        return "Unesite naziv!";
                }
                else if (columnName == "Location")
                {
                    if (string.IsNullOrEmpty(Location))
                        return "Unesite naziv!";

                    if (!Regex.IsMatch(Location, "^[^,]+,[^,]+"))
                        return "Format Grad,Drzava!";
                }
                else if (columnName == "Type")
                {
                    if (string.IsNullOrEmpty(Type.ToString()))
                        return "Unesite tip!";
                }
                else if (columnName == "GuestsMaximum")
                {
                    int maxGuests;
                    if (!int.TryParse(GuestsMaximum, out maxGuests))
                        return "Unesite cio broj!";

                    if (maxGuests <= 0)
                        return "Unesite broj veći od 0!";
                }
                else if (columnName == "MinimumReservationDays")
                {
                    int daysNumber;
                    if (!int.TryParse(MinimumReservationDays, out daysNumber))
                        return "Unesite cio broj!";

                    if (daysNumber <= 0)
                        return "Unesite broj veći od 0!";
                }
                else if (columnName == "DismissalDays")
                {
                    if (string.IsNullOrEmpty(DismissalDays))
                        return "Unesite broj dana za otkaz!";

                    int daysNumber;
                    if (!int.TryParse(DismissalDays, out daysNumber))
                        return "Unesite cio broj!";

                    if (daysNumber <= 0)
                        return "Unesite broj veći od 0!";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "AccommodationName", "Location", "Type", "GuestsMaximum", "MinimumReservationDays", "DismissalDays" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
