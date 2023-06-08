﻿using ProjectSims.Domain.Model;
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
        public TourRequest SelectedTourRequest { get; set; }
        public List<DateTime> AvailableDates;
        public List<DateTime> AvailableAppointments;
        public List<DateTime> AvailableDurations;

        public AcceptTourViewModel(TourRequest selectedTourRequest,ComboBox DateCombobox, ComboBox TimeCombobox, ComboBox DurationCombobox)
        {
            SelectedTourRequest = selectedTourRequest;
        }
    }
}
