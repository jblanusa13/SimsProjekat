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

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for CreateTourView.xaml
    /// </summary>
    public partial class CreateTourView : Page,INotifyPropertyChanged, IDataErrorInfo
    {
        private CreateTourViewModel createTourViewModel;
        public List<string> OtherKeyPoints { get; set; }
        public List<string> Appointments { get; set; }
        public Guide Guide { get; set; }
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
        private string _date;
        public string Date
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
        private string _hour;
        public string Hour
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
        private string _minute;
        public string Minute
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
        private Regex _maxNumberGuestsRegex = new Regex("^[1-9][0-9]*$");
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
                    if (string.IsNullOrEmpty(MaxNumberGuests))
                        return "Unesite broj mesta!";
                    Match match = _maxNumberGuestsRegex.Match(MaxNumberGuests);
                    if (!match.Success)
                        return "Los format!";
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
        public bool CreatedTourByLanguage { get; set; }
        public bool CreatedTourByLocation { get; set; }
        public CreateTourView(Guide guide, TourRequest tourRequest,string language,string location)
        {
            InitializeComponent();
            DataContext = this;
            createTourViewModel = new CreateTourViewModel(guide, tourRequest);
            Guide = guide;
            OtherKeyPoints = new List<string>();
            Appointments = new List<string>();
            Images = new List<string>();
            AddKeyPointButton.IsEnabled = false;
            AddAppointmentButton.IsEnabled = false;
            TourDatePicker.BlackoutDates.AddDatesInPast();
            CreatedTourByLanguage = false;
            CreatedTourByLocation = false;
            if (tourRequest != null)
            {
               SetRequestData(tourRequest);
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
        public void SetRequestData(TourRequest tourRequest)
        {
            SetLocation(tourRequest.Location);
            SetLanguage(tourRequest.Language);
            MaxNumberGuests = tourRequest.MaxNumberGuests.ToString();
            MaxNumberGuestsTextBox.IsReadOnly = true;
            TourDatePicker.DisplayDateStart = new DateTime(tourRequest.DateRangeStart.Year, tourRequest.DateRangeStart.Month, tourRequest.DateRangeStart.Day);
            TourDatePicker.DisplayDateEnd = new DateTime(tourRequest.DateRangeEnd.Year, tourRequest.DateRangeEnd.Month, tourRequest.DateRangeEnd.Day);
        }
        public void SetLocation(string location)
        {
            City = location.Split(',')[0];
            CityTextBox.IsReadOnly = true;
            Country = location.Split(',')[1];
            CountryTextBox.IsReadOnly = true;
        }
        public void SetLanguage(string language)
        {
            LanguageComboBox.Items.Clear();
            LanguageComboBox.Items.Add(language);
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
            Images.Add(GetRelativePath(apsolutePath));
        }
        private string GetRelativePath(string apsolutePath)
        {
            string[] helpString = apsolutePath.Split('\\');
            string image = helpString.Last();
            return "/Resources/Images/Guide/" + image;
        }
        public void OtherKeyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OtherKeyPointTextBox.Text))
                AddKeyPointButton.IsEnabled = true;
            else
                AddKeyPointButton.IsEnabled = false;
        }
        public void DurationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OtherKeyPointTextBox.Text))
                AddKeyPointButton.IsEnabled = true;
            else
                AddKeyPointButton.IsEnabled = false;
        }
        public bool IsCorrectAppointment()
        {
            int hour;
            int minute;
            double duration;
            DateTime date = TourDatePicker.SelectedDate.GetValueOrDefault();
            if (TourDatePicker.SelectedDate == null || string.IsNullOrEmpty(Hour) || string.IsNullOrEmpty(Minute) || string.IsNullOrEmpty(Duration))
                MessageTextBox.Text = "Odaberite termin!";
            else if (!Int32.TryParse(Hour, out hour) || hour < 0 || hour >= 24 || !Int32.TryParse(Minute, out minute) || minute < 0 || minute > 60 || !Double.TryParse(Duration, out duration))
                MessageTextBox.Text = "Los format!";
            else if (!createTourViewModel.GuideIsAvailable(date, hour, minute, duration))
                MessageTextBox.Text = "Termin je zauzet!";
            else
                MessageTextBox.Text = "";
            return string.IsNullOrEmpty(MessageTextBox.Text);
        }
        public void AppointmentInput_Changed(object sender, EventArgs e)
        {
            AddAppointmentButton.IsEnabled = IsCorrectAppointment();
        }
        public void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            Appointments.Add(TourDatePicker.SelectedDate.GetValueOrDefault().ToString("MM/dd/yyyy") + " " + Hour + ":" + Minute + "-" + Duration);
            TourDatePicker.SelectedDate = null;
            HourTextBox.Text = "";
            MinuteTextBox.Text = "";
            DurationTextBox.Text = "";
            AddAppointmentButton.IsEnabled = false;
        }
        public void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            OtherKeyPoints.Add(OtherKeyPointTextBox.Text);
            OtherKeyPointTextBox.Text = "";
            AddKeyPointButton.IsEnabled = false; 
        }
        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
           if (IsValid)
           {
                createTourViewModel.CreateTour(TourName,TourLanguage,City + "," + Country,MaxNumberGuests, Appointments,StartKeyPoint,OtherKeyPoints,FinishKeyPoint,Description,Images,CreatedTourByLocation,CreatedTourByLanguage);
                this.NavigationService.Navigate(new ScheduledToursView(Guide));
            }
           else
               MessageBox.Show("Nisu validno popunjena polja!");
        }
    }
}
