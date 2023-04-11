using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class GuideService
    {
        private GuideRepository guides;

        public GuideService()
        {
            guides = new GuideRepository();
        }

        public List<Guide> GetAllGuides()
        {
            return guides.GetAll();
        }

        public void Create(Guide guide)
        {
            guides.Add(guide);
        }

        public void Delete(Guide guide)
        {
            guides.Remove(guide);
        }

        public void Update(Guide guide)
        {
            guides.Update(guide);
        }

        public void Subscribe(IObserver observer)
        {
            guides.Subscribe(observer);
        }

        public Guide FindGuideById(int id)
        {
            return guides.FindById(id);
        }
    }
}
