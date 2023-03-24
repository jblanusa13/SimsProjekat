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
using System.IO;
using Microsoft.Win32;

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
        private List<ListBoxItem> SelectedImages { get; set; }
        public class Img
        {
            public String ImagePath { get; set; }
            public String Title { get; set; }

            public static explicit operator Img(ListBoxItem v)
            {
                throw new NotImplementedException();
            }
        }

        public AccommodationRegistrationView()
        {
            InitializeComponent();
            DataContext = this;

            accommodationController = new AccommodationController();
            ownerController = new OwnerController();
            accommodations = new ObservableCollection<Accommodation>();
            SelectedImages = new List<ListBoxItem>();
            /*List<string> lista = new List<string>();
            lista.Add("Images/filipovic1.jpg");
            lista.Add("Images/filipovic2.jpg");
            lista.Add("Images/jovanovic1.jpg");
            lista.Add("Images/jovanovic2.jpg");
            lista.Add("Images/draganovi_konaci1.jpg");
            lista.Add("Images/visnjin_dom1.jpg");
            lista.Add("Images/visnjin_dom2.jpg");
            ListBox.ItemsSource = lista;
            */
            ListBox.Items.Add(new Img() { ImagePath = "/Images/jovanovic1.jpg", Title = "Jovanovic" });
            ListBox.Items.Add(new Img() { ImagePath = "/Images/jovanovic2.jpg", Title = "Jovanovic" });
            ListBox.Items.Add(new Img() { ImagePath = "/Images/draganovi_konaci1.jpg", Title = "Draganovi konaci" });
            ListBox.Items.Add(new Img() { ImagePath = "/Images/filipovic1.jpg", Title = "Filipovic" });
            ListBox.Items.Add(new Img() { ImagePath = "/Images/filipovic2.jpg", Title = "Filipovic" });
            ListBox.Items.Add(new Img() { ImagePath = "/Images/kod_labuda1.jpg", Title = "Kod labuda" });
            ListBox.Items.Add(new Img() { ImagePath = "/Images/kod_labuda.jpg", Title = "Kod labuda" });
            ListBox.Items.Add(new Img() { ImagePath = "/Images/mladenovic.jpg", Title = "Mladenovic" }); 
            ListBox.Items.Add(new Img() { ImagePath = "/Images/mladenovic2.jpg", Title = "Mladenovic" });
            ListBox.Items.Add(new Img() { ImagePath = "/Images/visnjin_dom1.jpg", Title = "Visnjin dom" });
            ListBox.Items.Add(new Img() { ImagePath = "/Images/visnjin_dom2.jpg", Title = "Visnjin dom" });
        
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
        
        private void AccommodationNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = !IsValidDismissalDaysTextBox(((TextBox)sender).Text + e.Text);
        }

        private void LocationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           // e.Handled = !IsValidDismissalDaysTextBox(((TextBox)sender).Text + e.Text);
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedImages != null)
            {
                string[] paths = null;
                string[] titles = null;

                foreach(ListBoxItem image in SelectedImages)
                {
                    int count = SelectedImages.Count;
                    Img newImage = (Img)image;

                }
            }
            else 
            {
                MessageBox.Show("Izaberite sliku!", "Slike", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}