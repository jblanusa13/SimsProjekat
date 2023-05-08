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
        public List<Tuple<DateTime,DateTime>> GetAppointmentsByGuideIdAndDate(int id,DateOnly date)
        {
            List<Tuple<DateTime,DateTime>> appointments = new List<Tuple<DateTime, DateTime>>();
            foreach(var appointment in guideScheduleRepository.GetByGuideIdAndDate(id,date))
            {
                appointments.Add(new Tuple<DateTime, DateTime>(appointment.Start, appointment.End));
            }
            return appointments;
        }
        public List<Tuple<DateTime, DateTime>> GetFreeAppointmentsByDateAndGuideId(int id,DateOnly date)
        {
            List<Tuple<DateTime, DateTime>> appointments = GetAppointmentsByGuideIdAndDate(id,date).OrderBy(x=>x.Item1).ToList();
            List<Tuple<DateTime, DateTime>> freeAppointments = new List<Tuple<DateTime, DateTime>>();
            DateTime begin = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            if (appointments.Count != 0)
            {
                freeAppointments.Add(new Tuple<DateTime, DateTime>(begin, appointments.First().Item1));
                for (int i = 0; i < appointments.Count - 1; i++)
                {
                    freeAppointments.Add(new Tuple<DateTime, DateTime>(appointments[1].Item2, appointments[i + 1].Item1));

                }
                freeAppointments.Add(new Tuple<DateTime, DateTime>(appointments.Last().Item2, end));
            }
            else
            {
                freeAppointments.Add(new Tuple<DateTime, DateTime>(begin, end));
            }
            return freeAppointments;
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
