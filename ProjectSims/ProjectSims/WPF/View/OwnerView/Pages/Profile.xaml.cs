using ProjectSims.Domain.Model;
using ProjectSims.WPF.ViewModel.OwnerViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.OwnerView.Pages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page, INotifyPropertyChanged
    {
        public Owner Owner { get; set; }
        public ProfileViewModel profileViewModel { get; set; }

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

        private string _surname;
        public string Surname
        {
            get => _surname;

            set
            {
                if (value != _surname)
                {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;

            set
            {
                if (value != _password)
                {
                    _password = value;
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

        private string _address;
        public string Address
        {
            get => _address;

            set
            {
                if (value != _address)
                {
                    _address = value;
                    OnPropertyChanged();
                }
            }
        }

        public Profile(Owner o)
        {
            InitializeComponent();
            Owner = o;
            profileViewModel = new ProfileViewModel(Owner);
            this.DataContext = profileViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            profileViewModel.ChangeProfile(NameTextBox.Text, UsernameTextBox.Text, PasswordTextBox.Text, EmailTextBox.Text, AddressTextBox.Text, SuperOwnerCheckBox.IsChecked);
        }
    }
}
