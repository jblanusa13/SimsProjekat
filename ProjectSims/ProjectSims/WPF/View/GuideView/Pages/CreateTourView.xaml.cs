using ceTe.DynamicPDF.PageElements;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.View.GuideView;
using ProjectSims.WPF.ViewModel.GuideViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Globalization;
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
using static System.Net.Mime.MediaTypeNames;
using ProjectSims.Commands;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for CreateTourView.xaml
    /// </summary>
    public partial class CreateTourView : Page,INotifyPropertyChanged, IDataErrorInfo
    {
        private CreateTourViewModel createTourViewModel;
        public RelayCommand AddAppointmentCommand { get; set; }
        public RelayCommand AddKeyPointCommand { get; set; }
        public RelayCommand CreateTourCommand { get; set; }
        public List<string> OtherKeyPoints { get; set; }
        public List<Tuple<DateTime,int>> Appointments { get; set; }
        public Guide Guide { get; set; }
        public bool CreatedTourByLanguage { get; set; }
        public bool CreatedTourByLocation { get; set; }
        public List<string> Images { get; set; }

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
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _hour;
        public int Hour
        {
            get => _hour;
            set
            {
                if (value != _hour)
                {
                    _hour = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _minute;
        public int Minute
        {
            get => _minute;
            set
            {
                if (value != _minute)
                {
                    _minute = value;
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
        private int _duration;
        public int Duration
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
        private int _maxNumberGuests;
        public int MaxNumberGuests
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
        private bool _numberGuestsEnabled;
        public bool NumberGuestsEnabled
        {
            get => _numberGuestsEnabled;
            set
            {
                if (value != _numberGuestsEnabled)
                {
                    _numberGuestsEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _appointmentEnabled;
        public bool AppointmentEnabled
        {
            get => _appointmentEnabled;
            set
            {
                if (value != _appointmentEnabled)
                {
                    _appointmentEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _locationEnabled;
        public bool LocationEnabled
        {
            get => _locationEnabled;
            set
            {
                if (value != _locationEnabled)
                {
                    _locationEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _languageEnabled;
        public bool LanguageEnabled
        {
            get => _languageEnabled;
            set
            {
                if (value != _languageEnabled)
                {
                    _languageEnabled = value;
                    OnPropertyChanged();
                }
            }
        }


        public String Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "TourName")
                {
                    if (string.IsNullOrEmpty(TourName))
                        return "Ovo je obavezno polje!";
                }
                else if (columnName == "TourLanguage")
                {
                    if (string.IsNullOrEmpty(TourLanguage))
                        return "Ovo je obavezno polje!";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "Ovo je obavezno polje!";
                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Ovo je obavezno polje!";
                }
                else if (columnName == "MaxNumberGuests")
                {
                    if (MaxNumberGuests == 0)
                        return "Unesite broj mesta!";
                }
                else if (columnName == "StartKeyPoint")
                {
                    if (string.IsNullOrEmpty(StartKeyPoint))
                        return "Ovo je obavezno polje!";
                }
                else if (columnName == "FinishKeyPoint")
                {
                    if (string.IsNullOrEmpty(FinishKeyPoint))
                        return "Ovo je obavezno polje!";
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Ovo je obavezno polje!";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "TourName", "TourLanguage", "City", "Country", "MaxNumberGuests", "Duration", "StartKeyPoint", "FinishKeyPoint", "Description"};
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
        public CreateTourView(Guide guide, TourRequest tourRequest,string language,string location,DateTime selectedDate,int duration)
        {
            InitializeComponent();
            AddAppointmentCommand = new RelayCommand(Execute_AddAppointmentCommand, CanExecute_AddAppointmentCommand);
            AddKeyPointCommand = new RelayCommand(Execute_AddKeyPointCommand, CanExecute_AddKeyPointCommand);
            CreateTourCommand = new RelayCommand(Execute_CreateTourCommand, CanExecute_CreateTourCommand);
            DataContext = this;
            createTourViewModel = new CreateTourViewModel(guide, tourRequest);
            Guide = guide;
            OtherKeyPoints = new List<string>();
            Appointments = new List<Tuple<DateTime,int>>();
            Images = new List<string>();
            TourDatePicker.BlackoutDates.AddDatesInPast();
            Date = DateTime.Now;
            CreatedTourByLanguage = false;
            CreatedTourByLocation = false;
            LanguageEnabled = true;
            LocationEnabled = true;
            AppointmentEnabled = true;
            _numberGuestsEnabled = true;
            if (tourRequest != null)
            {
               SetRequestData(tourRequest,selectedDate,duration);
            }
            if(language != null)
            {
                SetLanguage(language);
                CreatedTourByLanguage = true;
            }
            if(location != null)
            {
                SetLocation(location);
                CreatedTourByLocation = true;
            }
        }
        public void SetRequestData(TourRequest tourRequest,DateTime date,int duration)
        {
            SetLocation(tourRequest.Location);
            SetLanguage(tourRequest.Language);
            MaxNumberGuests = tourRequest.MaxNumberGuests;
            NumberGuestsEnabled = false;
            Date = date.Date;
            Hour = date.Hour;
            Minute = date.Minute;
            Duration = duration;
            AppointmentEnabled = false;
        }
        public void SetLocation(string location)
        {
            City = location.Split(',')[0];
            Country = location.Split(',')[1];
            LocationEnabled = false;
        }
        public void SetLanguage(string language)
        {
            LanguageComboBox.SetValue(UidProperty, language);
            TourLanguage = language;
            LanguageEnabled = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool CanExecute_AddAppointmentCommand(object obj)
        {
            if (!createTourViewModel.GuideIsAvailable(Date, Hour, Minute, Duration))
              MessageTextBox.Text = "Termin je zauzet!";
            else
            MessageTextBox.Text = "";
            return string.IsNullOrEmpty(MessageTextBox.Text);
        }
        private void Execute_AddAppointmentCommand(object obj)
        {
            DateTime date = new DateTime(Date.Year,Date.Month,Date.Day, Hour, Minute, 0);
            Appointments.Add(new Tuple<DateTime, int>(date, Duration));
            Date = DateTime.Now;
            Hour = 0;
            Minute = 0;
            Duration = 0;
        }
        private bool CanExecute_AddKeyPointCommand(object obj)
        {
            if (!string.IsNullOrEmpty(OtherKeyPointTextBox.Text))
                return true;
            else
                return false;
        }
        private void Execute_AddKeyPointCommand(object obj)
        {
            OtherKeyPoints.Add(OtherKeyPointTextBox.Text);
            OtherKeyPointTextBox.Text = "";
        }
        private bool CanExecute_CreateTourCommand(object obj)
        {
                return IsValid;
        }
        private void Execute_CreateTourCommand(object obj)
        {
            createTourViewModel.CreateTour(TourName, TourLanguage, City + "," + Country, MaxNumberGuests.ToString(), Appointments, StartKeyPoint, OtherKeyPoints, FinishKeyPoint, Description, Images, CreatedTourByLocation, CreatedTourByLanguage);
            this.NavigationService.Navigate(new ScheduledToursView(Guide));
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
            Images.Add(GetRelativePath(apsolutePath));
        }
        private string GetRelativePath(string apsolutePath)
        {
            string[] helpString = apsolutePath.Split('\\');
            string image = helpString.Last();
            return "/Resources/Images/Guide/" + image;
        }
    }
}
