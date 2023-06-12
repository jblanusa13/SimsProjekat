using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class ForumCommentService
    {
        private IGuest1Repository guestRepository;
        private IOwnerRepository ownerRepository;
        private IForumCommentRepository forumCommentRepository;
        private IForumRepository forumRepository;
        private IAccommodationReservationRepository reservationRepository;

        public ForumCommentService()
        {
            guestRepository = Injector.CreateInstance<IGuest1Repository>();
            ownerRepository = Injector.CreateInstance<IOwnerRepository>();
            forumCommentRepository = Injector.CreateInstance<IForumCommentRepository>();
            forumRepository = Injector.CreateInstance<IForumRepository>();
            reservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();

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

        private void InitializeForum()
        {
            foreach (var comment in forumCommentRepository.GetAll())
            {
                comment.Forum = forumRepository.GetById(comment.ForumId);
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

        public void CreateComment(Forum forum, Guest1 guest, string comment)
        {
            bool visited = CheckIfVisited(forum, guest);
            int id = forumCommentRepository.NextId();
            ForumComment forumComment = new ForumComment(id, forum.Id, forum, guest.Id, guest, -1, null, guest.Name, guest.Surname, comment, visited, true, "");
            forumCommentRepository.Create(forumComment);
        }

        public void CommentForum(Forum forum, string comment, Owner owner)
        {
            forumCommentRepository.Create(new ForumComment(forumCommentRepository.NextId(), forum.Id, forum, -1, null,
                                          owner.Id, owner, owner.Name, owner.Surname, comment, false, false, ""));
        }

        public bool CheckIfVisited(Forum forum, Guest1 guest)
        {
            List<AccommodationReservation> guestReservations = reservationRepository.GetAllActiveByGuest(guest.Id);
            foreach (AccommodationReservation reservation in guestReservations)
            {
                if(reservation.Accommodation.Location == forum.Location && reservation.CheckOutDate <= DateOnly.FromDateTime(DateTime.Today))
                {
                    return true;
                }
            }
            return false;
        }

        public void Subscribe(IObserver observer)
        {
            forumCommentRepository.Subscribe(observer);
        }
        public void ReportComment(ForumComment comment)
        {
            comment.ReportNumber = ((int.Parse(comment.ReportNumber)) + 1).ToString();
            forumCommentRepository.Update(comment);
        }

    }
}