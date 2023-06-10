using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ceTe.DynamicPDF.Forms;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;

namespace ProjectSims.WPF.ViewModel.Guest1ViewModel
{
    public class ForumCommentsViewModel : IObserver
    {
        private ForumCommentService forumCommentService;
        public ObservableCollection<ForumComment> Comments { get; set; }
        public ForumCommentsViewModel()
        {
            forumCommentService = new ForumCommentService();
        }
        public void Update()
        {
            Comments.Clear();
            foreach (ForumComment comment in Comments)
            {
                Comments.Add(comment);
            }
        }
    }
}
