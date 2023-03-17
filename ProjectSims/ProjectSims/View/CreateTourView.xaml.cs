using ProjectSims.Controller;
using ProjectSims.FileHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using ProjectSims.Model;
using ProjectSims.Observer;
using System.Media;
using System.Text.RegularExpressions;

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTourView : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly TourController _controller;

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

        private string _tourStart;
        public string TourStart
        {
            get => _tourStart;
            set
            {
                if (value != _tourStart)
                {
                    _tourStart = value;
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
                    Match match =_durationRegex.Match(Duration);
                    if (!match.Success)
                        return "Format nije ispravan!";
                }
                else if (columnName == "TourStart")
                {
                    if (string.IsNullOrEmpty(TourStart))
                        return "Unesite datum i vreme početka ture!";
                    DateTime result;
                    foreach(string dateTime in TourStart.Split(',')){
                        if (!DateTime.TryParse(dateTime, out result))
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
             return null;

            }
        }

        private readonly string[] _validatedProperties = { "TourName", "Location", "TourLanguage", "MaxNumberGuests", "Duration", "TourStart", "StartKeyPoint", "FinishKeyPoint"};
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
        public CreateTourView()
        {
            InitializeComponent();
            DataContext = this;
            _controller = new TourController();
            OtherKeyPoints = new List<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateTourClick(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                _controller.Create(TourName, Location, Description, TourLanguage, MaxNumberGuests, StartKeyPoint, FinishKeyPoint, OtherKeyPoints, TourStart, Duration, Images);
                Close();
            }
            else
                MessageBox.Show("Nisu validno popunjena polja!");
            
        }

        private void AddKeyPoint(object sender, RoutedEventArgs e)
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
