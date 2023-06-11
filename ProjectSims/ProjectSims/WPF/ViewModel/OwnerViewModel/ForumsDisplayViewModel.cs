using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.View.OwnerView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class ForumsDisplayViewModel : IObserver, INotifyPropertyChanged
    {
        public ObservableCollection<Forum> Forums { get; set; }
        public Forum SelectedForum { get; set; }
        public Owner Owner { get; set; }
        private ForumService forumService;
        public NavigationService NavService { get; set; }
        public RelayCommand OpenCommand { get; set; }

        public ForumsDisplayViewModel(Owner owner, NavigationService navService) 
        {
            Owner = owner;
            NavService = navService;
            forumService = new ForumService();
            forumService.Subscribe(this);
            Forums = new ObservableCollection<Forum>(forumService.GetAllForumsByOwner(Owner));
            OpenCommand = new RelayCommand(Execute_OpenCommand, CanExecute_OpenCommand);
            forumService.SetUsefulnessForEachForum(Owner);
        }
        public void Execute_OpenCommand(object obj)
        {
            NavService.Navigate(new OpenedForumView(Owner, NavService, SelectedForum));
        }

        public bool CanExecute_OpenCommand(object obj)
        {
            return SelectedForum != null;
        }

        public void Update()
        {
            Forums.Clear();
            foreach (Forum forum in forumService.GetAllForumsByOwner(Owner))
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
