using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSims.WPF.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for SuggestionsView.xaml
    /// </summary>
    public partial class SuggestionsView : Page
    {
        public string MostWantedLanguage { get; set; }
        private TourRequestService tourRequestService;
        public SuggestionsView()
        {
            InitializeComponent();
            DataContext = this;
            tourRequestService = new TourRequestService();
            MostWantedLanguage = tourRequestService.GetMostWantedLanguageInLastYear();
        }
    }
}
