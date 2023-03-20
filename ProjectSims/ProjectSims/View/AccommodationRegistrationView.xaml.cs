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

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window, INotifyPropertyChanged, IDataErrorInfo
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
                        && !string.IsNullOrEmpty(DismissalDaysTextBox.Text))
            {
                Accommodation accommodation = new Accommodation(-1, AccommodationName, Location, Type, GuestsMaximum, MinimumReservationDays, DismissalDays, Images, null, -1);
                accommodationController.Create(accommodation);
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
                    else if (Location.Contains(" "))
                    {
                        LocationTextBox.Text = LocationTextBox.Text.Trim();
                        LocationTextBox.UpdateLayout();
                        return "Iskljucivo bez razmaka!";
                    }
                    else if (Location.Contains(",")) 
                    {
                        int count = 0;
                        foreach (char character in LocationTextBox.Text)
                        {
                            if (character == ',') 
                            {
                                count++;
                                LocationTextBox.UpdateLayout();
                            }

                            if (count>1) 
                            {
                                LocationTextBox.UpdateLayout();
                                return "Iskljucivo jedna zapeta!";
                            }
                        }
                    }
                    else if (!Regex.IsMatch(Location, @"^[a-zA-Z,]*$"))
                    {
                        return "Iskljucivo: slova[a-z] u formatu Grad,Drzava!";
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
                    else if (GuestsMaximum.ToString().Contains(" "))
                    {
                        GuestsMaximumTextBox.Text = GuestsMaximumTextBox.Text.Trim();
                        GuestsMaximumTextBox.UpdateLayout();
                        return "Iskljucivo bez razmaka!";
                    }
                }

                else if (columnName == "MinimumReservationDays")
                {
                    if (string.IsNullOrEmpty(Convert.ToString(MinimumReservationDays)))
                    {
                        return "Unesite vrijednost!";
                    }
                    else if (MinimumReservationDays.ToString().Contains(" "))
                    {
                        MinimumReservationDaysTextBox.Text = MinimumReservationDaysTextBox.Text.Trim();
                        MinimumReservationDaysTextBox.UpdateLayout();
                        return "Iskljucivo bez razmaka!";
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
                    else if (DismissalDays.ToString().Contains(" "))
                    {
                        DismissalDaysTextBox.Text = DismissalDaysTextBox.Text.Trim();
                        DismissalDaysTextBox.UpdateLayout();
                        return "Iskljucivo bez razmaka!";
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
                    if (string.IsNullOrEmpty(Images))
                    {
                        return "Unesite vrijednost!";
                    }
                    else if(Images.Contains(" ")) 
                    {
                        ImagesTextBox.Text = ImagesTextBox.Text.Trim();
                        ImagesTextBox.UpdateLayout();
                        return "Iskljucivo bez razmaka!";
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

        private void MinimumReservationDaysTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidMinimumReservationDaysTextBox(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValidMinimumReservationDaysTextBox(string str) 
        {
            int temp;
            if(!(int.TryParse(str, out temp) && temp >= 1))
                MessageBox.Show("Iskljucivo brojevi veci od 0!", "Minimum dana za rezervaciju", MessageBoxButton.OK, MessageBoxImage.Error);
            return int.TryParse(str, out temp) && temp>=1;
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
        
        private void ImagesTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = !IsValidImagesTextBox(((TextBox)sender).Text + e.Text);
        }/*
        public static bool IsValidImagesTextBox(string str)
        {
            if (str.Contains(" "))  
            {
                return false;
            }
            return true;
            /* if(!Regex.IsMatch(str, @"^[a-zA-Z0-9,.!]*$"))
                 MessageBox.Show("Iskljucivo bez razmak!", "Slike", MessageBoxButton.OK, MessageBoxImage.Error);
             return Regex.IsMatch(str, @"^[a-zA-Z0-9,.!]*$");
        }*/

        private void AccommodationNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = !IsValidDismissalDaysTextBox(((TextBox)sender).Text + e.Text);
        }

        private void LocationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           // e.Handled = !IsValidDismissalDaysTextBox(((TextBox)sender).Text + e.Text);
        }
    }
}