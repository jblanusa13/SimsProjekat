using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model
{
    public enum TourRequestState { Waiting, Accepted, Invalid }
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public int Guest2Id { get; set; }
        public int GuideId { get; set; }
        public TourRequestState State { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxNumberGuests { get; set; }
        public DateOnly DateRangeStart { get; set; }
        public DateOnly DateRangeEnd { get; set; }
        public TourRequest()
        {
        }
        public TourRequest(int id,int guest2Id, int guideId, TourRequestState state, string location, string description, string language, int maxNumberGuests, DateOnly dateRangeStart, DateOnly dateRangeEnd)
        {
            Id = id;
            Guest2Id = guest2Id;
            GuideId = guideId;
            State = state;
            Location = location;
            Description = description;
            Language = language;
            MaxNumberGuests = maxNumberGuests;
            DateRangeStart = dateRangeStart;
            DateRangeEnd = dateRangeEnd;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest2Id = Convert.ToInt32(values[1]);
            GuideId = Convert.ToInt32(values[2]);
            State = (TourRequestState)Enum.Parse(typeof(TourRequestState), values[3]);
            Location = values[4];
            Description = values[5];
            Language = values[6];
            MaxNumberGuests = Convert.ToInt32(values[7]);
            DateRangeStart = DateOnly.ParseExact(values[8], "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateRangeEnd = DateOnly.ParseExact(values[9], "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), Guest2Id.ToString(), GuideId.ToString(), State.ToString(), Location, Description, Language, MaxNumberGuests.ToString(), DateRangeStart.ToString("MM/dd/yyyy"), DateRangeEnd.ToString("MM/dd/yyyy") };
            return csvvalues;
        }
    }
}
