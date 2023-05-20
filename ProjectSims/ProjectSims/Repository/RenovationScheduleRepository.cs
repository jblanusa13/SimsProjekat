using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.WPF.View.Guest1View.MainPages;

namespace ProjectSims.Repository
{
    public class RenovationScheduleRepository : IRenovationScheduleRepository
    {
        private RenovationScheduleFileHandler renovationFileHandler;
        private List<RenovationSchedule> renovations;

        public RenovationScheduleRepository()
        {
            renovationFileHandler = new RenovationScheduleFileHandler();
            renovations = renovationFileHandler.Load();
        }

        public List<RenovationSchedule> GetAll()
        {
            return renovations;
        }

        public List<DateOnly> GetPassedRenovationDatesforAccommodation(int id)
        {
            List<DateOnly> dates = new List<DateOnly>();
            foreach (RenovationSchedule renovation in renovations)
            {
                if (renovation.AccommodationId == id && renovation.DateRange.CheckOut < DateOnly.FromDateTime(DateTime.Today))
                {
                    dates.Add(renovation.DateRange.CheckOut);
                }
            }
            return dates;
        }

        public DateOnly FindMaxDate(List<DateOnly> dates)
        {
            if (dates.Count != 0)
            {
                return dates.Max();
            }
            else 
            {
                return new DateOnly(1, 1, 1);
            }
        }

        public int NextId()
        {
            if (renovations.Count == 0)
            {
                return 0;
            }
            return renovations.Max(r => r.Id) + 1;
        }

        public void Create(RenovationSchedule entity)
        {
            renovations.Add(entity);
            renovationFileHandler.Save(renovations);
        }

        public void Update(RenovationSchedule entity)
        {
            int index = renovations.FindIndex(a => entity.Id == a.Id);
            if (index != -1)
            {
                renovations[index] = entity;
            }
            renovationFileHandler.Save(renovations);
        }

        public void Remove(RenovationSchedule entity)
        {
            renovations.Remove(entity);
            renovationFileHandler.Save(renovations);
        }

        public RenovationSchedule GetById(int key)
        {
            return renovations.Find(r => r.Id == key);
        }
    }
}
