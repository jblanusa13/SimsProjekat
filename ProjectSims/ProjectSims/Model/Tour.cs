using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Descrption { get; set; }
        public string Language { get; set; }
        public int MaxNumberGuests { get; set; }
        public string KeyPoints { get; set; }
        public DateTime StartOfTheTour { get; set; }
        public int Duration { get; set; }
        public string Images { get; set; }


        public Tour() { }

        public Tour(int id, string name, string location, string descrption, string language, int maxNumberGuests, string keyPoints, DateTime startOfTheTour, int duration, string images)
        {
            Id = id;
            Name = name;
            Location = location;
            Descrption = descrption;
            Language = language;
            MaxNumberGuests = maxNumberGuests;
            KeyPoints = keyPoints;
            StartOfTheTour = startOfTheTour;
            Duration = duration;
            Images = images;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = values[2];
            Descrption = values[3];
            Language = values[4];
            MaxNumberGuests = Convert.ToInt32(values[5]);
            KeyPoints = values[6];
            StartOfTheTour = DateTime.Parse(values[7]);
            Duration = Convert.ToInt32(values[8]);
            Images = values[9];
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), Name, Location, Descrption, Language, MaxNumberGuests.ToString(), KeyPoints, StartOfTheTour.ToString(), Duration.ToString(), Images };
            return csvvalues;
        }
    }
}
