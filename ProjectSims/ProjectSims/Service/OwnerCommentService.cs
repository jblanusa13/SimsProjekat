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
    public class OwnerCommentService
    {
        private IOwnerRepository ownerRepository;
        private IOwnerCommentRepository commentRepository;

        public OwnerCommentService()
        {
            commentRepository = Injector.CreateInstance<IOwnerCommentRepository>();
            ownerRepository = Injector.CreateInstance<IOwnerRepository>();

            InitializeOwner();
        }
        private void InitializeOwner()
        {
            foreach (var comment in commentRepository.GetAll())
            {
                comment.Owner = ownerRepository.GetById(comment.OwnerId);
            }
        }

        public List<OwnerComment> GetAllOwnerComments()
        {
            return commentRepository.GetAll();
        }
        public List<OwnerComment> GetAllCommentsByOwner(int ownerId)
        {
            return commentRepository.GetAllByOwner(ownerId);
        }

        public void Subscribe(IObserver observer)
        {
            commentRepository.Subscribe(observer);
        }

    }
}
