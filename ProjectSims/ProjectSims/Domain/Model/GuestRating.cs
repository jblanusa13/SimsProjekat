using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public class GuestRating : ISerializable
    {
        public int Id { get; set; }
        public int CleanlinessRating { get; set; }
        public int RespectingRulesRating { get; set; }
        public int TidinessRating { get; set; }
        public int CommunicationRating { get; set; }
        public string Comment { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public DateOnly TimeStamp { get; set; }
        public int GuestId { get; set; }


        public GuestRating() { }

        public GuestRating(int id, int cleanlinessRating, int respectingRulesRating, int tidinessRating, int communicationRating, string comment, AccommodationReservation accommodationReservation, DateOnly timeStamp, int guestId)
        {
            Id = id;
            CleanlinessRating = cleanlinessRating;
            RespectingRulesRating = respectingRulesRating;
            TidinessRating = tidinessRating;
            CommunicationRating = communicationRating;
            Comment = comment;
            Reservation = accommodationReservation;
            TimeStamp = timeStamp;
            GuestId = guestId;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            CleanlinessRating = Convert.ToInt32(values[1]);
            RespectingRulesRating = Convert.ToInt32(values[2]);
            TidinessRating = Convert.ToInt32(values[3]);
            CommunicationRating = Convert.ToInt32(values[4]);
            Comment = values[5];
            TimeStamp = DateOnly.ParseExact(values[6], "dd.MM.yyyy");
            GuestId = Convert.ToInt32(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                CleanlinessRating.ToString(),
                RespectingRulesRating.ToString(),
                TidinessRating.ToString(),
                CommunicationRating.ToString(),
                Comment,
                TimeStamp.ToString("dd.MM.yyyy"),
                GuestId.ToString()};
            return csvValues;
        }
    }
}
