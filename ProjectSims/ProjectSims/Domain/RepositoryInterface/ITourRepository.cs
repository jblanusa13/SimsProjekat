using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface ITourRepository
    {
        public void Create(Tour tour);
        public void Update(Tour tour);
        public void Remove(Tour tour);
        public List<Tour> GetAll();
        public Tour GetTourById(int id);
        public List<Tour> GetToursByStateAndGuideId(TourState state, int guideId);
        public Tour GetTourByStateAndGuideId(TourState state, int guideId);
        public List<Tour> GetTodayTours(int guideId);
        public int NextId();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();

    }
}
