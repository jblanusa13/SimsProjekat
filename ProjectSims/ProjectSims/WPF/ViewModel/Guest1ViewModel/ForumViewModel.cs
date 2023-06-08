using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View;

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
        public MyICommand SearchCommand { get; set; }
        public MyICommand FindMyForumsCommand { get; set; }
        public MyICommand StartNewForumCommand { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }
        private Guest1 guest;
        public ForumViewModel(Guest1 guest)
        {
            forumService = new ForumService();
            Forums = new ObservableCollection<Forum>(forumService.GetAllForums());
            SearchCommand = new MyICommand(OnSearch);
            FindMyForumsCommand = new MyICommand(OnFind);
            StartNewForumCommand = new MyICommand(OnStart);

            ButtonContent = "Nadji moje forume";

            this.guest = guest;
        }
        public void OnStart()
        {
            ForumStartView createForum = new ForumStartView(guest);
            createForum.Show();
        }

        public void OnSearch()
        {
            Forums.Clear();
            foreach (Forum forum in forumService.GetAllForums())
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

        public void OnFind()
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
