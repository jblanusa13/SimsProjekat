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
        public Guide guide { get; set; }

        public List<string> OtherKeyPoints { get; set; }
        public List<DateTime> Appointments { get; set; }
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

        private Regex _durationRegex = new Regex("^([0-9]*\\.)?[0-9]+$");
        private Regex _maxNumberGuestsRegex = new Regex("^[1-9][0-9]*$");
        private Regex _hoursRegex = new Regex("^[01]?[0-9]|2[0-3]$");
        private Regex _minuteRegex = new Regex("^[0-5][0-9]$");
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
                else if (columnName == "TourLanguage")
                {
                    if (string.IsNullOrEmpty(TourLanguage))
                        return "Odaberite jezik!";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "Unesite grad!";
                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Unesite drzavu!";
                }
                else if (columnName == "MaxNumberGuests")
                {
                    if (string.IsNullOrEmpty(MaxNumberGuests))
                        return "Unesite broj mesta!";
                    Match match = _maxNumberGuestsRegex.Match(MaxNumberGuests);
                    if (!match.Success)
                        return "Format nije ispravan!";
                }
                else if (columnName == "Date")
                {
                    if (string.IsNullOrEmpty(Date))
                        return "Odaberite datum!";
                }
                else if (columnName == "Hour")
                {
                    if (string.IsNullOrEmpty(Hour))
                        return "Unesite sat!";
                    Match match = _hoursRegex.Match(MaxNumberGuests);
                    if (!match.Success)
                        return "Format nije ispravan!";
                }
                else if (columnName == "Minute")
                {
                    if (string.IsNullOrEmpty(Minute))
                        return "Unesite minut!";
                    Match match = _minuteRegex.Match(MaxNumberGuests);
                    if (!match.Success)
                        return "Format nije ispravan!";
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
                else if (columnName == "OtherKeyPoint")
                {
                    if (string.IsNullOrEmpty(FinishKeyPoint))
                        return "Unesite krajnju stanicu!";
                }
                else if (columnName == "Duration")
                {
                    if (string.IsNullOrEmpty(Duration))
                        return "Unesite trajanje!";
                    Match match = _durationRegex.Match(Duration);
                    if (!match.Success)
                        return "Format nije ispravan!";
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Unesite opis!";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "TourName", "TourLanguage", "City", "Country", "MaxNumberGuests", "Date", "Hour", "Minute","Duration", "StartKeyPoint", "FinishKeyPoint", "Description"};
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
            createTourViewModel = new CreateTourViewModel(g);
            OtherKeyPoints =new List<string>();
            Appointments = new List<DateTime>();
            Images = new List<string>();
            AddKeyPointButton.IsEnabled = false;
            AddAppointmentButton.IsEnabled = false;
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
            if(!string.IsNullOrWhiteSpace(OtherKeyPointTextBox.Text))
            {
                AddKeyPointButton.IsEnabled = true;
            }
        }
        public void HourTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Match matchHour = _hoursRegex.Match(HourTextBox.Text);
            Match matchMinute = _minuteRegex.Match(MinuteTextBox.Text);
            if (TourDatePicker.SelectedDate != null && matchHour.Success && matchMinute.Success)
            {
                AddAppointmentButton.IsEnabled = true;
            }
        }
        public void MinuteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Match matchHour = _hoursRegex.Match(HourTextBox.Text);
            Match matchMinute = _minuteRegex.Match(MinuteTextBox.Text);
            if (TourDatePicker.SelectedDate != null && matchHour.Success && matchMinute.Success)
            {
                AddAppointmentButton.IsEnabled = true;
            }
        }
        public void TourDatePicker_SelectedDateChanged(object sender, EventArgs e)
        {
            Match matchHour = _hoursRegex.Match(HourTextBox.Text);
            Match matchMinute = _minuteRegex.Match(MinuteTextBox.Text);
            if (TourDatePicker.SelectedDate != null && matchHour.Success && matchMinute.Success)
            {
                AddAppointmentButton.IsEnabled = true;
            }
        }
        public void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            Appointments.Add(new DateTime(TourDatePicker.SelectedDate.Value.Year, TourDatePicker.SelectedDate.Value.Month,TourDatePicker.SelectedDate.Value.Day,Convert.ToInt32(HourTextBox.Text), Convert.ToInt32(MinuteTextBox.Text),0));
            TourDatePicker.SelectedDate = null;
            HourTextBox.Text = "";
            MinuteTextBox.Text = "";
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
                createTourViewModel.CreateTour(guide.Id,TourName,TourLanguage,City + "," + Country,MaxNumberGuests, Appointments, Duration,StartKeyPoint,OtherKeyPoints,FinishKeyPoint,Description,Images);
                this.NavigationService.Navigate(new ScheduledToursView(guide));
           }
           else
               MessageBox.Show("Nisu validno popunjena polja!");
        }
    }
}
