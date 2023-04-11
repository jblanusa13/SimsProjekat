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
        public int Rating { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public DateOnly TimeStamp { get; set; }
        public Guest1 Guest { get; set; }

        public GuestRating() { }

        public GuestRating(int id, int rating, AccommodationReservation accommodationReservation, DateOnly timeStamp, Guest1 guest)
        {
            Id = id;
            Rating = rating;
            Reservation = accommodationReservation;
            TimeStamp = timeStamp;
            Guest = guest;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Rating = Convert.ToInt32(values[1]);
            TimeStamp = DateOnly.Parse(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Rating.ToString(), TimeStamp.ToString() };
            return csvValues;
        }
    }
}
