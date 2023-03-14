﻿using ProjectSims.Serializer;
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
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxNumberGuests { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public DateTime StartOfTheTour { get; set; }
        public double Duration { get; set; }
        public string Images { get; set; }
        public int AvailableSeats { get; set; }


        public Tour() 
        {
            KeyPoints = new List<KeyPoint>();
        }

        public Tour(int id, string name, string location, string description, string language, int maxNumberGuests, List<KeyPoint> keyPoints, DateTime startOfTheTour, double duration, string images, int availableSeats)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxNumberGuests = maxNumberGuests;
            KeyPoints = keyPoints;
            StartOfTheTour = startOfTheTour;
            Duration = duration;
            Images = images;
            AvailableSeats = availableSeats;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = values[2];
            Description = values[3];
            Language = values[4];
            MaxNumberGuests = Convert.ToInt32(values[5]);

            foreach(string keyPointName in values[6].Split(","))
            {
                 KeyPoints.Add(new KeyPoint(keyPointName, Id));
            }
            
            StartOfTheTour = DateTime.Parse(values[7]);
            Duration = Convert.ToDouble(values[8]);
            Images = values[9];
        }

        public string[] ToCSV()
        {
            string KeyPointNames="";
            foreach(KeyPoint keyPoint in KeyPoints)
            {
                if(keyPoint != KeyPoints.Last())
                {
                    KeyPointNames = KeyPointNames + keyPoint.Name + ",";
                }
            }
            KeyPointNames=KeyPointNames+KeyPoints.Last().Name;

            string[] csvvalues = { Id.ToString(), Name, Location, Description, Language, MaxNumberGuests.ToString(), KeyPointNames, StartOfTheTour.ToString(), Duration.ToString(), Images };
            return csvvalues;
        }
    }
}
