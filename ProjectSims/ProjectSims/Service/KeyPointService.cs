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
        public List<KeyPoint> GetAllKeyPoints()
        {
            return keyPoints.GetAll();
        }
        public void Create(KeyPoint keyPoint)
        {
            keyPoints.Add(keyPoint);
        }

        public void Delete(KeyPoint keyPoint)
        {
            keyPoints.Remove(keyPoint);
        }
        public void Finish(KeyPoint keyPoint)
        {
            keyPoint.Finished = true;
            keyPoints.Update(keyPoint);
        }
        public string FindNameById(int id)
        {
            List<KeyPoint> allKeyPoints = keyPoints.GetAll();
            return allKeyPoints.Find(k => k.Id == id).Name;
        }
        public List<KeyPoint> FindKeyPointsByIds(List<int> ids)
        {
            List<KeyPoint> returnkeyPoints = new List<KeyPoint>();
            foreach (KeyPoint keyPoint in keyPoints.GetAll())
            {
                foreach (int id in ids)
                {
                    if (keyPoint.Id == id)
                    {
                        returnkeyPoints.Add(keyPoint);
                    }
                }
            }
            return returnkeyPoints;
        }

        public List<KeyPoint> FindFinishedKeyPointsByIds(List<int> ids)
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            foreach (KeyPoint keyPoint in FindKeyPointsByIds(ids))
            { 
                    if (keyPoint.Finished)
                    {
                        keyPoints.Add(keyPoint);
                    }
            }
            return keyPoints;
        }
        public List<KeyPoint> FindUnFinishedKeyPointsByIds(List<int> ids)
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            foreach (KeyPoint keyPoint in FindKeyPointsByIds(ids))
            {
                if (!keyPoint.Finished)
                {
                    keyPoints.Add(keyPoint);
                }
            }
            return keyPoints;
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
