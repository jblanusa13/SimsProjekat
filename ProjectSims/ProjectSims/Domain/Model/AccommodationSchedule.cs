using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public class AccommodationSchedule
    {
        public int Id { get; set; }
        public List<DateRanges> UnavailableDates { get; set; }

        public AccommodationSchedule()
        {
            UnavailableDates = new List<DateRanges>();
        }

        public AccommodationSchedule(int id, List<DateRanges> unavailableDates)
        {
            Id = id;
            UnavailableDates = unavailableDates;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            List<DateRanges> unavailableDates = new List<DateRanges>();
            foreach (string value in values[1].Split(","))
            {
                unavailableDates.Add(new DateRanges(DateOnly.ParseExact(value.Split("-")[0], "dd.MM.yyyy"), DateOnly.ParseExact(value.Split("-")[1], "dd.MM.yyyy")));
            }
            UnavailableDates = unavailableDates;
        }

        public string[] ToCSV()
        {
            string UnavailableDatesString = "";
            foreach (DateRanges range in UnavailableDates)
            {
                if (range != UnavailableDates.Last())
                {
                    UnavailableDatesString += range.CheckIn.ToString() + "-" + range.CheckOut.ToString() + ",";
                }
            }
            UnavailableDatesString += UnavailableDates.Last().CheckIn + "-" + UnavailableDates.Last().CheckOut;
            string[] csvValues = {
                Id.ToString(),
                UnavailableDatesString
            };
            return csvValues;
        }
    }
}
