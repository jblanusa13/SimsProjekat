using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Service
{
    public class GuideScheduleService
    {
        private IGuideScheduleRepository guideScheduleRepository;

        public GuideScheduleService()
        {
            guideScheduleRepository = Injector.CreateInstance<IGuideScheduleRepository>();
        }
        public List<GuideSchedule> GetAll()
        {
            return guideScheduleRepository.GetAll();
        }
        public GuideSchedule GetById(int id)
        {
            return guideScheduleRepository.GetById(id);
        }
        public List<Tuple<DateTime,DateTime>> GetGuidesDailySchedule(int guideId,DateOnly date)
        {
            List<Tuple<DateTime,DateTime>> appointments = new List<Tuple<DateTime, DateTime>>();
            foreach(var appointment in guideScheduleRepository.GetByGuideIdAndDate(guideId, date))
            {
                appointments.Add(new Tuple<DateTime, DateTime>(appointment.Start, appointment.End));
            }
            return appointments.OrderBy(x => x.Item1).ToList();
        }
        public List<Tuple<DateTime, DateTime>> GetFreeAppointmentsForThatDay(int guideId, DateOnly date)
        {
            List<Tuple<DateTime, DateTime>> dailySchedule = GetGuidesDailySchedule(guideId, date);
            DateTime dayBegin = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime dayEnd = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            if (dailySchedule.Count != 0)
            {
                List<Tuple<DateTime, DateTime>> freeAppointments = new List<Tuple<DateTime, DateTime>>();
                freeAppointments.Add(new Tuple<DateTime, DateTime>(dayBegin, dailySchedule.First().Item1));
                for (int i = 0; i < dailySchedule.Count - 1; i++)
                {
                    freeAppointments.Add(new Tuple<DateTime, DateTime>(dailySchedule[1].Item2, dailySchedule[i + 1].Item1));
                }
                freeAppointments.Add(new Tuple<DateTime, DateTime>(dailySchedule.Last().Item2, dayEnd));
                return freeAppointments;
            }
            return new List<Tuple<DateTime, DateTime>>() { new Tuple<DateTime, DateTime>(dayBegin, dayEnd) };
        }
        public void Create(GuideSchedule guideSchedule)
        {           
            guideScheduleRepository.Create(guideSchedule);
        }

        public void Delete(GuideSchedule guideSchedule)
        {
            guideScheduleRepository.Remove(guideSchedule);
        }

        public void Update(GuideSchedule guideSchedule)
        {
            guideScheduleRepository.Update(guideSchedule);
        }

        public void Subscribe(IObserver observer)
        {
            guideScheduleRepository.Subscribe(observer);
        }
    }
}
