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
    public class KeyPointService
    {
        private IKeyPointRepository keyPointRepository;

        public KeyPointService()
        {
            keyPointRepository = Injector.CreateInstance<IKeyPointRepository>();
        }
        public int GetNextId()
        {
            return keyPointRepository.NextId();
        }
        public List<KeyPoint> GetAllKeyPoints()
        {
            return keyPointRepository.GetAll();
        }
        public void Create(int id, string name, KeyPointType type)
        {
            keyPointRepository.Create(new KeyPoint(id,name,type,false));
        }
        public void Remove(KeyPoint keyPoint)
        {
            keyPointRepository.Remove(keyPoint);
        }
        public KeyPoint GetKeyPointById(int id)
        {
            return keyPointRepository.GetById(id);
        }
        public List<KeyPoint> GetKeyPointsByStateAndIds(List<int> ids,bool state)
        {
            return keyPointRepository.GetKeyPointsByStateAndIds(ids, state);
        }
        public void Finish(KeyPoint keyPoint)
        {
            keyPoint.Finished = true;
            keyPointRepository.Update(keyPoint);
        }
        public void Update(KeyPoint keyPoint)
        {
            keyPointRepository.Update(keyPoint);
        }
        public void Subscribe(IObserver observer)
        {
            keyPointRepository.Subscribe(observer);
        }
    }
}
