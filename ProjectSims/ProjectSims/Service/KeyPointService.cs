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
    public class KeyPointService
    {
        private KeyPointRepository keyPoints;

        public KeyPointService()
        {
            keyPoints= new KeyPointRepository();
        }
        public int GetNextId()
        {
            return keyPoints.GetNextId();
        }
        public List<KeyPoint> GetAllKeyPoints()
        {
            return keyPoints.GetAll();
        }
        public void Create(int id, string name, KeyPointType type)
        {
            keyPoints.Create(new KeyPoint(id,name,type,false));
        }
        public void Remove(KeyPoint keyPoint)
        {
            keyPoints.Remove(keyPoint);
        }
        public KeyPoint GetKeyPointById(int id)
        {
            return keyPoints.GetKeyPointById(id);
        }
        public List<KeyPoint> GetKeyPointsByStateAndIds(List<int> ids,bool state)
        {
            return keyPoints.GetKeyPointsByStateAndIds(ids, state);
        }
        public void Finish(KeyPoint keyPoint)
        {
            keyPoint.Finished = true;
            keyPoints.Update(keyPoint);
        }
        public void Update(KeyPoint keyPoint)
        {
            keyPoints.Update(keyPoint);
        }
        public void Subscribe(IObserver observer)
        {
            keyPoints.Subscribe(observer);
        }
    }
}
