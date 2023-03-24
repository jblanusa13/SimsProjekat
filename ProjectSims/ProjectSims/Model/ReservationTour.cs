using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    public enum Guest2State { InactiveTour, Invited, Waiting, Present, NotPresent}
    public class ReservationTour : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int NumberGuest { get; set; }
        public int Guest2Id { get; set; }
        public Guest2State State { get; set; }

       

        public ReservationTour() { }

        public ReservationTour( int tourId, int numberGuest, int guest2Id)
        {
            TourId = tourId;
            NumberGuest = numberGuest;
            Guest2Id = guest2Id;
            State = Guest2State.InactiveTour;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            NumberGuest = Convert.ToInt32(values[2]);  
            Guest2Id = Convert.ToInt32(values[3]);
            State = (Guest2State)Enum.Parse(typeof(Guest2State), values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), TourId.ToString(), NumberGuest.ToString(), Guest2Id.ToString() ,State.ToString()};
            return csvvalues;
        }
    }
}
