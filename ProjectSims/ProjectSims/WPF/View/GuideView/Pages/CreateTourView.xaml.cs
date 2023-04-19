using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.View.GuideView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for CreateTourView.xaml
    /// </summary>
    public partial class CreateTourView : Page,INotifyPropertyChanged, IDataErrorInfo
    {
        public Guide guide { get; set; }

        private readonly TourService _controller;

        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
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

        private string _tourLanguage;
        public string TourLanguage
        {
            get => _tourLanguage;
            set
            {
                if (value != _tourLanguage)
                {
                    _tourLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxNumberGuests;
        public string MaxNumberGuests
        {
            get => _maxNumberGuests;
            set
            {
                if (value != _maxNumberGuests)
                {
                    _maxNumberGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _duration;
        public string Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tourStarts;
        public string TourStarts
        {
            get => _tourStarts;
            set
            {
                if (value != _tourStarts)
                {
                    _tourStarts = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _startKeyPoint;
        public string StartKeyPoint
        {
            get => _startKeyPoint;
            set
            {
                if (value != _startKeyPoint)
                {
                    _startKeyPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _finishKeyPoint;
        public string FinishKeyPoint
        {
            get => _finishKeyPoint;
            set
            {
                if (value != _finishKeyPoint)
                {
                    _finishKeyPoint = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _otherKeyPoint;
        public string OtherKeyPoint
        {
            get => _otherKeyPoint;
            set
            {
                if (value != _otherKeyPoint)
                {
                    _otherKeyPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
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

        private List<string> _otherKeyPoints;
        public List<string> OtherKeyPoints
        {
            get => _otherKeyPoints;
            set
            {
                if (value != _otherKeyPoints)
                {
                    _otherKeyPoints = value;
                    OnPropertyChanged();
                }
            }
        }

        private Regex _durationRegex = new Regex("^([0-9]*\\.)?[0-9]+$");
        private Regex _maxNumberGuestsRegex = new Regex("^[1-9][0-9]*$");
        public String Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "TourName")
                {
                    if (string.IsNullOrEmpty(TourName))
                        return "Unesite naziv ture!";
                }
                else if (columnName == "Location")
                {
                    if (string.IsNullOrEmpty(Location))
                        return "Unesite lokaciju!";
                }
                else if (columnName == "TourLanguage")
                {
                    if (string.IsNullOrEmpty(TourLanguage))
                        return "Odaberite jezik!";
                }
                else if (columnName == "MaxNumberGuests")
                {
                    if (string.IsNullOrEmpty(MaxNumberGuests))
                        return "Unesite broj mesta!";
                    Match match = _maxNumberGuestsRegex.Match(MaxNumberGuests);
                    if (!match.Success)
                        return "Format nije ispravan!";
                }
                else if (columnName == "Duration")
                {
                    if (string.IsNullOrEmpty(Duration))
                        return "Unesite trajanje ture!";
                    Match match = _durationRegex.Match(Duration);
                    if (!match.Success)
                        return "Format nije ispravan!";
                }
                else if (columnName == "TourStarts")
                {
                    if (string.IsNullOrEmpty(TourStarts))
                        return "Unesite datum i vreme početka ture!";
                    DateTime result;
                    foreach (string tourStart in TourStarts.Split(','))
                    {
                        if (!DateTime.TryParse(tourStart, out result))
                            return "Format nije ispravan!";
                    }
                }
                else if (columnName == "StartKeyPoint")
                {
                    if (string.IsNullOrEmpty(StartKeyPoint))
                        return "Unesite početnu stanicu!";
                }
                else if (columnName == "FinishKeyPoint")
                {
                    if (string.IsNullOrEmpty(FinishKeyPoint))
                        return "Unesite krajnju stanicu!";
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Unesite opis!";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "TourName", "Location", "TourLanguage", "MaxNumberGuests", "Duration", "TourStarts", "StartKeyPoint", "FinishKeyPoint", "Description"};
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
        public CreateTourView(Guide g)
        {
            InitializeComponent();
            DataContext = this;
            _controller = new TourService();
            OtherKeyPoints = new List<string>();
            guide = g;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDlg.ShowDialog();

            string apsolutePath = "";
            if (result == true)
            {
                apsolutePath = openFileDlg.FileName;
            }
            else
            {
                return;
            }
            Images += GetRelativePath(apsolutePath) + ",";
        }
        private string GetRelativePath(string apsolutePath)
        {
            string nameFile = apsolutePath.Remove(0, 90);
            return "../../../Resources/Images/Guide/" + nameFile;
        }
        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                foreach (string TourStart in TourStarts.Split(','))
                {
                    Images.Remove(Images.Length - 1, 1);
                    _controller.Create(guide.Id, TourName, Location, Description, TourLanguage, MaxNumberGuests, StartKeyPoint, FinishKeyPoint, OtherKeyPoints, TourStart, Duration, Images);
                }
                this.NavigationService.Navigate(new ScheduledToursView(guide));
            }
            else
                MessageBox.Show("Nisu validno popunjena polja!");

        }

        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            if (OtherKeyPoint != "")
            {
                OtherKeyPoints.Add(OtherKeyPoint);
                OtherKeyPointTextBox.Text = "";
            }
            else
                MessageBox.Show("Unesite stanicu!");
        }
    }
}
