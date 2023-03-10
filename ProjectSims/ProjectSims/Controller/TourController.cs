using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.ModelDAO;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Controller
{
    public class TourController
    {
        private TourDAO tours;

        public TourController()
        {
            tours = new TourDAO();
        }

        public List<Tour> GetAllTours()
        {
            return tours.GetAll();
        }

        public void Create(Tour tour)
        {
            tours.Add(tour);
        }

        public void Delete(Tour tour)
        {
            tours.Remove(tour);
        }

        public void Update(Tour tour)
        {
            tours.Update(tour);
        }

        public void Subscribe(IObserver observer)
        {
            tours.Subscribe(observer);
        }
    }
}
