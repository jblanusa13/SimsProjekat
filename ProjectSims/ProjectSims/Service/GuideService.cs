using ProjectSims.Domain.Model;
using ProjectSims.Repository;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Serializer;
using System.Windows.Input;
using Syncfusion.Windows.Shared;

namespace ProjectSims.Service
{
    public class GuideService
    {
        private IGuideRepository guideRepository;
        private ITourRepository tourRepository;
        private IUserRepository userRepository;
        private ITourRatingRepository tourRatingRepository;

        public GuideService()
        {
            guideRepository = Injector.CreateInstance<IGuideRepository>();
            userRepository = Injector.CreateInstance<IUserRepository>();
            tourRatingRepository = Injector.CreateInstance<ITourRatingRepository>();
            tourRepository = Injector.CreateInstance<ITourRepository>();
        }
        private void InitializeUser()
        {
            foreach(var item in guideRepository.GetAll())
            {
                item.User = userRepository.GetById(item.UserId);
            }
        }
        public Guide GetGuideByUserId(int userId)
        {
            return guideRepository.GetByUserId(userId);
        }

        public List<Guide> GetAllGuides()
        {
            return guideRepository.GetAll();
        }
        public Guide GetGuideById(int id)
        {
            return guideRepository.GetById(id);
        }
        public List<String> GetGuidesLanguages(int id)
        {
            List<Tour> guideFinishedTours = tourRepository.GetToursByStateAndGuideId(TourState.Finished,id);
            if(guideFinishedTours != null)
            {
                List<String> guideLanguages = guideFinishedTours.Select(t => t.Language).ToList();
                return guideLanguages.Distinct().ToList();
            }
            return null;
        }
        public List<Tuple<DateTime, DateTime>> GetGuidesDailySchedule(int id, DateTime date)
        {
            List<Tuple<DateTime, DateTime>> appointments = new List<Tuple<DateTime, DateTime>>();
            foreach (var tour in tourRepository.GetToursByStateAndGuideId(TourState.Inactive, id))
            {
                if (tour.StartOfTheTour.Date == date.Date)
                {
                    appointments.Add(new Tuple<DateTime, DateTime>(tour.StartOfTheTour, tour.StartOfTheTour.AddHours(tour.Duration)));
                }
            }
            return appointments.OrderBy(x => x.Item1).ToList();
        }
        public List<Tuple<DateTime, DateTime>> GetGuidesFreeAppointmentsByDate(int guideId, DateTime date)
        {
            List<Tuple<DateTime, DateTime>> dailySchedule = GetGuidesDailySchedule(guideId, date);
            DateTime dayBegin = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime dayEnd = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            if (dailySchedule.Count != 0)
            {
                List<Tuple<DateTime, DateTime>> freeAppointments = new List<Tuple<DateTime, DateTime>>();
                freeAppointments.Add(new Tuple<DateTime, DateTime>(dayBegin, dailySchedule.First().Item1));
                for (int i = 0; i < dailySchedule.Count - 1; i++)
                {
                    freeAppointments.Add(new Tuple<DateTime, DateTime>(dailySchedule[1].Item2, dailySchedule[i + 1].Item1));
                }
                freeAppointments.Add(new Tuple<DateTime, DateTime>(dailySchedule.Last().Item2, dayEnd));
                return freeAppointments;
            }
            return new List<Tuple<DateTime, DateTime>>() { new Tuple<DateTime, DateTime>(dayBegin, dayEnd) };
        }
        public bool CheckIfGuideIsAvailable(DateTime start,DateTime end,int id)
        {
            foreach (var freeAppointment in GetGuidesFreeAppointmentsByDate(id, start.Date))
            {
                if ((start >= freeAppointment.Item1) && (start <= freeAppointment.Item2) && (end <= freeAppointment.Item2) && (end <= freeAppointment.Item2))
                    return true;
            }
            return false;
        }
        public List<String> GetLanguagesOfGuideFinishedTours(int id)
        {
            List<Tour> guideFinishedTours = tourRepository.GetToursByStateAndGuideId(TourState.Finished, id);
            List<String> guideLanguages = guideFinishedTours.Select(t => t.Language).ToList();
            return guideLanguages.Distinct().ToList();
        }
        public List<Tour> GetGuidesToursInLastYearByLanguage(string language, int id)
        {
            List<Tour> toursOnLanguage = tourRepository.GetToursByLanguageAndGuideId(language, id);
            return toursOnLanguage.Where(t => (DateTime.Now - t.StartOfTheTour).TotalDays <= 365).ToList();
        }
        public double CalculateAverageLanguageRatingOnTour(Tour tour)
        {
            List<TourAndGuideRating> tourAndGuideratings = tourRatingRepository.GetAllRatingsByTour(tour);
            if(tourAndGuideratings.Count > 0)
            {
                List<int> languageRatings = tourAndGuideratings.Select(r => r.LanguageGuide).ToList();
                return languageRatings.Average();
            }
            return 0;
        }
        public int GetSuperguideState(string language,int id)
        {
            List<Tour> tours = GetGuidesToursInLastYearByLanguage(language, id);
            List<Tour> wantedTours = tours.Where(t => (CalculateAverageLanguageRatingOnTour(t) > 4)).ToList();
            return wantedTours.Count();
        }
        public bool IsSuperGuideForLanguage(string language, int id)
        {
            return GetSuperguideState(language, id) > 20;
        }
        public List<Tour> SortToursBySuperGuides(List<Tour> allTours)
        {
            List<Tour> superGuidesTours = allTours.Where(t=> IsSuperGuideForLanguage(t.Language,t.GuideId)).ToList();
            List<Tour> otherGuidesTours = allTours.Where(t => !IsSuperGuideForLanguage(t.Language, t.GuideId)).ToList();
            superGuidesTours.AddRange(otherGuidesTours);
            return superGuidesTours;
        }
        public void Create(Guide guide)
        {
            guideRepository.Create(guide);
        }

        public void Delete(Guide guide)
        {
            guideRepository.Remove(guide);
        }

        public void Update(Guide guide)
        {
            guideRepository.Update(guide);
        }

        public void Subscribe(IObserver observer)
        {
            guideRepository.Subscribe(observer);
        }
    }
}
