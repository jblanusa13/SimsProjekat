using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectSims.WPF.ViewModel.OwnerViewModel
{
    public class StatisticsViewModel
    {
        public Owner Owner { get; set; }
        public TextBlock TitleTextBlock { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public StatisticsViewModel(Owner o, TextBlock titleTextBlock, Accommodation selectedAccommodetion)
        {
            Owner = o;
            TitleTextBlock = titleTextBlock;
            SelectedAccommodation = selectedAccommodetion;
        }
    }
}
