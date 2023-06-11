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
using ceTe.DynamicPDF.Forms;
using ProjectSims.Commands;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using ProjectSims.WPF.View.Guest1View.HelpPages;
using ProjectSims.WPF.View.Guest1View;
using ProjectSims.WPF.View.Guest1View.MainPages;
using ProjectSims.WPF.View.Guest1View.NotifAndHelp;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class ForumCommentsViewModel : IObserver, INotifyPropertyChanged
    {
        private ForumCommentService forumCommentService;
        private ForumService forumService;
        private AccommodationReservationService accommodationReservationService;
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
                }
            }
        }
        public ObservableCollection<ForumComment> Comments { get; set; }
        public bool IsGuest { get; set; }
        public Guest1 Guest { get; set; }
        public Forum Forum { get; set; }
        public RelayCommand ThemeCommand { get; set; }
        public RelayCommand NotifCommand { get; set; }
        public RelayCommand CloseForumCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand HelpCommand { get; set; }
        public RelayCommand LeaveCommentCommand { get; set; }
        public MyICommand<View.Guest1View.MainPages.ForumCommentsView> LogOutCommand { get; set; }
        public NavigationService NavService { get; set; }
        public ForumCommentsView ForumCommentsView { get; set; }
        public ForumCommentsViewModel(Guest1 guest, NavigationService navigation, ForumCommentsView forumCommentsView, Forum forum)
        {
            Guest = guest;
            NavService = navigation;
            ForumCommentsView = forumCommentsView;
            Forum = forum;
            forumCommentService = new ForumCommentService();
            forumService = new ForumService();
            accommodationReservationService = new AccommodationReservationService();
            forumCommentService.Subscribe(this);
            ThemeCommand = new RelayCommand(Execute_ThemeCommand);
            NotifCommand = new RelayCommand(Execute_NotifCommand);
            CancelCommand = new RelayCommand(Execute_CancelCommand);
            CloseForumCommand = new RelayCommand(Execute_CloseForumCommand);
            LeaveCommentCommand = new RelayCommand(Execute_LeaveCommentCommand, CanExecute_LeaveCommentCommand);
            LogOutCommand = new MyICommand<View.Guest1View.MainPages.ForumCommentsView>(OnLogOut);
            Comments = new ObservableCollection<ForumComment>(forumCommentService.GetAllCommentsByForumId(Forum.Id));
            HelpCommand = new RelayCommand(Execute_HelpCommand);

            SetCloseButton();
            if(Forum.Status == ForumStatus.Zatvoren)
            {
                SetViewForClosedForum();
            }
            else
            {
                SetViewForOpenForum();
            }

            SetUsefulForum();
        }

        public void SetUsefulForum()
        {
            if (forumService.CheckIfVeryUseful(Forum))
            {
                ForumCommentsView.UsefulGrid.Visibility = Visibility.Visible;
            }
            else
            {
                ForumCommentsView.UsefulGrid.Visibility = Visibility.Hidden;
            }
        }

        public void SetCloseButton()
        {
            if(Forum.Status == ForumStatus.Otvoren && Forum.GuestId == Guest.Id)
            {
                ForumCommentsView.CloseButton.Visibility = Visibility.Visible;
            }
            else
            {
                ForumCommentsView.CloseButton.Visibility = Visibility.Hidden;
            }
        }

        public void SetViewForClosedForum()
        {
            ForumCommentsView.CommentTb.IsEnabled = false;
            Comment = "Ovaj forum je zatvoren";
            ForumCommentsView.CommentLabel.Visibility = Visibility.Hidden;
            ForumCommentsView.CommentBtn.Visibility = Visibility.Hidden;
        }

        public void SetViewForOpenForum()
        {
            ForumCommentsView.CommentTb.IsEnabled = true;
            ForumCommentsView.CommentLabel.Visibility = Visibility.Visible;
            ForumCommentsView.CommentBtn.Visibility = Visibility.Visible;
        }

        private bool CanExecute_LeaveCommentCommand(object obj)
        {
            return !string.IsNullOrEmpty(Comment);
        }

        private void Execute_LeaveCommentCommand(object obj)
        {
            forumCommentService.CreateComment(Forum, Guest, Comment);
            ForumCommentsView.CommentTb.Text = "";
        }

        private void Execute_CloseForumCommand(object obj)
        {
            forumService.CloseForum(Forum.Id);
            SetCloseButton();
            SetViewForClosedForum();
        }
        private void Execute_HelpCommand(object obj)
        {
            HelpStartView helpStart = new HelpStartView();
            helpStart.SelectedTab.Content = new ForumCommentsHelpView();
            helpStart.Show();
        }

        private void OnLogOut(View.Guest1View.MainPages.ForumCommentsView page)
        {
            var login = new MainWindow();
            login.Show();
            Window parentWindow = Window.GetWindow(page);
            parentWindow.Close();
        }
        public void Execute_CancelCommand(object obj)
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
            NotificationsView notificationsView = new NotificationsView(Guest);
            notificationsView.Show();
        }
        public void Update()
        {
            Comments.Clear();
            foreach (ForumComment comment in forumCommentService.GetAllCommentsByForumId(Forum.Id))
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
