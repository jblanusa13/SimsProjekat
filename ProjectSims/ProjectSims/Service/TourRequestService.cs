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
    public class TourRequestService
    {
        private ITourRequestRepository tourRequestRepository;
        public TourRequestService()
        {
            tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
        }
        public int GetNextId()
        {
            return tourRequestRepository.GetNextId();
        }
        public List<TourRequest> GetAllRequests()
        {
            return tourRequestRepository.GetAll();
        }
        public TourRequest GetById(int id)
        {
            return tourRequestRepository.GetById(id);
        }
        public void Create(TourRequest tourRequest)
        {
            tourRequestRepository.Create(tourRequest);
        }
        public void Delete(TourRequest tourRequest)
        {
            tourRequestRepository.Remove(tourRequest);
        }
        public void Update(TourRequest tourRequest)
        {
            tourRequestRepository.Update(tourRequest);
        }
        public void Subscribe(IObserver observer)
        {
            tourRequestRepository.Subscribe(observer);
        }
    }
}
