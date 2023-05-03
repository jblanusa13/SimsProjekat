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
        public List<DateTime> daysBetween = new List<DateTime>();
        public List<DateTime> availableAppointments = new List<DateTime>();
        public DateTime SelectedStartOfTheTour { get; set; }
        public TourRequest SelectedTourRequest { get; set; }
        public Guide Guide { get; set; }
        public AcceptTourViewModel(TourRequest selectedTourRequest,Guide guide)
        {
            guideService = new GuideService();
            Guide = guide;
            SelectedTourRequest = selectedTourRequest;
            daysBetween = new List<DateTime>();
        }
        public void ShowAvailableDays(ComboBox DaysComboBox)
        {
            for (DateOnly day = SelectedTourRequest.DateRangeStart; day <= SelectedTourRequest.DateRangeEnd; day = day.AddDays(1))
            {
                DaysComboBox.Items.Add(day);
            }
        }
        public void ShowAvailableTimes(DateOnly selectedDay,ComboBox AppointmentsComboBox, double Duration)
        {
           // availableAppointments 
            for (DateOnly day = SelectedTourRequest.DateRangeStart; day <= SelectedTourRequest.DateRangeEnd; day = day.AddDays(1))
            {
                AppointmentsComboBox.Items.Add(day);
            }
        }

    }
}
