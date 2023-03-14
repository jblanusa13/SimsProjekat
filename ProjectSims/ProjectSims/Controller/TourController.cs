using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.ModelDAO;
using ProjectSims.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Controller
{
    public class TourController
    {
        private TourDAO tours;

        public TourController()
        {
            tours = new TourDAO();
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

        public void Create( Tour tour)
        {
            tours.Add(tour);
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

        public List<Tour> SearchTours(String location, int duration, String language, int numberGuests)
        {
            List<Tour> tours = GetAllTours();
            List<Tour> wantedTours = new List<Tour>();


            if (location != "")
            {
                if (duration != -1 && language == "" && numberGuests != -1)    //1
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.MaxNumberGuests >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration != -1 && language == "" && numberGuests == -1)   //2
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.Duration >= duration)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration != -1 && language != "" && numberGuests == -1)    //3
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.Duration >= duration
                            && (tour.Language.ToLower()).Contains(language.ToLower()))
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration != -1 && language != "" && numberGuests != -1)      //4
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.Duration >= duration
                            && (tour.Language.ToLower()).Contains(language.ToLower()) && tour.MaxNumberGuests >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration == -1 && language != "" && numberGuests != -1)      //5
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) &&
                           (tour.Language.ToLower()).Contains(language.ToLower()) && tour.MaxNumberGuests >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration == -1 && language == "" && numberGuests != -1)      //6
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.MaxNumberGuests >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration == -1 && language != "" && numberGuests == -1)         //7
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && (tour.Language.ToLower()).Contains(language.ToLower()))
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration == -1 && language == "" && numberGuests == -1)         //8
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()))
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
            }
            else if (location == "")
            {
                if (duration != -1 && language == "" && numberGuests != -1)    //9
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.MaxNumberGuests >= numberGuests && tour.Duration >= duration)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration != -1 && language == "" && numberGuests == -1)   //10
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.Duration >= duration)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration != -1 && language != "" && numberGuests == -1)    //11
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.Duration >= duration
                            && (tour.Language.ToLower()).Contains(language.ToLower()))
                        {
                            wantedTours.Add(tour);
                        }
                    }
                    return wantedTours;
                }
                else if (duration != -1 && language != "" && numberGuests != -1)      //12
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.Duration >= duration && (tour.Language.ToLower()).Contains(language.ToLower())
                            && tour.MaxNumberGuests >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }
                    return wantedTours;
                }
                else if (duration == -1 && language != "" && numberGuests != -1)      //13
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Language.ToLower()).Contains(language.ToLower()) && tour.MaxNumberGuests >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration == -1 && language == "" && numberGuests != -1)      //14
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.MaxNumberGuests >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (duration == -1 && language != "" && numberGuests == -1)         //15
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Language.ToLower()).Contains(language.ToLower()))
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
            }

            return wantedTours;

        }

    }
}
