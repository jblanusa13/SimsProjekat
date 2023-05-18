using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;

namespace ProjectSims.Repository
{
    public class AccommodationScheduleRepository : IAccommodationScheduleRepository
    {
        private AccommodationScheduleFileHandler scheduleFileHandler;
        private List<AccommodationSchedule> schedules;

        public AccommodationScheduleRepository()
        {
            scheduleFileHandler = new AccommodationScheduleFileHandler();
            schedules = scheduleFileHandler.Load();
        }

        public List<AccommodationSchedule> GetAll()
        {
            return schedules;
        }

        public List<DateRanges> GetUnavailableDates(int accommodationId)
        {
            AccommodationSchedule schedule = schedules.Find(s => s.Id == accommodationId);
            return schedule.UnavailableDates;
        }

        public AccommodationSchedule GetById(int key)
        {
            return schedules.Find(r => r.Id == key);
        }

        public int NextId()
        {
            if (schedules.Count == 0)
            {
                return 0;
            }
            return schedules.Max(r => r.Id) + 1;
        }

        public void Create(AccommodationSchedule entity)
        {
            entity.Id = NextId();
            schedules.Add(entity);
            scheduleFileHandler.Save(schedules);
        }

        public void Remove(AccommodationSchedule entity)
        {
            schedules.Remove(entity);
            scheduleFileHandler.Save(schedules);
        }

        public void Update(AccommodationSchedule entity)
        {
            int index = schedules.FindIndex(r => entity.Id == r.Id);
            if (index != -1)
            {
                schedules[index] = entity;
            }
            scheduleFileHandler.Save(schedules);
        }
    }   
}
