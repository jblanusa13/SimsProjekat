using ProjectSims.Domain.Model;
using ProjectSims.WPF.View.GuideView.Pages;
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
using System.Windows.Shapes;
using ProjectSims.Service;
using ProjectSims.Observer;
using System.Windows.Navigation;
using ProjectSims.WPF.ViewModel.GuideViewModel;

namespace ProjectSims.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuideStartingView.xaml
    /// </summary>
    public partial class GuideStartingView : Window, IObserver
    {
        public TourService tourService { get; set; }
        public Guide Guide { get; set; }
        public Tour ActiveTour { get; set; }
        public GuideStartingView(Guide g)
        {
            InitializeComponent();
            tourService = new TourService();
            Guide = g;
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
            GuideFrame.Content = new HomeView(Guide, GuideFrame.NavigationService);
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            GuideFrame.Content = new HomeView(Guide,GuideFrame.NavigationService);
            HelpButton.ToolTip = "Ukoliko imate aktivnu turu,bice prikazana na ovoj stranici!";
        }
        private void Account_Click(object sender, RoutedEventArgs e)
        {
            GuideFrame.Content = new GuideAccountView(Guide);
            HelpButton.ToolTip = "Prikazan je vas nalog sa osnovnim informacijama" + System.Environment.NewLine  + "Mozete proveriti " +
                "da li ste supervodic za neki jezik odabirom tog jezika u listi! " + System.Environment.NewLine + "Mozete se odjaviti ili  " +
                "dati tokaz ";
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void TrackTour_Click(object sender, RoutedEventArgs e)
        {
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
            if (ActiveTour != null)
            {
                Page tourTrackingPage = new TourTrackingView(ActiveTour,Guide,GuideFrame.NavigationService);
                GuideFrame.Content = tourTrackingPage;
                HelpButton.ToolTip = "Prikazana je trenutno aktivna tura." + System.Environment.NewLine + 
                "Kada prodjete kljucnu tacku,odaberite je kako bi ona bila oznacena kao zavrsena!" + System.Environment.NewLine +
                "Ukoliko se nista ne desava,znaci da ste preskocili kljucnu tacku,morate ici redom! " + System.Environment.NewLine + 
                "Prikazana je lista prisutnih gostiju i gostiju koji fale ." + System.Environment.NewLine +
                "Ako zelite da zavrsite turu, pritisnite na dugme za zavrsavanje ture (u gornjem desnom cosku)!";
            }
        }
        private void AvailableTours_Click(object sender, RoutedEventArgs e)
        {
            Page availableToursPage = new AvailableToursView(Guide);
            GuideFrame.Content = availableToursPage;
            HelpButton.ToolTip = "Prikazane su danasnje neaktivne ture." + System.Environment.NewLine +
               "Pritiskom na dugme 'zapocni turu' zapocecete odabranu turu i tada nijedna druga tura nece moci da se zapocne,dok ne zavrsite trenutno aktivnu";
        }
        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            Page createTourPage = new CreateTourView(Guide,null,null,null,new DateTime(0001,1,1),0);
            GuideFrame.Content = createTourPage;
            HelpButton.ToolTip = "Da biste kreirali turu,morate popuniti sva polja i moraju biti validno popunjena." + System.Environment.NewLine +
              "Mozete odabrati vise termina za pocetak,ali nakon unosa svakog termina,pritisnite na dugme za dodavanje pored" + System.Environment.NewLine +
              "Da biste uneli vise kljucnih tacaka,morate uneti pocetnu i krajnju.Za medjustanice potrebno je pritisnuti plus nakon unosa" + System.Environment.NewLine +
               "Da biste uneli slike,potrebno je da pritisnete plus,otvorice se fajl dijalog i odatle odaberite slike!" + System.Environment.NewLine +
               "Nakon sto ste sva polja validno popunili,mozete pritisnuti dugme za kreiranje ture!";
        }
        private void Suggestions_Click(object sender, RoutedEventArgs e)
        {
            Page suggestionsPage = new SuggestionsView(Guide);
            GuideFrame.Content = suggestionsPage;
            HelpButton.ToolTip = "Da biste kreirali turu spram najtrazenije lokacije/jezika potrebno je da kliknete na link!";
        }
        private void ScheduledTours_Click(object sender, RoutedEventArgs e)
        {
            GuideFrame.Content = new ScheduledToursView(Guide,GuideFrame.NavigationService);
            HelpButton.ToolTip = "Prikazane su vase zakazane ture.Za vise informacija o zakazanoj turi,pritisnite dugme za detalje!";
        }
        private void FinishedToursStatistics_Click(object sender, RoutedEventArgs e)
        {
            Page finishedToursStatisticsView = new FinishedToursStatisticsView(Guide, GuideFrame.NavigationService);
            GuideFrame.Content = finishedToursStatisticsView;
            HelpButton.ToolTip = "Prikazane su vase zavrsene ture.Prva tura je najposecenija ikad,druga je najposecenija ove godine!"+ System.Environment.NewLine +
                "Za vise informacija o statistici ture,pritisnite dugme za detalje!";
        }
        private void FinishedToursRatings_Click(object sender, RoutedEventArgs e)
        {
            Page finishedToursRatingsView = new FinishedToursRatingsView(Guide,GuideFrame.NavigationService);
            GuideFrame.Content = finishedToursRatingsView;
            HelpButton.ToolTip = "Prikazane su vase zavrsene ture.!" + System.Environment.NewLine +
               "Za vise informacija o ocenama ture,pritisnite dugme za detalje!";
        }
        private void TourRequests_Click(object sender, RoutedEventArgs e)
        {
            Page finishedTourRequestsView = new TourRequestsView(Guide);
            GuideFrame.Content = finishedTourRequestsView;
            HelpButton.ToolTip = "Prikazani su vam dostupni zahtevi za ture!" + System.Environment.NewLine +
               "Za vise informacija o zahtevu i prihvatanju zahteva,pritisnite dugme za detalje!" + System.Environment.NewLine +
               "Ako zelite da filtrirate zahteve,nakon sto popunite zeljene parametre za filtriranje,pritisnite dugme za pretragu!";
        }
        private void RequestStatistics_Click(object sender, RoutedEventArgs e)
        {
            Page requestStatisticsView = new RequestStatisticsView(Guide);
            GuideFrame.Content = requestStatisticsView;
            HelpButton.ToolTip = "Prikazana je statistika svih zahteva za ture!" + System.Environment.NewLine +
               "Ako odaberete zeljenu godinu,prikazace se statistika zahteva spram meseci u toj godini!";
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var startView = new MainWindow();
            startView.Show();
            Window.GetWindow(this).Close();
        }
        public void Update()
        {
            ActiveTour = tourService.GetTourByStateAndGuideId(TourState.Active, Guide.Id);
        }
    }
}
