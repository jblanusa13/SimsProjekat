using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class OpenedForumViewModel : IObserver, INotifyPropertyChanged
    {
        public Owner Owner { get; set; }
        public Guest1 Guest { get; set; }
        public ObservableCollection<ForumComment> Comments { get; set; }
        public Forum ActiveForum { get; set; }
        public ForumComment SelectedForumComment { get; set; }
        public NavigationService NavService { get; set; }
        public ForumCommentService commentService { get; set; }
        public RelayCommand ReportCommand { get; set; }
        public RelayCommand CommentCommand { get; set; }
        private string _comment;
        public string Comment
        {
            get => _comment;

            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public OpenedForumViewModel(Owner owner, NavigationService navService, Forum selectedForum) 
        {
            Owner = owner;
            NavService = navService;
            ActiveForum = selectedForum;
            commentService = new ForumCommentService();
            commentService.Subscribe(this);
            Comments = new ObservableCollection<ForumComment>(commentService.GetAllCommentsByForumId(selectedForum.Id));
            ReportCommand = new RelayCommand(Execute_ReportCommand, CanExecute_ReportCommand);
            CommentCommand = new RelayCommand(Execute_CommentCommand, CanExecute_CommentCommand);
        }

        private void Execute_ReportCommand(object obj)
        {
            commentService.ReportComment(SelectedForumComment);
        }
        private bool CanExecute_ReportCommand(object obj)
        {
            return SelectedForumComment != null && SelectedForumComment.IsGuest == true && SelectedForumComment.GuestVisited == false;
        }
        private void Execute_CommentCommand(object obj)
        {
            commentService.CommentForum(ActiveForum, Comment, Owner);
        }
        private bool CanExecute_CommentCommand(object obj)
        {
            return !string.IsNullOrEmpty(Comment);
        }
        public void Update()
        {
            Comments.Clear();
            foreach (ForumComment comment in commentService.GetAllCommentsByForumId(ActiveForum.Id))
            {
                Comments.Add(comment);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
