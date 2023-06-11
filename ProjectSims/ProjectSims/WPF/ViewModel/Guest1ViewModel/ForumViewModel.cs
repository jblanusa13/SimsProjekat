using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View;
using ProjectSims.Commands;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.View.Guest1View.NotifAndHelp;
using ProjectSims.WPF.View.Guest1View.HelpPages;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class ForumViewModel : IObserver, INotifyPropertyChanged
    {
        private string city;
        public string City
        {
            get => city;
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string country;
        public string Country
        {
            get => country;
            set
            {
                if (value != country)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string buttonContent;
        public string ButtonContent
        {
            get => buttonContent;
            set
            {
                if (value != buttonContent)
                {
                    buttonContent = value;
                    OnPropertyChanged();
                }
            }
        }

        private ForumService forumService;
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand FindMyForumsCommand { get; set; }
        public RelayCommand ShowForumCommand { get; set; }
        public RelayCommand StartNewForumCommand { get; set; }
        public RelayCommand ThemeCommand { get; set; }
        public RelayCommand NotifCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand HelpCommand { get; set; }
        public MyICommand<View.Guest1View.MainPages.ForumView> LogOutCommand { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }
        public Forum SelectedForum { get; set; }
        private Guest1 guest;
        public NavigationService NavService { get; set; }
        public ForumViewModel(Guest1 guest, NavigationService navigation)
        {
            forumService = new ForumService();
            forumService.Subscribe(this);
            Forums = new ObservableCollection<Forum>(forumService.GetAllForums());
            SearchCommand = new RelayCommand(Execute_SearchCommand);
            FindMyForumsCommand = new RelayCommand(Execute_FindMyForumsCommand);
            ShowForumCommand = new RelayCommand(Execute_ShowForumCommand, CanExecute_ShowForumCommand);
            StartNewForumCommand = new RelayCommand(Execute_StartNewForumCommand);
            ThemeCommand = new RelayCommand(Execute_ThemeCommand);
            NotifCommand = new RelayCommand(Execute_NotifCommand);
            CancelCommand = new RelayCommand(Execute_CancelCommand);
            LogOutCommand = new MyICommand<View.Guest1View.MainPages.ForumView>(OnLogOut);
            HelpCommand = new RelayCommand(Execute_HelpCommand);

            ButtonContent = "Nadji moje forume";

            this.guest = guest;
            NavService = navigation;
        }
        private void Execute_HelpCommand(object obj)
        {
            HelpStartView helpStart = new HelpStartView();
            helpStart.SelectedTab.Content = new ForumHelpView();
            helpStart.Show();
        }
        private void OnLogOut(View.Guest1View.MainPages.ForumView page)
        {
            var login = new MainWindow();
            login.Show();
            Window parentWindow = Window.GetWindow(page);
            parentWindow.Close();
        }

        private void Execute_CancelCommand(object obj)
        {
            NavService.GoBack();
        }

        private void Execute_ThemeCommand(object obj)
        {
            App app = (App)Application.Current;

            if (App.IsDark)
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                App.IsDark = false;
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                App.IsDark = true;
            }
        }
        private void Execute_NotifCommand(object obj)
        {
            NotificationsView notificationsView = new NotificationsView(guest);
            notificationsView.Show();
        }
        public bool CanExecute_ShowForumCommand(object obj)
        {
            return SelectedForum != null; 
        }
        public void Execute_ShowForumCommand(object obj)
        {
            NavService.Navigate(new ForumCommentsView(guest, NavService, SelectedForum));
        }
        public void Execute_StartNewForumCommand(object obj)
        {
            ForumStartView createForum = new ForumStartView(guest);
            createForum.Show();
        }

        public void Execute_SearchCommand(object obj)
        {
            Forums.Clear();
            foreach (var forum in forumService.GetAllForums())
            {
                if (CheckSearchConditions(forum))
                {
                    Forums.Add(forum);
                }
            }
        }

        private bool CheckSearchConditions(Forum forum)
        {
            bool ContainsCountry, ContainsCity;

            ContainsCity = string.IsNullOrEmpty(City) ? true : forum.Location.City.ToLower().Contains(City.ToLower());
            ContainsCountry = string.IsNullOrEmpty(Country) ? true : forum.Location.Country.ToLower().Contains(Country.ToLower());

            return ContainsCity && ContainsCountry;
        }

        public void Execute_FindMyForumsCommand(object obj)
        {
            if (ButtonContent == "Nadji moje forume")
            {
                Forums.Clear();
                foreach (Forum forum in forumService.GetAllForumsByGuest(guest.Id))
                {
                    Forums.Add(forum);
                }

                ButtonContent = "Prikazi sve";
            }
            else if(ButtonContent == "Prikazi sve")
            {
                Update();
                ButtonContent = "Nadji moje forume";
            }
        }


        public void Update()
        {
            Forums.Clear();
            foreach (Forum forum in forumService.GetAllForums())
            {
                Forums.Add(forum);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
