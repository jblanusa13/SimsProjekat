using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Service
{
    public class GuideService
    {
        private IGuideRepository guideRepository;
        private IUserRepository userRepository;

        public GuideService()
        {
            guideRepository = Injector.CreateInstance<IGuideRepository>();
            userRepository = Injector.CreateInstance<IUserRepository>();
            InitializeUser();
        }
        private void InitializeUser()
        {
            foreach(var item in guideRepository.GetAll())
            {
                item.User = userRepository.GetById(item.UserId);
            }
        }

        public List<Guide> GetAllGuides()
        {
            return guideRepository.GetAll();
        }
        public Guide GetGuideById(int id)
        {
            return guideRepository.GetById(id);
        }
        public void Create(Guide guide)
        {
            guideRepository.Create(guide);
        }

        public void Delete(Guide guide)
        {
            guideRepository.Remove(guide);
        }

        public void Update(Guide guide)
        {
            guideRepository.Update(guide);
        }

        public void Subscribe(IObserver observer)
        {
            guideRepository.Subscribe(observer);
        }
    }
}
