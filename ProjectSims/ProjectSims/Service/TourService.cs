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
using ProjectSims.Domain.RepositoryInterface;

namespace ProjectSims.Service
{
    public class TourService
    {
        private ITourRepository tourRepository;
        private KeyPointService keyPointService;
        private GuideScheduleService guideScheduleService;
        private ReservationTourService reservationService;
        public TourService()
        {
            tourRepository = Injector.CreateInstance<ITourRepository>();
            keyPointService = new KeyPointService();
            guideScheduleService = new GuideScheduleService();
            reservationService = new ReservationTourService();
        }
        public int NextId()
        {
            return tourRepository.NextId();
        }
        public List<Tour> GetAllTours()
        {
            return tourRepository.GetAll();
        }
        public Tour GetTourById(int id)
        {
            return tourRepository.GetById(id);
        }
        public List<Tour> GetToursByStateAndGuideId(TourState state, int guideId)
        {
            return tourRepository.GetToursByStateAndGuideId(state,guideId);          
        }
        public Tour GetTourByStateAndGuideId(TourState state, int guideId)
        {
            return tourRepository.GetTourByStateAndGuideId(state, guideId);
        }
        public List<KeyPoint> GetTourKeyPoints(Tour tour)
        {
            List<int> keyPointIds = tour.KeyPointIds;
            return keyPointIds.Select(id => keyPointService.GetKeyPointById(id)).ToList();
        }
        public List<Tour> GetToursByDateAndGuideId(DateTime date,int guideId)
        {
           return tourRepository.GetToursByDateAndGuideId(date,guideId);
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
       public void Create(Tour tour) 
       {
            tourRepository.Create(tour);
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
            tourRepository.Remove(tour);
        }
        public void Update(Tour tour)
        {
            tourRepository.Update(tour);
        }
        public void Subscribe(IObserver observer)
        {
            tourRepository.Subscribe(observer);
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

            foreach (Tour tour in tourRepository.GetAll())
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
                MessageBox.Show("Pogresan unos! Trajanje ture mora biti realan broj!");
                return -2;
            }
            else if (number < 0)
            {
                MessageBox.Show("Pogresan unos! Trajanje ture ne moze imati negativnu vrijednost!");
                return -2;
            }
            return number;
        }
        public Tour GetFinishedTourById(int id)
        {
            foreach (Tour tour in tourRepository.GetAll())
            {
                if (tour.Id == id && IsFinished(tour))
                {
                    return tour;

                }
            }
            return null;
        }
        public List<Tour> GetToursWhichFinishedWhereGuestPresent(int guest2Id)
        {
            List<Tour> toursFinished = new List<Tour>();
            ReservationTourService reservationTourService = new ReservationTourService();
            foreach (int id in reservationTourService.FindTourIdsWhereGuestPresent(guest2Id))
            {
                Tour tour = GetFinishedTourById(id);
                if (tour != null)
                {
                    toursFinished.Add(tour);
                }
            }
            return toursFinished;
        }
        public Tour GetActivatedTourById(int id)
        {
            foreach (Tour tour in tourRepository.GetAll())
            {
                if (tour.Id == id && IsActivated(tour))
                {
                    return tour;

                }
            }
            return null;
        }

        public List<Tour> GetToursWhichActivatedWhereGuestPresent(int guest2Id)
        {
            List<Tour> toursActivated = new List<Tour>();
            ReservationTourService reservationTourService = new ReservationTourService();
            foreach (int id in reservationTourService.FindTourIdsWhereGuestPresent(guest2Id))
            {
                Tour tour = GetActivatedTourById(id);
                if (tour != null)
                {
                    toursActivated.Add(tour);
                }
            }
            return toursActivated;
        }

        public bool IsFinished(Tour tour)
        {
            return (tour.State == TourState.Finished);
        }
        public bool IsActivated(Tour tour)
        {
            return (tour.State == TourState.Active);
        }

    }
}
