using ProjectSims.Domain.Model;
using ProjectSims.Observer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class SuggestionsViewModel
    {
        private TourRequestService tourRequestService;
        public string MostWantedLanguage { get; set; }
        public string MostWantedLocation { get; set; }
        public Guide Guide { get; set; }
        public SuggestionsViewModel(Guide g)
        { 
            tourRequestService = new TourRequestService();
            if (tourRequestService.GetMostWantedLanguageInLastYear() != null)
            {
                MostWantedLanguage = tourRequestService.GetMostWantedLanguageInLastYear().ToUpperInvariant();
            }
            else
            {
                MostWantedLanguage = "Trenutno ne postoji jezik sa najvecim brojem zahteva!";
            }
            if (tourRequestService.GetMostWantedLocationInLastYear() != null)
            {
                MostWantedLocation = tourRequestService.GetMostWantedLocationInLastYear().ToUpperInvariant();
            }
            else
            {
                MostWantedLocation = "Trenutno ne postoji lokacija sa najvecim brojem zahteva!";
            }
        }
        public string GetMostWantedLanguage()
        {
            return MostWantedLanguage;
        }
        public string GetMostWantedLocation()
        {
            return MostWantedLocation;
        }

    }
}
