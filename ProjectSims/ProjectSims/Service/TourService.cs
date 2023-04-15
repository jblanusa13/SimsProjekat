using ProjectSims.FileHandler;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;
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

namespace ProjectSims.Service
{
    public class TourService
    {
        private TourRepository tours;
        private KeyPointService keyPointService;
        private Guest2Service guestService;
        private ReservationTourService reservationService;
        public TourService()
        {
            tours = new TourRepository();
            keyPointService = new KeyPointService();
            guestService = new Guest2Service();
            reservationService = new ReservationTourService();
        }
        public List<Tour> GetAllTours()
        {
            return tours.GetAll();
        }
        public Tour GetTourById(int id)
        {
            return tours.GetTourById(id);
        }
        public List<Tour> GetToursByStateAndGuideId(TourState state, int guideId)
        {
            return tours.GetToursByStateAndGuideId(state,guideId);          
        }
        public Tour GetTourByStateAndGuideId(TourState state, int guideId)
        {
            return tours.GetTourByStateAndGuideId(state, guideId);
        }
        public List<KeyPoint> GetTourKeyPoints(Tour tour)
        {
            List<int> keyPointIds = tour.KeyPointIds;
            return keyPointIds.Select(id => keyPointService.GetKeyPointById(id)).ToList();
        }
        public List<Tour> GetTodayTours(int guideId)
        {
           return tours.GetTodayTours(guideId);
        }
       public Tour GetMostVisitedTour(int guideId,bool thisYear)
        {
            List<Tour> wantedTours = GetToursByStateAndGuideId(TourState.Finished, guideId);
            if (wantedTours.Count != 0)
            {
                if (thisYear)
                    wantedTours = wantedTours.Where(t => t.StartOfTheTour.Year == DateTime.Now.Year).ToList();
                Dictionary<int, int> numberOfGuestsOnTour = new Dictionary<int, int>();
                foreach (Tour t in wantedTours)
                {
                    numberOfGuestsOnTour.Add(t.Id, reservationService.GetNumberOfPresentGuests(t));
                }
                int mostVisitedTourId = numberOfGuestsOnTour.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                return GetTourById(mostVisitedTourId);
            }
            else
                return null;
        }
        public void Create(int guideId, string name, string location, string description, string language, string maxNumberGuests,string startKeyPointName, string finishKeyPointName, 
            List<string> otherKeyPointsNames, string tourStart, string duration, string images)
        {
                List<int> keyPointIds = new List<int>();
                int startKeyPointId = keyPointService.GetNextId();
                keyPointService.Create(startKeyPointId, startKeyPointName,KeyPointType.First);
                keyPointIds.Add(startKeyPointId);
                foreach (string keyPointName in otherKeyPointsNames)
                {
                    int otherKeyPointId = keyPointService.GetNextId();
                    keyPointService.Create(otherKeyPointId, keyPointName, KeyPointType.Intermediate);
                    keyPointIds.Add(otherKeyPointId);
                }
                int finishKeyPointId = keyPointService.GetNextId();
                keyPointService.Create(finishKeyPointId, finishKeyPointName, KeyPointType.Last);
                keyPointIds.Add(finishKeyPointId);
                List<string> imageList = new List<string>();
                foreach (string image in images.Split(','))
                {
                    imageList.Add(image);
                }
                Tour newTour =  new Tour(-1, guideId, name, location, description, language, Convert.ToInt32(maxNumberGuests), keyPointIds, DateTime.Parse(tourStart), Convert.ToDouble(duration), imageList, Convert.ToInt32(maxNumberGuests),TourState.Inactive,-1);
                tours.Create(newTour);
        }
        public void UpdateTourState(Tour tour,TourState state)
        {
            tour.State = state;
            tour.ActiveKeyPointId = (state == TourState.Active) ? tour.KeyPointIds.First() : -1;
            Update(tour);         
        }     
        public void UpdateActiveKeyPoint(Tour tour,int keyPointId)
        {
            tour.ActiveKeyPointId = keyPointId;
            Update(tour);
        }
        public void Remove(Tour tour)
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
        public List<Tour> GetAllToursWithSameLocation(Tour t)
        {
            List<Tour> wantedTours = new List<Tour>();

            foreach (Tour tour in tours.GetAll())
            {
                if (tour.Location == t.Location && tour.Id != t.Id)
                {
                    wantedTours.Add(tour);
                }
            }

            return wantedTours;
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
