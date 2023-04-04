using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.ModelDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using ProjectSims.Observer;
using System.Globalization;
using ProjectSims.View;

namespace ProjectSims.Controller
{
    public class TourController
    {

        private TourDAO tours;
        private KeyPointDAO keyPointDAO;
        private KeyPointController keyPointController;

        public TourController()
        {
            tours = new TourDAO();
            keyPointDAO = new KeyPointDAO();
            keyPointController = new KeyPointController();
        }
        public List<Tour> GetAllTours()
        {
            return tours.GetAll();
        }
        public List<Tour> GetAllToursWithSameLocation(Tour t)
        {
            List<Tour> wantedTours = new List<Tour>();

            foreach(Tour tour in tours.GetAll())
            {
                if(tour.Location == t.Location && tour.Id != t.Id)
                {
                    wantedTours.Add(tour);
                }
            }

            return wantedTours;
        }
        public bool IsToday(Tour tour)
        {
            DateTime tourDate = tour.StartOfTheTour.Date;
            return (DateTime.Today == tourDate);
        }
        public bool IsInactive(Tour tour)
        {
            return (tour.State == TourState.Inactive);
        }
        public bool ExistsActiveTour()
        {
            foreach(Tour availableTour in GetAllTours())
            {
                if(availableTour.State == TourState.Active)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Tour> GetAvailableTours(int guideId)
        {
            List<Tour> availableTours = new List<Tour>();

            foreach (Tour tour in tours.GetAll())
            {
                if (IsInactive(tour) && IsToday(tour) && tour.GuideId == guideId)
                {
                    availableTours.Add(tour);
                }
            }

            return availableTours;
        }

        public List<Tour> GetScheduledTours(int guideId)
        {
            List<Tour> scheduledTours = new List<Tour>();

            foreach (Tour tour in tours.GetAll())
            {
                if (IsInactive(tour) && tour.GuideId == guideId)
                {
                    scheduledTours.Add(tour);
                }
            }

            return scheduledTours;
        }
        public List<KeyPoint> GetTourKeyPoints(Tour tour)
        {
            List<KeyPoint> allKeyPoints = keyPointDAO.GetAll();
            List<KeyPoint> tourKeyPoints = new List<KeyPoint>();
            foreach (KeyPoint keyPoint in allKeyPoints)
            {
                if (tour.KeyPointIds.Contains(keyPoint.Id))
                {
                    tourKeyPoints.Add(keyPoint);
                }
            }
            return tourKeyPoints;
        }
        public void Create(int idGuide, string name, string location, string description, string language, string maxNumberGuests, 
            string startKeyPoint, string finishKeyPoint, List<string> otherKeyPoints,
            string tourStart, string duration, string images)
        {
                List<int> keyPointIds = new List<int>();
                int startId = keyPointDAO.Add(new KeyPoint(-1, startKeyPoint, KeyPointType.First,false));
                keyPointIds.Add(startId);
                foreach (string keyPoint in otherKeyPoints)
                {
                    int otherKeyPointId = keyPointDAO.Add(new KeyPoint(-1, keyPoint, KeyPointType.Intermediate,false));
                    keyPointIds.Add(otherKeyPointId);
                }
                int finishId = keyPointDAO.Add(new KeyPoint(-1, finishKeyPoint, KeyPointType.Last,false));
                keyPointIds.Add(finishId);

                List<string> imageList = new List<string>();
                foreach (string image in images.Split(','))
                {
                    imageList.Add(image);
                }
                Tour newTour =  new Tour(-1, idGuide, name, location, description, language, Convert.ToInt32(maxNumberGuests), keyPointIds, DateTime.Parse(tourStart), Convert.ToDouble(duration), imageList, Convert.ToInt32(maxNumberGuests),TourState.Inactive,-1);
                tours.Add(newTour);
        }

        public bool StartTour(Tour tour)
        {
            if(!ExistsActiveTour())
            {
                tour.State = TourState.Active;
                tour.ActiveKeyPointId = tour.KeyPointIds.First();
                tours.Update(tour);
                return true;
            }            
             return false;           
        }
        public Tour FindStartedTour()
        {
            foreach (Tour availableTour in GetAllTours())
            {
                if (availableTour.State == TourState.Active)
                {
                    return availableTour;
                }
            }
            return null;
        }
        public void UpdateActiveKeyPoint(int tourId,int keyPointId)
        {
            Tour tour = GetAllTours().Find(t => t.Id == tourId);
            tour.ActiveKeyPointId = keyPointId;
            tours.Update(tour);
        }

        public int FindExpectedKeyPointId(Tour tour)
        {
            foreach (KeyPoint keyPoint in GetTourKeyPoints(tour))
            {
                if (keyPoint.Finished == false)
                {
                    return keyPoint.Id;
                }
            }
            return 0;
        }
        public bool StartsInLessThan48Hours(Tour tour)
        {
            return(tour.StartOfTheTour - DateTime.Now).TotalHours < 48;
        }
        public void FinishTour(Tour tour,List<Guest2> PresentGuests)
        {
            tour.State = TourState.Finished;
            tour.NumberOfPresentGuests = PresentGuests.Count;
            foreach(Guest2 guest2 in PresentGuests)
            {
                double days = (DateTime.Now - guest2.BirthDate).TotalDays;
                int age = Convert.ToInt32(days)/365;
                if(age<18)
                {
                    tour.NumberOfPresentGuestsUnder18++;
                }
                else if(age >=18 && age < 50)
                {
                    tour.NumberOfPresentGuestsBetween18And50++;
                }
                else
                {
                    tour.NumberOfPresentGuestsOver50++;
                }
            }
            tour.ActiveKeyPointId = -1;
            tours.Update(tour);
        }
        public void Delete(Tour tour)
        {
            tours.Remove(tour);
        }

        public void Update(Tour tour)
        {
            tours.Update(tour);
        }
        public void Subscribe(IObserver observer)
        {
            tours.Subscribe(observer);
        }

        public List<Tour> SearchTours(String location, double durationStart, double durationEnd, String language, int numberGuests)
        {
            List<Tour> tours = GetAllTours();
            List<Tour> wantedTours = new List<Tour>();
            foreach (Tour tour in tours)
            {
                if (CheckSearchConditions(tour, location, durationStart, durationEnd, language, numberGuests))
                {
                    wantedTours.Add(tour);
                }
            }
            return wantedTours;
        }

        public bool CheckSearchConditions(Tour tour, string location, double durationStart, double durationEnd, string language, int numberGuests)
        {
            bool ContainsLocation, ContainsLanguage, NumberGuestsIsLower, DurationBetween;

            ContainsLocation = (location == "") ? true : (tour.Location.ToLower()).Contains(location.ToLower());
            ContainsLanguage = (language == "") ? true : (tour.Language.ToLower()).Contains(language.ToLower());
            NumberGuestsIsLower = (numberGuests == -1) ? true : numberGuests <= tour.AvailableSeats;
            DurationBetween = (durationStart == -1) ? true : durationStart <= tour.Duration && tour.Duration <= durationEnd;

            return ContainsLocation && ContainsLanguage && NumberGuestsIsLower && DurationBetween;
        }

        //if text empty return -1
        //if text isn't integer or < 0 return -2
        public int ConvertToInt(String text)
        {
            int number;
            if (string.IsNullOrEmpty(text))
            {
                number = -1;
            }
            else if (!int.TryParse(text, out number))
            {
                MessageBox.Show("Wrong input! Number guests on tour must be a integer!");
                return -2;
            }
            else if (number < 0)
            {
                MessageBox.Show("The number of people on the tour can't be negative!");
                return -2;
            }

            return number;
        }

        //if text empty return -1
        //if text isn't double or < 0 return -2
        public double ConvertToDouble(String text)
        {
            double number;
            if (string.IsNullOrEmpty(text))
            {
                number = -1;
            }
            else if (!double.TryParse(text, out number))
            {
                MessageBox.Show("Wrong input! Duration tour must be a double!");
                return -2;
            }
            else if (number < 0)
            {
                MessageBox.Show("The tour duration search fields cannot have a negative value");
                return -2;
            }
            return number;
        }

       public Tour GetFinishedTourById(int id)
        {
            foreach (Tour tour in tours.GetAll())
            {
                if (tour.Id == id && IsFinished(tour))
                {
                    return tour;
                    
                }
            }
            return null;
        }

        public bool IsFinished(Tour tour)
        {
            return (tour.State == TourState.Finished);
        }

    }
}
