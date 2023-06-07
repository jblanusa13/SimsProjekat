using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Service
{
    public class ForumService
    {
        private IForumRepository forumRepository;
        private ILocationRepository locationRepository;

        public ForumService()
        {
            forumRepository = Injector.CreateInstance<IForumRepository>();
            locationRepository = Injector.CreateInstance<ILocationRepository>();

            InitializeLocation();
        }

        private void InitializeLocation()
        {
            foreach (var forum in forumRepository.GetAll())
            {
                forum.Location = locationRepository.GetById(forum.LocationId);
            }
        }

        public List<Forum> GetAllForums()
        {
            return forumRepository.GetAll();
        }
    }
}
