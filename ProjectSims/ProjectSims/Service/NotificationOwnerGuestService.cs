using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using ProjectSims.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class NotificationOwnerGuestService
    {
        private INotificationOwnerGuestRepository notificationRepository;
        private IGuest1Repository guest1Repository;
        private IOwnerRepository ownerRepository;
        private IForumRepository forumRepository;
        private IRequestRepository requestRepository;
        public NotificationOwnerGuestService()
        {
            notificationRepository = Injector.CreateInstance<INotificationOwnerGuestRepository>();
            guest1Repository = Injector.CreateInstance<IGuest1Repository>();
            ownerRepository = Injector.CreateInstance<IOwnerRepository>();
            forumRepository = Injector.CreateInstance<IForumRepository>();
            requestRepository = Injector.CreateInstance<IRequestRepository>();
            InitializeGuest();
            InitializeOwner();
            InitializeForum();
            InitializeRequest();
        }

        private void InitializeGuest()
        {
            foreach (var item in notificationRepository.GetAll())
            {
                item.Guest1 = guest1Repository.GetById(item.Guest1Id);
            }
        }
        private void InitializeOwner()
        {
            foreach (var item in notificationRepository.GetAll())
            {
                item.Owner = ownerRepository.GetById(item.OwnerId);
            }
        }        
        private void InitializeForum()
        {
            foreach (var item in notificationRepository.GetAll())
            {
                item.Forum = forumRepository.GetById(item.ForumId);
            }
        }
        private void InitializeRequest()
        {
            foreach (var item in notificationRepository.GetAll())
            {
                item.Request = requestRepository.GetById(item.RequestId);
            }
        }
        public int NextId()
        {
            return notificationRepository.NextId();
        }
        public List<NotificationOwnerGuest> GetAllNotifications()
        {
            return notificationRepository.GetAll();
        }     
        public List<Forum> GetAllForums()
        {
            return notificationRepository.GetAllForums();
        }
        public NotificationOwnerGuest GetById(int id)
        {
            return notificationRepository.GetById(id);
        }
/*
        public int GetNumberUnseenNotificationsByGuest2(int guest2Id)
        {
            int number = 0;
            foreach(NotificationOwnerGuest notificationTour in GetAllNotificationsByGuest2(guest2Id))
            {
                if (notificationTour.Seen == false) number++;
            }
            return number;
        }
*/
        public void Create(NotificationOwnerGuest notificationTour)
        {
            notificationRepository.Create(notificationTour);
        }
        public void Delete(NotificationOwnerGuest notificationTour)
        {
            notificationRepository.Remove(notificationTour);
        }
        public void Update(NotificationOwnerGuest notificationTour)
        {
            notificationRepository.Update(notificationTour);
        }
        public void Subscribe(IObserver observer)
        {
            notificationRepository.Subscribe(observer);
        }
    }
}
