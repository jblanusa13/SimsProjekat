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

        public bool IsAvailableRequestedDate(Request request, DateOnly firstDate, DateOnly lastDate)
        {
            AccommodationReservationRepository reservationRepository = new AccommodationReservationRepository();

            int days = reservationRepository.GetById(request.ReservationId).CheckOutDate.DayNumber - reservationRepository.GetById(request.ReservationId).CheckInDate.DayNumber;

            //Requested: 12.03, Vacation days: 5, Reserved: 15.03-19.03.
            for (int i = 0; i < days; i++)
            {
                if (request.ChangeDate.AddDays(i) >= firstDate && request.ChangeDate.AddDays(i) <= lastDate)
                {
                    return true;
                }
            }
            return false;
        }

        public List<DateRanges> FindUnavailableDatesForRequest(Request request)
        {
            List<DateRanges> unavailableDates = new List<DateRanges>();
            AccommodationReservationRepository reservationRepository = new AccommodationReservationRepository();

            foreach (AccommodationReservation reservation in reservationRepository.GetAll())
            {
                if (reservation.AccommodationId == reservationRepository.GetById(request.ReservationId).AccommodationId
                    && reservation.State == ReservationState.Active
                    && reservation.Id != request.ReservationId)
                {
                    unavailableDates.Add(new DateRanges(reservation.CheckInDate, reservation.CheckOutDate));
                }
            }
            return unavailableDates;
        }

        public AccommodationSchedule GetById(int key)
        {
            return schedules.Find(r => r.Id == key);
        }

        public void AddUnavailableDate(AccommodationSchedule schedule, DateRanges dateRange)
        {
            DateRanges helpVariable;

            for (int i = 0; i < schedule.UnavailableDates.Count; i++)
            {
                if (dateRange.CheckIn < schedule.UnavailableDates[i].CheckIn)
                {
                    helpVariable = schedule.UnavailableDates[i];
                    schedule.UnavailableDates[i] = dateRange;
                    dateRange = helpVariable;
                }
            }
            schedule.UnavailableDates.Add(dateRange);
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
