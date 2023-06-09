using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSims.WPF.ViewModel.Guest2ViewModel
{
    public class UpdateAccountViewModel :  INotifyPropertyChanged,IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
        public Guest2 guest2 { get; set; }

        private Guest2Service guest2Service;

        public UpdateAccountViewModel(Guest2 g)
        {
            guest2Service = new Guest2Service();
            guest2 = g;
        }
        public string Name
        {
            get => guest2.Name;
            set
            {
                if (value != guest2.Name)
                {
                    guest2.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Surname
        {
            get => guest2.Surname;
            set
            {
                if (value != guest2.Surname)
                {
                    guest2.Surname = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime BirthDate
        {
            get => guest2.BirthDate;
            set
            {
                if (value != guest2.BirthDate)
                {
                    guest2.BirthDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Adress
        {
            get => guest2.Adress;
            set
            {
                if (value != guest2.Adress)
                {
                    guest2.Adress = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => guest2.Email;
            set
            {
                if (value != guest2.Email)
                {
                    guest2.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PhoneNumber
        {
            get => guest2.PhoneNumber;
            set
            {
                if (value != guest2.PhoneNumber)
                {
                    guest2.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Unesite Vase ime!";
                }
                else if (columnName == "Surname")
                {
                    if (string.IsNullOrEmpty(Surname))
                        return "Unesite Vase prezime!";
                }
                else if (columnName == "Adress")
                {
                    if (string.IsNullOrEmpty(Adress))
                        return "Unesite Vasu adresu stanovanja!";
                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "Unesite Vasu email adresu!";
                }
                else if (columnName == "PhoneNumber")
                {
                    if (string.IsNullOrEmpty(PhoneNumber))
                        return "Unesite Vas broj telefona!";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "Name", "Surname", "Adress", "Email", "PhoneNumber", "BirthDate" };
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
        public bool UpdateAccountData()
        {
            if (IsValid)
            {
                guest2Service.Update(guest2);
                MessageBox.Show("Uspjeno ste izmjenili podatke!");
                return true;
            }
            else
            {
                MessageBox.Show("Nisu sva polja popunjena!");
                return false;
            }
        }
    }
}
