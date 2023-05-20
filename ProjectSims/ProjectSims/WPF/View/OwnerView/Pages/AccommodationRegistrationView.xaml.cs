using System;
using ProjectSims.Domain.Model;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ProjectSims.FileHandler;
using Microsoft.Win32;
using System.Security;
using System.Collections.ObjectModel;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
using ProjectSims.WPF.View.OwnerView;
using System.IO;
using System.Windows.Media;

namespace ProjectSims.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Page, INotifyPropertyChanged
    {
        public Owner Owner { get; set; }
        public Image SelectedImage { get; set; }
        public AccommodationRegistrationViewModel accommodationRegistrationViewModel { get; set; }

        public List<string> relativePaths = new List<string>();

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

        public TextBlock TitleTextBlock { get; set; }
        
        public AccommodationRegistrationView(Owner o, TextBlock titleTextBlock, Accommodation selectedAccommodation)
        {
            InitializeComponent();
            Owner = o;
            TitleTextBlock = titleTextBlock;
            accommodationRegistrationViewModel = new AccommodationRegistrationViewModel(Owner);
            this.DataContext = accommodationRegistrationViewModel;
            if (selectedAccommodation != null)
            {
                LocationTextBox.Text = selectedAccommodation.Location.ToString();
                LocationTextBox.IsEnabled = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            List<string> Pics = new List<string>();
            foreach (string path in relativePaths)
            {
                Pics.Add(path);
            }
            if (!string.IsNullOrEmpty(AccommodationNameTextBox.Text) && !string.IsNullOrEmpty(LocationTextBox.Text) && !string.IsNullOrEmpty(GuestsMaximumTextBox.Text) && !string.IsNullOrEmpty(MinimumReservationDaysTextBox.Text) && !string.IsNullOrEmpty(DismissalDaysTextBox.Text))
            {
                accommodationRegistrationViewModel.RegisterAccommodation(LocationTextBox.Text, Pics, AccommodationNameTextBox.Text, Type, Convert.ToInt32(GuestsMaximumTextBox.Text), Convert.ToInt32(MinimumReservationDaysTextBox.Text), Convert.ToInt32(DismissalDaysTextBox.Text));
            }
            this.NavigationService.Navigate(new AccommodationsDisplayView(Owner, TitleTextBlock));
            TitleTextBlock.Text = "Smještaji";
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
                ImageList.Items.Add(Images);
        }

        private void AddRelativePath(string fileName)
        {
            relativePaths.Add(GetRelativePath(fileName));   
        }
        
        private string GetRelativePath(string fileName)
        {
            return "/Resources/Images/Owner/Accommodations/" + fileName;
        }

        private void Dismiss_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AccommodationsDisplayView(Owner, TitleTextBlock));
            TitleTextBlock.Text = "Smještaji";
        }

        private void DeleteImage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            SelectedImage = (Image)ImageList.SelectedItem;
            if (SelectedImage != null && e.Key.Equals(Key.Delete))
            {
                BitmapImage image = (BitmapImage)SelectedImage.Source;
                ImageList.Items.Remove(SelectedImage);
                relativePaths.Remove(image.UriSource.ToString());
            }
        }
    }
}