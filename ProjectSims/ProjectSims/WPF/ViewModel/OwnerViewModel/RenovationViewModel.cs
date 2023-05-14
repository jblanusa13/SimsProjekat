using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class RenovationViewModel 
    {
        public Owner Owner { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<DateRanges> DateRanges { get; set; }
        public DateOnly FirstDate { get; set; }
        public DateOnly SecondDate { get; set; }
        public int Duration { get; set; }
        private RenovationService renovationService;
        private AccommodationScheduleService accommodationScheduleService;

        public RenovationViewModel(Accommodation selectedAccommodation, Owner owner) 
        {
            Owner = owner;
            SelectedAccommodation = selectedAccommodation;
            renovationService = new RenovationService();
            accommodationScheduleService = new AccommodationScheduleService();
            DateRanges = new ObservableCollection<DateRanges>();
        }

        public void CreateRenovation(DateRanges dateRange, string description)
        {
            renovationService.CreateRenovation(dateRange, description, SelectedAccommodation.Id, SelectedAccommodation);
        }

        public void FindAvailableDates(DateOnly firstDate, DateOnly secondDate, int duration)
        {
            FirstDate = firstDate;
            SecondDate = secondDate;
            Duration = duration;
            List<DateRanges> availableDates = new List<DateRanges>();
            availableDates = accommodationScheduleService.FindDates(firstDate, secondDate, duration, SelectedAccommodation.Id);
            Update(availableDates);
        }
        public void Update(List<DateRanges> availableDates)
        {
            DateRanges.Clear();
            foreach (DateRanges date in availableDates)
            {
                DateRanges.Add(date);
            }
        }
    }
}
