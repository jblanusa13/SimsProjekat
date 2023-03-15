using ProjectSims.Controller;
using ProjectSims.Model;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace ProjectSims
{
    /// <summary>
    /// Interaction logic for TourDisplayAndSearchView.xaml
    /// </summary>
    public partial class TourDisplayAndSearchView : Window , IObserver
    {
        private TourController tourController;

        public  ObservableCollection<Tour> ListTour { get; }
        public Tour SelectedTour { get; set; }
        public TourDisplayAndSearchView()
        {
            InitializeComponent();
            DataContext = this;
            
            tourController = new TourController();
            tourController.Subscribe(this);           
            ListTour = new ObservableCollection<Tour>(tourController.GetAllTours());
            

        }

        private void UpdateTourList()
        {
            ListTour.Clear();
            foreach (var tour in tourController.GetAllTours())
            {
                ListTour.Add(tour);
            }
        }

        public void Update()
        {
            UpdateTourList();
        }

        private void See_More_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                var see_more = new DetailsTourWindow(SelectedTour);
                see_more.Show();
            }
            else
            {
                MessageBox.Show("You must select a tour for more details!");
            }

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            String location = LocationTextBox.Text;

            double durationStart;
            double durationEnd;
            //Validation wrong input
            if (string.IsNullOrEmpty(DurationStartTextBox.Text))
            {
                durationStart = -1;
            }
            else if( !double.TryParse(DurationStartTextBox.Text, out durationStart))
            {
                MessageBox.Show("Wrong input! Duration tour must be a double!");
                return;
            }
            else if (durationStart < 0)
            {
                MessageBox.Show("The tour duration search fields cannot have a negative value");
                return;
            }
            if (string.IsNullOrEmpty(DurationEndTextBox.Text))
            {
                durationEnd = -1;
            }
            else if (!double.TryParse(DurationEndTextBox.Text, out durationEnd))
            {
                MessageBox.Show("Wrong input! Duration tour must be a double!");
                return;
            }else if( durationEnd < 0)
            {
                MessageBox.Show("The tour duration search fields cannot have a negative value");
                return;
            }
            //both duration fields must be entered
            if((durationStart == -1 && durationEnd != -1) || (durationStart != -1 && durationEnd == -1))
            {
                MessageBox.Show("Both duration fields must be entered for search tours!");
                return;
            }else if(durationStart != -1 && durationEnd != -1)
            //if both fields are entered the first must be less than the second
            {
                if(durationStart > durationEnd)
                {
                    MessageBox.Show("The first duration fiels must be less than the second!");
                    return;
                }
            }

            String language = LanguageTextBox.Text;

            int numberGuests;
            //Validation wrong input
            if (string.IsNullOrEmpty(NumberGuestsTextBox.Text))
            {
                numberGuests = -1;
            }
            else if( !int.TryParse(NumberGuestsTextBox.Text, out numberGuests) )
            {
                MessageBox.Show("Wrong input! Number guests on tour must be a integer!");
                return;
            }else if(numberGuests < 0)
            {
                MessageBox.Show("The number of people on the tour can't be negative!");
                return;
            }

            if (location == "" && durationStart == -1 && language == "" && numberGuests == -1)         //16. case (nothing entered)
            {
                MessageBox.Show("You must enter some information for search!");
                ListTour.Clear();
                foreach (var tour in tourController.GetAllTours())
                {
                    ListTour.Add(tour);
                }
                return;
            }
            List<Tour> wantedTours = tourController.SearchTours(location,durationStart,durationEnd,language,numberGuests);
            ListTour.Clear();
            foreach (Tour tour in wantedTours)
            {
                ListTour.Add(tour);
            }
        }
    }
}
