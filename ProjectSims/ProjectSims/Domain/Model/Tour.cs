using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public enum TourState { Inactive, Active, Finished, Cancelled}
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxNumberGuests { get; set; }
        public List<int> KeyPointIds { get; set; }
        public DateTime StartOfTheTour { get; set; }
        public double Duration { get; set; }
        public List<string> Images { get; set; }
        public int AvailableSeats { get; set; }
        public TourState State { get; set; }
        public int ActiveKeyPointId { get; set; }
        public Tour() 
        {
            KeyPointIds = new List<int>();
            Images = new List<string>();
        }

        public Tour(int id, int guideId, string name, string location, string description, string language, int maxNumberGuests,List<int> keyPointIds, DateTime startOfTheTour, double duration, List<String> images, int availableSeats, TourState state,int activeKeyPointId)
        {
            Id = id;
            GuideId = guideId;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxNumberGuests = maxNumberGuests;
            KeyPointIds = keyPointIds;
            StartOfTheTour = startOfTheTour;
            Duration = duration;
            Images = images;
            AvailableSeats = availableSeats;
            State = state;
            ActiveKeyPointId = activeKeyPointId;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuideId = Convert.ToInt32(values[1]);
            Name = values[2];
            Location = values[3];
            Description = values[4];
            Language = values[5];
            MaxNumberGuests = Convert.ToInt32(values[6]);
            foreach(string keyPoint in values[7].Split(","))
            {
                 int keyPointId = Convert.ToInt32(keyPoint);
                 KeyPointIds.Add(keyPointId);
            }         
            StartOfTheTour = DateTime.ParseExact(values[8], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            Duration = Convert.ToDouble(values[9]);
            foreach(string image in values[10].Split(","))
            {
                Images.Add(image);
            }
            AvailableSeats = Convert.ToInt32(values[11]);
            State = (TourState)Enum.Parse(typeof(TourState), values[12]);
            ActiveKeyPointId = Convert.ToInt32(values[13]);
        }

        public string[] ToCSV()
        {
            string KeyPointIdArray = "";
            foreach (int keyPointId in KeyPointIds)
            {
                if (keyPointId != KeyPointIds.Last())
                {
                    KeyPointIdArray += keyPointId.ToString() + ",";
                }
            }
            KeyPointIdArray += KeyPointIds.Last().ToString();

            string ImageString = "";
            foreach (string image in Images)
            {
                if (image != Images.Last())
                {
                    ImageString += image + ",";
                }
            }
            ImageString += Images.Last();

            string[] csvvalues = { Id.ToString(), GuideId.ToString(), Name, Location, Description, Language, MaxNumberGuests.ToString(), KeyPointIdArray, StartOfTheTour.ToString("dd/MM/yyyy HH:mm:ss"), Duration.ToString(), ImageString, AvailableSeats.ToString(),State.ToString(),ActiveKeyPointId.ToString()};
            return csvvalues;
        }
    }
}
