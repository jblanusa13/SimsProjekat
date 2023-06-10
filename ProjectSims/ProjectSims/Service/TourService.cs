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
using System.ComponentModel.Design;
using System.Windows.Automation.Peers;

namespace ProjectSims.Service
{
    public class TourService
    {
        private ITourRepository tourRepository;
        private IGuideRepository guideRepository;
        private IKeyPointRepository keyPointRepository;
        private ITourRequestRepository tourRequestRepository;
        private IRequestForComplexTourRepository requestForComplexTourRepository;
        private ReservationTourService reservationService;

        public TourService()
        {
            tourRepository = Injector.CreateInstance<ITourRepository>();
            guideRepository = Injector.CreateInstance<IGuideRepository>();
            keyPointRepository = Injector.CreateInstance<IKeyPointRepository>();
            tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            requestForComplexTourRepository = Injector.CreateInstance<IRequestForComplexTourRepository>();
            reservationService = new ReservationTourService();
            InitializeGuide();
            InitializeKeyPoints();
            InitializeTourRequest();
        }
        public void InitializeGuide()
        {
            foreach (var item in tourRepository.GetAll())
            {
                item.Guide = guideRepository.GetById(item.GuideId);
            }
        }
        public void InitializeKeyPoints()
        {
            foreach (var item in tourRepository.GetAll())
            {
                if(item.KeyPoints.Count() != 0)
                {
                    item.KeyPoints.Clear();
                }
               foreach(int id in item.KeyPointIds)
                {
                    item.KeyPoints.Add(keyPointRepository.GetById(id));
                }
                item.ActiveKeyPoint = keyPointRepository.GetById(item.ActiveKeyPointId);
            }        
        }
        public void InitializeTourRequest()
        {
            foreach (var item in tourRepository.GetAll())
            {
                item.TourRequest = tourRequestRepository.GetById(item.TourRequestId);
            }
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
        public List<Tuple<DateTime, DateTime>> GetScheduledAppointmentsByDateAndGuide(int guideId,DateTime date)
        {
            List<Tuple<DateTime, DateTime>> appointments = new List<Tuple<DateTime, DateTime>>();
            foreach (var tour in tourRepository.GetToursByStateAndGuideId(TourState.Inactive, guideId))
            {
                if(tour.StartOfTheTour.Date == date.Date)
                {
                    appointments.Add(new Tuple<DateTime, DateTime>(tour.StartOfTheTour, tour.StartOfTheTour.AddHours(tour.Duration)));
                }
            }
            return appointments.OrderBy(x => x.Item1).ToList();
        }
        public List<Tuple<DateTime, DateTime>> GetTimeSpanBetweenAppointmentsByDateAndGuide(int guideId,DateTime date)
        {
            List<Tuple<DateTime, DateTime>> freeAppointments = new List<Tuple<DateTime, DateTime>>();
            List<Tuple<DateTime, DateTime>> scheduledAppointments = GetScheduledAppointmentsByDateAndGuide(guideId,date);
            DateTime start = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            if(scheduledAppointments.Count != 0)
            {
                freeAppointments.Add(new Tuple<DateTime, DateTime> ( start, scheduledAppointments.First().Item1));
                for (int i = 0; i < scheduledAppointments.Count - 1; i++)
                {
                    freeAppointments.Add(new Tuple<DateTime, DateTime>(scheduledAppointments[1].Item2, scheduledAppointments[i + 1].Item1));
                }
                freeAppointments.Add(new Tuple<DateTime, DateTime>(scheduledAppointments.Last().Item2, end));
                return freeAppointments;
            }
            return new List<Tuple<DateTime, DateTime>>() { new Tuple<DateTime, DateTime>(start, end) };
        }
        public List<Tuple<DateTime, DateTime>> SplitTimeSpan(Tuple<DateTime,DateTime> timeSpan)
        {
            List<DateTime> hours = new List<DateTime>();
            List<DateTime> ends = new List<DateTime>();
            List<Tuple<DateTime,DateTime>> appointments = new List<Tuple<DateTime, DateTime>>(); 
            for (DateTime hour = timeSpan.Item1; hour != timeSpan.Item2; hour.AddMinutes(30))
            {
                hours.Add(hour);
            }
            for (int i = 0; i < hours.Count(); i++)
            {
                for (int j = i+1;j<hours.Count();j++)
                {
                    appointments.Add(new Tuple<DateTime, DateTime>(hours[i], hours[j]));
                }
            }
            return appointments;
        }
        public List<Tuple<DateTime, DateTime>> GetFreeAppointmentsByDateAndGuide(int guideId, DateTime date)
        {
            List<Tuple<DateTime, DateTime>> freeAppointments = new List<Tuple<DateTime, DateTime>>();
            foreach(var timeSpan in GetTimeSpanBetweenAppointmentsByDateAndGuide(guideId, date)){
                freeAppointments.AddRange(SplitTimeSpan(timeSpan));
            }
            return freeAppointments;         
        }
        public List<Tuple<DateTime, DateTime>> GetAvailableAppointmentsForPartOfComplexTour(int guideId, DateTime date,int partId)
        {
            List<Tuple<DateTime, DateTime>> freeAppointments = GetFreeAppointmentsByDateAndGuide(guideId,date);
            RequestForComplexTour complexRequest = requestForComplexTourRepository.GetBySimpleRequestId(partId);
            foreach(var simpleAcceptedRequest in complexRequest.TourRequests)
            {
                Tour tour = tourRepository.GetByRequestId(simpleAcceptedRequest.Id);
                if(tour != null)
                {
                    Tuple<DateTime,DateTime> unavailableAppointment = new Tuple<DateTime, DateTime>(tour.StartOfTheTour, tour.StartOfTheTour.AddHours(tour.Duration));
                    if (freeAppointments.Contains(unavailableAppointment))
                    {
                        freeAppointments.Remove(unavailableAppointment);
                    }
                }
            }
            return freeAppointments;
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

        public Tour GetMostVisitedTourLastMonth()
        {
            List<Tour> tours = new List<Tour>();
            foreach(var tour in tourRepository.GetAll())
            {
                if(tour.State == TourState.Finished && tour.StartOfTheTour > DateTime.Now.AddMonths(-1))
                {
                    tours.Add(tour);
                }
            }
            List<Tour> sortedTours = tours.OrderByDescending(t => t.MaxNumberGuests - t.AvailableSeats).ToList();
            return sortedTours.First();
        }

    }
}
