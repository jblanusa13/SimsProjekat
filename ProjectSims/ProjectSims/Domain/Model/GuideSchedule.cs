using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model
{
    public class GuideSchedule : ISerializable
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public int TourId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public GuideSchedule()
        {
        }
        public GuideSchedule(int id,int guideId, int tourId, DateTime start, DateTime end)
        {
            Id = id;
            GuideId = guideId;
            TourId = tourId;
            Start = start;
            End = end;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuideId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            Start = DateTime.ParseExact(values[3], "dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture);
            End = DateTime.ParseExact(values[4], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), GuideId.ToString(), TourId.ToString(), Start.ToString("dd/MM/yyyy HH:mm"), End.ToString("dd/MM/yyyy HH:mm") };
            return csvvalues;
        }
    }
}