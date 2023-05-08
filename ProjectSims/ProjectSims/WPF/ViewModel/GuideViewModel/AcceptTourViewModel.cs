using ProjectSims.Domain.Model;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class AcceptTourViewModel
    {
        private GuideService guideService;
        private GuideScheduleService guideScheduleService;
        public List<DateTime> daysBetween;
        public List<Tuple<DateTime,DateTime>> freeAppointments;
        public DateTime SelectedStartOfTheTour { get; set; }
        public TourRequest SelectedTourRequest { get; set; }
        public Guide Guide { get; set; }
        public AcceptTourViewModel(TourRequest selectedTourRequest,Guide guide)
        {
            guideService = new GuideService();
            guideScheduleService = new GuideScheduleService();
            Guide = guide;
            SelectedTourRequest = selectedTourRequest;
            daysBetween = new List<DateTime>();
            freeAppointments = new List<Tuple<DateTime, DateTime>>();
        }
        public void ShowAvailableDays(ComboBox DaysComboBox)
        {
            for (DateOnly day = SelectedTourRequest.DateRangeStart; day <= SelectedTourRequest.DateRangeEnd; day = day.AddDays(1))
            {
                DaysComboBox.Items.Add(day);
            }
        }
        public void ShowAvailableAppointments(DateOnly selectedDate,ComboBox AppointmentsComboBox)
        {
            foreach (var freeappointment in guideScheduleService.GetFreeAppointmentsByDateAndGuideId(Guide.Id, selectedDate))
            {
                AppointmentsComboBox.Items.Add(freeappointment);
            }
        }

    }
}
