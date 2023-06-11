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
    public class ForumService
    {
        private IForumRepository forumRepository;
        private ILocationRepository locationRepository;
        private IGuest1Repository guest1Repository;
        private IForumCommentRepository commentRepository;

        public ForumService()
        {
            forumRepository = Injector.CreateInstance<IForumRepository>();
            locationRepository = Injector.CreateInstance<ILocationRepository>();
            guest1Repository = Injector.CreateInstance<IGuest1Repository>();
            commentRepository = Injector.CreateInstance<IForumCommentRepository>();

            InitializeLocation();
            InitializeGuest();
        }

        private void InitializeLocation()
        {
            foreach (var forum in forumRepository.GetAll())
            {
                forum.Location = locationRepository.GetById(forum.LocationId);
            }
        }
        private void InitializeGuest()
        {
            foreach (var forum in forumRepository.GetAll())
            {
                forum.Guest = guest1Repository.GetById(forum.GuestId);
            }
        }

        public List<Forum> GetAllForums()
        {
            return forumRepository.GetAll();
        }
        public List<Forum> GetAllForumsByGuest(int guestId)
        {
            return forumRepository.GetAllByGuest(guestId);
        }

        public void CreateForum(Guest1 guest, Location location, string comment)
        {
            int id = forumRepository.NextId();
            Forum forum = new Forum(id, location.Id, location, comment, ForumStatus.Otvoren, guest.Id, guest);
            forumRepository.Create(forum);
        }

        public void CloseForum(int forumId)
        {
            Forum forumToClose = forumRepository.GetById(forumId);
            forumToClose.Status = ForumStatus.Zatvoren;
            forumRepository.Update(forumToClose);
        }
        public void Subscribe(IObserver observer)
        {
            forumRepository.Subscribe(observer);
        }

        public bool CheckIfVeryUseful(Forum forum)
        {
            return IsVeryUsefulForum(commentRepository.GetAllByForumId(forum.Id));
        }

        public bool IsVeryUsefulForum(List<ForumComment> comments)
        {
            int guestCommentsCounter = 0;
            int ownerCommentsCounter = 0;
            foreach (ForumComment comment in comments)
            {
                if (comment.IsGuest && comment.GuestVisited)
                {
                    guestCommentsCounter++;
                }
                else
                {
                    ownerCommentsCounter++;
                }
            }
            return guestCommentsCounter >= 20 && ownerCommentsCounter >= 10;
        }

    }
}
