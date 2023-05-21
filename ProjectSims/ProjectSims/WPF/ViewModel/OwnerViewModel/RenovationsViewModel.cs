using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class RenovationsViewModel : IObserver, INotifyPropertyChanged
    {
        public Owner Owner { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        private RenovationScheduleService renovationService;
        public RenovationSchedule SelectedRenovation { get; set; }
        public ObservableCollection<RenovationSchedule> RenovationList { get; set; }
        public ObservableCollection<int> DurationList { get; set; }
        public DateOnly FirstDate { get; set; }
        public DateOnly SecondDate { get; set; }
        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        public RenovationsViewModel(RenovationSchedule selectedRenovation, Owner owner) 
        {
            Owner = owner;
            SelectedRenovation = selectedRenovation;
            renovationService = new RenovationScheduleService();
            renovationService.Subscribe(this);
            RenovationList = new ObservableCollection<RenovationSchedule>(renovationService.GetPassedAndFutureRenovationsByOwner(Owner.Id));
            SetDurationForEachRenovation();
        }

        public void SetDurationForEachRenovation()
        {
            foreach (var renovation in renovationService.GetPassedAndFutureRenovationsByOwner(Owner.Id))
            {
                renovation.Duration = renovationService.CalculateDurationForRenovation(renovation);
            }
        }

        public void QuitRenovation(RenovationSchedule renovation)
        {
            if (renovation != null)
            {
                if (renovationService.CanQuitRenovation(renovation))
                {
                    renovationService.RemoveRenovation(renovation);
                    Update();
                }
                else
                {
                    MessageBox.Show("Rok za otkazivanje je prošao, ne možete otkazati renovaciju");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Update()
        {
            RenovationList.Clear();
            foreach (RenovationSchedule renovation in renovationService.GetPassedAndFutureRenovationsByOwner(Owner.Id))
            {
                RenovationList.Add(renovation);
            }
        }
    }
}
