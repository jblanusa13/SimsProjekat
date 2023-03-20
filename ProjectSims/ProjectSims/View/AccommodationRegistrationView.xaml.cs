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
using System.Collections.ObjectModel;

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly AccommodationController accommodationController;
        private readonly OwnerController ownerController;
        public ObservableCollection<Accommodation> accommodations;
        public AccommodationRegistrationView()
        {
            InitializeComponent();
            DataContext = this;

            accommodationController = new AccommodationController();
            ownerController = new OwnerController();
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

        private int _dismissalDays;
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
        private string _address;
        public string Address
        {
            get => _address;

            set
            {
                if (value != _address)
                {
                    _address = value;
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
                        && !string.IsNullOrEmpty(DismissalDaysTextBox.Text))
            {
                Accommodation accommodation = new Accommodation(-1, AccommodationName, Location, Type, GuestsMaximum, MinimumReservationDays, DismissalDays, Images, null, -1);
                accommodationController.Create(accommodation);
                //accommodations.Add(accommodation);
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
                    if (string.IsNullOrEmpty(Location))
                    {
                        return "Unesite vrijednost!";
                    }
                    else if (!Regex.IsMatch(Location, @"^[a-zA-Z0-9,. ]*$"))
                    {
                        return "Iskljucivo: slova[a-z] i brojevi[0-9]!";
                    }
                }
                else if (columnName == "GuestsMaximum")
                {
                    if (string.IsNullOrEmpty(Convert.ToString(GuestsMaximum)))
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
                    if (string.IsNullOrEmpty(Convert.ToString(MinimumReservationDays)))
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
                    if (string.IsNullOrEmpty(Convert.ToString(DismissalDays)))
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

        private void MinimumReservationDaysTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidMinimumReservationDaysTextBox(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValidMinimumReservationDaysTextBox(string str)
        {
            int temp;
            if (!(int.TryParse(str, out temp) && temp >= 1))
                MessageBox.Show("Iskljucivo brojevi veci od 0!", "Minimum dana za rezervaciju", MessageBoxButton.OK, MessageBoxImage.Error);
            return int.TryParse(str, out temp) && temp >= 1;
        }

        private void GuestsMaximumTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidGuestsMaximumTextBox(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValidGuestsMaximumTextBox(string str)
        {
            int temp;
            if (!(int.TryParse(str, out temp) && temp >= 1))
                MessageBox.Show("Iskljucivo brojevi veci od 0!", "Maksimalan broj gostiju", MessageBoxButton.OK, MessageBoxImage.Error);
            return int.TryParse(str, out temp) && temp >= 1;
        }
        private void DismissalDaysTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidDismissalDaysTextBox(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValidDismissalDaysTextBox(string str)
        {
            int temp;
            if (!(int.TryParse(str, out temp) && temp >= 0))
                MessageBox.Show("Iskljucivo nenegativni brojevi!", "Dani za otkaz", MessageBoxButton.OK, MessageBoxImage.Error);
            return int.TryParse(str, out temp) && temp >= 0;
        }
    }
}