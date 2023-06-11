using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private AppointmentFileHandler appointmentFileHandler;
        private List<Appointment> appointments;
        private readonly List<IObserver> observers;

        public AppointmentRepository()
        {
            appointmentFileHandler = new AppointmentFileHandler();
            appointments = appointmentFileHandler.Load();
            observers = new List<IObserver>();
        }
        public List<Appointment> GetAll()
        {
            return appointments;
        }
        public int NextId()
        {
            if (appointments.Count == 0)
            {
                return 0;
            }
            return appointments.Max(r => r.Id) + 1;
        }
        public void Create(Appointment appointment)
        {
            appointments.Add(appointment);
            appointmentFileHandler.Save(appointments);
            NotifyObservers();
        }

        public void Update(Appointment appointment)
        {
            int index = appointments.FindIndex(a => appointment.Id == a.Id);
            if (index != -1)
            {
                appointments[index] = appointment;
            }
            appointmentFileHandler.Save(appointments);
            NotifyObservers();
        }

        public void Remove(Appointment appointment)
        {
            appointments.Remove(appointment);
            appointmentFileHandler.Save(appointments);
            NotifyObservers();
        }

        public Appointment GetById(int key)
        {
            return appointments.Find(r => r.Id == key);
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
