using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using ProjectSims.Repository;
using Syncfusion.Windows.Shared;

namespace ProjectSims.Service
{
    public class ForumCommentService
    {
        private IGuest1Repository guestRepository;
        private IOwnerRepository ownerRepository;
        private IForumCommentRepository forumCommentRepository;
        private IForumRepository forumRepository;
        public ForumCommentService()
        {
            guestRepository = Injector.CreateInstance<IGuest1Repository>();
            ownerRepository = Injector.CreateInstance<IOwnerRepository>();
            forumCommentRepository = Injector.CreateInstance<IForumCommentRepository>();
            forumRepository = Injector.CreateInstance<IForumRepository>();

            InitializeGuest1();
            InitializeOwner();
            InitializeForum();
        }
        private void InitializeGuest1()
        {
            foreach (var comment in forumCommentRepository.GetAll())
            {
                if (comment.GuestId != -1)
                {
                    comment.Guest = guestRepository.GetById(comment.GuestId);
                    comment.Name = comment.Guest.Name;
                    comment.Surname = comment.Guest.Surname;
                }
            }
        }
        private void InitializeForum()
        {
            foreach (var comment in forumCommentRepository.GetAll())
            {
                comment.Forum = forumRepository.GetById(comment.ForumId);
            }
        }
        private void InitializeOwner()
        {
            foreach (var comment in forumCommentRepository.GetAll())
            {
                if (comment.OwnerId != -1)
                {
                    comment.Owner = ownerRepository.GetById(comment.OwnerId);
                    comment.Name = comment.Owner.Name;
                    comment.Surname = comment.Owner.Surname;
                }
            }
        }

        public List<ForumComment> GetAllComments()
        {
            return forumCommentRepository.GetAll();
        }
        public List<ForumComment> GetAllCommentsByForumId(int forumId)
        {
            return forumCommentRepository.GetAllByForumId(forumId);
        }
        public void Subscribe(IObserver observer)
        {
            forumCommentRepository.Subscribe(observer);
        }

        public void ReportComment(ForumComment comment)
        {
            comment.ReportNumber = ((int.Parse(comment.ReportNumber))+1).ToString();
            forumCommentRepository.Update(comment);
        }
        public void CommentForum(Forum forum, string comment, Owner owner)
        {
            forumCommentRepository.Create(new ForumComment(forumCommentRepository.NextId(), forum.Id, forum, -1, null, 
                                          owner.Id, owner, owner.Name, owner.Surname, comment, false, false, ""));
        }
    }
}