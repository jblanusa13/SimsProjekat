using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.WPF.View.GuideView.Pages;

namespace ProjectSims.WPF.ViewModel.GuideViewModel
{
    public class HomeViewModel: INotifyPropertyChanged
    {
        private TourService tourService;
        public NavigationService NavService { get; set; }
        public Guide Guide { get; set; }
        public Tour ActiveTour { get; set; }
        public RelayCommand StartTourCommand { get; set; }
        private string _imgSrc;
        public string ImgSrc
        {
            get => _imgSrc;
            set
            {
                if (value != _imgSrc)
                {
                    _imgSrc = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _labelText;
        public string LabelText
        {
            get => _labelText;
            set
            {
                if (value != _labelText)
                {
                    _labelText = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _showButton;
        public bool ShowButton
        {
            get => _showButton;
            set
            {
                if (value != _showButton)
                {
                    _showButton = value;
                    OnPropertyChanged();
                }
            }
        }
        public HomeViewModel(Guide guide,NavigationService navigationService)
        {
            NavService = navigationService;
            Guide = guide;
            tourService = new TourService();
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
            StartTourCommand = new RelayCommand(Execute_StartTourCommand);
            if (ActiveTour == null) 
            {
                ImgSrc = "/Resources/Images/Guide/wlppr.jpg";
                LabelText = "D o b r o d o s l i !";
                ShowButton = false;
            }
            else
            {
                ImgSrc = ActiveTour.Images.First();
                LabelText = "A k t i v n a   t u r a :" + System.Environment.NewLine+ ActiveTour.Name;
                ShowButton = true;
            }
        }
        private void Execute_StartTourCommand(object obj)
        {
            NavService.Navigate(new TourTrackingView(ActiveTour, Guide,NavService));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
