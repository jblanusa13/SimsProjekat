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
        public void Save(string name, string location, string description, string language, string maxNumberGuests, string startKeyPoint, string finishKeyPoint, List<string> otherKeyPoints, string tourStarts, string duration, string images)
        {
            List<string> keyPoints = new List<string>();
            keyPoints.Add(startKeyPoint);
            keyPoints.AddRange(otherKeyPoints);
            keyPoints.Add(finishKeyPoint);

            List<string> imageList = new List<string>();
            foreach (string image in images.Split(','))
            {
                imageList.Add(image);
            }


            foreach (string tourStart in tourStarts.Split(','))
            {
                tours.Save(name, location, description, language, Convert.ToInt32(maxNumberGuests), keyPoints, DateTime.Parse(tourStart), Convert.ToDouble(duration), imageList);
            }
        }

        public void Subscribe(IObserver observer)
        {
            tours.Subscribe(observer);
        }

        public List<Tour> SearchTours(String location, double durationStart, double durationEnd, String language, int numberGuests)
        {
            List<Tour> tours = GetAllTours();
            List<Tour> wantedTours = new List<Tour>();

            //durationStart and durationEnd always go together (both are -1 or both aren't -1)
            /*
            if (location != "")
            {
                if (durationStart != -1 && language == "" && numberGuests != -1)    //1
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.AvailableSeats >= numberGuests
                            && tour.Duration >= durationStart && tour.Duration <= durationEnd)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart != -1 && language == "" && numberGuests == -1)   //2
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.Duration >= durationStart
                            && tour.Duration <= durationEnd)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart != -1 && language != "" && numberGuests == -1)    //3
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.Duration >= durationStart
                            && tour.Duration <= durationEnd && (tour.Language.ToLower()).Contains(language.ToLower()))
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart != -1 && language != "" && numberGuests != -1)      //4
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.Duration >= durationStart && tour.Duration <= durationEnd
                            && (tour.Language.ToLower()).Contains(language.ToLower()) && tour.AvailableSeats >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart == -1 && language != "" && numberGuests != -1)      //5
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) &&
                           (tour.Language.ToLower()).Contains(language.ToLower()) && tour.AvailableSeats >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart == -1 && language == "" && numberGuests != -1)      //6
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.ToLower()).Contains(location.ToLower()) && tour.AvailableSeats >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart == -1 && language != "" && numberGuests == -1)         //7
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
                else if (durationStart == -1 && language == "" && numberGuests == -1)         //8
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
                if (durationStart != -1 && language == "" && numberGuests != -1)    //9
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.AvailableSeats >= numberGuests && tour.Duration >= durationStart && tour.Duration <= durationEnd)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart != -1 && language == "" && numberGuests == -1)   //10
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.Duration >= durationStart && tour.Duration <= durationEnd)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart != -1 && language != "" && numberGuests == -1)    //11
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.Duration >= durationStart && tour.Duration <= durationEnd
                            && (tour.Language.ToLower()).Contains(language.ToLower()))
                        {
                            wantedTours.Add(tour);
                        }
                    }
                    return wantedTours;
                }
                else if (durationStart != -1 && language != "" && numberGuests != -1)      //12
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.Duration >= durationStart && tour.Duration <= durationEnd
                            && (tour.Language.ToLower()).Contains(language.ToLower()) && tour.AvailableSeats >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }
                    return wantedTours;
                }
                else if (durationStart == -1 && language != "" && numberGuests != -1)      //13
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Language.ToLower()).Contains(language.ToLower()) && tour.AvailableSeats >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart == -1 && language == "" && numberGuests != -1)      //14
                {
                    foreach (Tour tour in tours)
                    {
                        if (tour.AvailableSeats >= numberGuests)
                        {
                            wantedTours.Add(tour);
                        }
                    }

                    return wantedTours;
                }
                else if (durationStart == -1 && language != "" && numberGuests == -1)         //15
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
            */

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

    }
}
