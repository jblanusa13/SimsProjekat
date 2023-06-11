using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.WPF.View.Guest1View;
using System.Windows;
using ceTe.DynamicPDF;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.Observer;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ProjectSims.Commands;
using ProjectSims.WPF.View.Guest1View.HelpPages;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class CreateForumViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string location;
        public string Location
        {
            get => location;
            set
            {
                if (value != location)
                {
                    location = value;
                    OnPropertyChanged();
                    StartForumCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private string comment;
        public string Comment
        {
            get => comment;
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged();
                    StartForumCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public ObservableCollection<string> Locations { get; set; }

        public MyICommand<View.Guest1View.ForumPages.CreateForum> StartForumCommand { get; set; }
        public MyICommand<View.Guest1View.ForumPages.CreateForum> CancelCommand { get; set; }
        public RelayCommand HelpCommand { get; set; }
        private LocationService locationService;
        private ForumService forumService;
        private Guest1 guest;

        public CreateForumViewModel(Guest1 guest)
        {
            locationService = new LocationService();
            forumService = new ForumService();
            StartForumCommand = new MyICommand<ProjectSims.WPF.View.Guest1View.ForumPages.CreateForum>(OnStart, CanStart);
            CancelCommand = new MyICommand<ProjectSims.WPF.View.Guest1View.ForumPages.CreateForum>(OnCancel);
            Locations = new ObservableCollection<string>(locationService.GetAllLocationsString());
            HelpCommand = new RelayCommand(Execute_HelpCommand);
            this.guest = guest;
        }

        private void Execute_HelpCommand(object obj)
        {
            HelpStartView helpStart = new HelpStartView();
            helpStart.SelectedTab.Content = new CreateForumHelpView();
            helpStart.Show();
        }

        public void OnCancel(View.Guest1View.ForumPages.CreateForum page)
        {
            ForumStartView startView = (ForumStartView)Window.GetWindow(page);
            startView.Close();
        }

        public bool CanStart(View.Guest1View.ForumPages.CreateForum page)
        {
            return IsValid;
        }

        public void OnStart(View.Guest1View.ForumPages.CreateForum page)
        {
            string city = Location.Split(", ")[0];
            string country = Location.Split(", ")[1];
            Location location = locationService.GetLocationByCityAndCountry(city, country);

            forumService.CreateForum(guest, location, Comment);

            ForumStartView startView = (ForumStartView)Window.GetWindow(page);
            startView.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public String Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Location")
                {
                    if (string.IsNullOrEmpty(Location))
                        return "Obavezno polje";
                }
                else if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                        return "Obavezno polje";
                }
                return null;

            }
        }

        private readonly string[] _validatedProperties = { "Location", "Comment" };
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
    }
}
