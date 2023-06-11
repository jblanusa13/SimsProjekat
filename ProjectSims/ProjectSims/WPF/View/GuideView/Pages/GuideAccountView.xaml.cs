using ProjectSims.Domain.Model;
using ProjectSims.Service;
using ProjectSims.View.GuideView;
using ProjectSims.WPF.View.Guest1View;
using ProjectSims.WPF.ViewModel.GuideViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for GuideAccountView.xaml
    /// </summary>
    public partial class GuideAccountView : Page
    {
        private TourService tourService { get; set; }
        private GuideService guideService { get; set; }
        private Guest2Service guest2Service { get; set; }
        private UserService userService { get; set; }
        private ReservationTourService reservationTourService { get; set; }
        public Guide Guide { get; set; }
        public List<string> Languages { get; set; }
        public string SelectedLanguage { get; set; }
        public GuideAccountView(Guide guide)
        {
            InitializeComponent();
            DataContext = this;
            Guide = guide;
            tourService = new TourService();
            reservationTourService = new ReservationTourService();
            guest2Service = new Guest2Service();
            userService  = new UserService();   
            guideService= new GuideService();
            Languages = guideService.GetGuidesLanguages(guide.Id);
        }
        public void Dismissal_Click(object sender, RoutedEventArgs e)
        {
            List<Tour> scheduledTours = tourService.GetToursByStateAndGuideId(TourState.Inactive, Guide.Id);
            List<Guest2> guests = reservationTourService.GetGuestsForScheduledToursByGuideId(Guide.Id);
            if (scheduledTours.Count > 0)
            {
                foreach (var scheduledTour in scheduledTours)
                {
                    scheduledTour.State = TourState.Cancelled;
                    tourService.Update(scheduledTour);
                }
            }
            if (guests.Count > 0)
            {
                foreach (var guest in guests)
                {
                    guest2Service.GiveVoucher(guest.Id, 2);
                }
            }
            userService.Delete(Guide.User);
            guideService.Delete(Guide);
            var startView = new MainWindow();
            startView.Show();
            Window.GetWindow(this).Close();
        }
    
        public void Logout_Click(object sender, RoutedEventArgs e)
        {

        }
        public void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (guideService.IsSuperGuideForLanguage(SelectedLanguage, Guide.Id))
            {
                Label1.Content = "Vi ste supervodic za " + SelectedLanguage.ToString() + " jezik";
                TextBlock1.Text = "Imate " + guideService.GetSuperguideState(SelectedLanguage, Guide.Id).ToString() + " tura na ovom jeziku ciji je prosek iznad 9!";
              //  StatusImage.Source = new BitmapImage(new Uri(@"/Resources/Icons/Guide/duration.png"));

            }
            else
            {
                Label1.Content = "Vi niste supervodic za " + SelectedLanguage.ToString() + " jezik";
                TextBlock1.Text = "Imate " + guideService.GetSuperguideState(SelectedLanguage, Guide.Id).ToString() + " tura na ovom jeziku ciji je prosek iznad 9!";
               // StatusImage.Source = new BitmapImage(new Uri(@"/Resources/Icons/Guide/superGuide.png"));
            }

        }
    }
}

