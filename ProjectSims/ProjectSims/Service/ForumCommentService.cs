using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class ForumCommentService
    {
        private IGuest1Repository guestRepository;
        private IOwnerRepository ownerRepository;
        private IForumCommentRepository forumCommentRepository;
        public ForumCommentService()
        {
            guestRepository = Injector.CreateInstance<IGuest1Repository>();
            ownerRepository = Injector.CreateInstance<IOwnerRepository>();
            forumCommentRepository = Injector.CreateInstance<IForumCommentRepository>();

            InitializeGuest1();
            InitializeOwner();
        }
        private void InitializeGuest1()
        {
            foreach (var comment in forumCommentRepository.GetAll())
            {
                if (comment.GuestId != -1)
                {
                    comment.Guest = guestRepository.GetById(comment.GuestId);
                }
            }
        }

        private void InitializeOwner()
        {
            foreach (var comment in forumCommentRepository.GetAll())
            {
                if (comment.OwnerId != -1)
                {
                    comment.Owner = ownerRepository.GetById(comment.OwnerId);
                }
            }
        }
    }
}
