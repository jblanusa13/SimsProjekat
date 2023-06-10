using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class OpenedForumViewModel : IObserver
    {
        public Owner Owner { get; set; }
        public Guest1 Guest { get; set; }
        public ObservableCollection<OwnerComment> Comments { get; set; }
        public Forum SelectedForum { get; set; }
        public NavigationService NavService { get; set; }
        public ForumService forumService { get; set; }
        public RelayCommand ReportCommand { get; set; }
        public RelayCommand CommentCommand { get; set; }

        public OpenedForumViewModel(Owner owner, NavigationService navService, Forum selectedForum) 
        {
            Owner = owner;
            NavService = navService;
            SelectedForum = selectedForum;
            forumService = new ForumService();
            forumService.Subscribe(this);
            Comments = new ObservableCollection<OwnerComment>(forumService.GetAllForums());
            ReportCommand = new RelayCommand(Execute_ReportCommand, CanExecute_Command);
            CommentCommand = new RelayCommand(Execute_CommentCommand, CanExecute_Command);
        }

        private void Execute_ReportCommand(object obj)
        { 
            
        }

        private bool CanExecute_Command(object obj) 
        {
            return SelectedForum != null;
        }

        private void Execute_CommentCommand(object obj)
        {

        }

        public void Update()
        {
            Comments.Clear();
            foreach (ForumComment comemnt in forumService.GetAllForums())
            {
                Comments.Add(forum);
            }
        }
    }
}
