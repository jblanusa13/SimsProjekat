using ProjectSims.Controller;
using ProjectSims.Model;
using ProjectSims.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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

namespace ProjectSims.View
{
    /// <summary>
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class OwnerView : Window, INotifyPropertyChanged, IObserver
    {
        private readonly GuestAccommodationController _guestAccommodationController;
        public ObservableCollection<GuestAccommodation> GuestAccommodations { get; set; }
        public GuestAccommodation SelectedGuestAccommodation { get; set; }
        public OwnerView()
        {
            InitializeComponent();
            DataContext = this;

            _guestAccommodationController = new GuestAccommodationController();
            _guestAccommodationController.Subscribe(this);
            GuestAccommodations = new ObservableCollection<GuestAccommodation>(_guestAccommodationController.GetAllGuestAccommodations());
        }

        /*
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateOnly _checkInDate;
        public DateOnly CheckInDate
        {
            get => _checkInDate;
            set
            {
                if (value != _checkInDate)
                {
                    _checkInDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateOnly _checkOutDate;
        public DateOnly CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                if (value != _checkOutDate)
                {
                    _checkOutDate = value;
                    OnPropertyChanged();
                }
            }
        }*/
        private bool _rated;
        public bool Rated
        {
            get => _rated;
            set
            {
                if (value != _rated)
                {
                    _rated = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       private void RateGuest_Click(object sender, RoutedEventArgs e)
        {
            SelectedGuestAccommodation = (GuestAccommodation)GuestAccommodationsTable.SelectedItem;

            if (SelectedGuestAccommodation != null && SelectedGuestAccommodation.Rated == false)
            {
                GuestRatingtView guestRatingtView = new GuestRatingtView(SelectedGuestAccommodation, _guestAccommodationController);
                guestRatingtView.Show();
            }
            else
            {
                String message = "Gost " + SelectedGuestAccommodation.FirstName + " " + SelectedGuestAccommodation.LastName + " je vec ocijenjen!";
                MessageBox.Show(message, "Ocjenjivanje gosta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        /*
        private IEnumerable<DataGridRow> GetDataGridRowsForButtons(DataGrid grid)
        {
            //IQueryable
            if (!(grid.ItemsSource is IEnumerable GuestAccommodations))
                yield return null;

            foreach (var item in GuestAccommodations)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row & row.IsSelected)
                    yield return row;
            }
        }

        private void RowButton_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    // var row = (DataGrid)vis;

                    var rows = GetDataGridRowsForButtons(dgv_Students);

                    string id;
                    foreach (DataGridRow dr in rows)
                    {
                        id = (dr.Item as tbl_student).Identification_code;
                        MessageBox.Show(id);
                        break;
                    }

                    break;
                }
        }*/

        private void RegistrateAccommodation_Click(object sender, RoutedEventArgs e)
        {
                AccommodationRegistrationView accommodationRegistrationView = new AccommodationRegistrationView();
                accommodationRegistrationView.Show();
        }

        public void Update()
        {
            GuestAccommodations.Clear();
            foreach (GuestAccommodation guestAccommodation in _guestAccommodationController.GetAllGuestAccommodations()) 
            {
                GuestAccommodations.Add(guestAccommodation);
            }
        }
    }
}
