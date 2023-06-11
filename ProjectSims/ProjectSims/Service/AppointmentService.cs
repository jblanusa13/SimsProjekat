using GalaSoft.MvvmLight.Command;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.WPF.ViewModel.GuideViewModel;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSims.Service
{
    public class AppointmentService
    {
        private IAppointmentRepository appointmentRepository;
        private GuideService guideService;
        private RequestForComplexTourService requestForComplexTourService;
      

        public AppointmentService()
        {
            appointmentRepository = Injector.CreateInstance<IAppointmentRepository>();
            guideService = new  GuideService();
            requestForComplexTourService = new RequestForComplexTourService();
            
        }
        public List<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }
        public List<Appointment> GetGuidesFreeAppointmentsOnDate(Guide guide,DateOnly date)
        {
            List<Appointment> freeAppointments = new List<Appointment>();
            foreach(var appointment in appointmentRepository.GetAll())
            {
                DateTime start = new DateTime(date.Year,date.Month,date.Day,appointment.Start.Hour,appointment.Start.Minute,0);
                DateTime end = new DateTime(date.Year, date.Month, date.Day, appointment.End.Hour, appointment.End.Minute, 0);
                if (guideService.CheckIfGuideIsAvailable(start, end, guide.Id))
                {
                    freeAppointments.Add(appointment);
                }
            }
            return freeAppointments;
        }
        public List<Appointment> GetAvailableAppointmentsForPartOfComplexTour(Guide guide, DateOnly date,TourRequest part)
        {
            List<Appointment> availableAppointments = new List<Appointment>();
            foreach (var appointment in GetGuidesFreeAppointmentsOnDate(guide,date))
            {
                DateTime start = new DateTime(date.Year, date.Month, date.Day, appointment.Start.Hour, appointment.Start.Minute, 0);
                DateTime end = new DateTime(date.Year, date.Month, date.Day, appointment.End.Hour, appointment.End.Minute, 0);
                if (requestForComplexTourService.CheckIfAppointmentIsAvailable(start, end,part))
                {
                    availableAppointments.Add(appointment);
                }
            }
            return availableAppointments;
        }
        public List<double> CalculateAvailableDurations(Guide guide, DateOnly date, TimeOnly start,TourRequest request)
        {
            List<Appointment> appointments = new List<Appointment>();
            if (request.RequestForComplexTour)
            {
                appointments = GetAvailableAppointmentsForPartOfComplexTour(guide, date, request);
            }
            else
            {
                appointments = GetGuidesFreeAppointmentsOnDate(guide, date);
            }
            List<Appointment> availableAppointmentsOnStart = appointments.Where(a=> (a.Start.Hour == start.Hour ) && (a.Start.Minute == start.Minute)).ToList();
            List<double> availableDurations = new List<double>();
            foreach(var availableAppointment in availableAppointmentsOnStart)
            {
                availableDurations.Add((availableAppointment.End - availableAppointment.Start).TotalHours);
            }
            return availableDurations;
        }

    }
}
