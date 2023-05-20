using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public enum Guest2State { InactiveTour, ActiveTour, Waiting, Present, NotPresent}
    public class ReservationTour : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int NumberGuest { get; set; }
        public int Guest2Id { get; set; }
        public Guest2 Guest2 { get; set; }
        public Guest2State State { get; set; }    
        public int KeyPointWhereGuestArrivedId { get; set; }
        public KeyPoint KeyPointWhereGuestArrived { get; set; } 
        public bool UsedVoucher { get; set; }
        public bool RatedTour { get; set; }
        public int GuestAgeOnTour { get; set; }
        public ReservationTour() { }

        public ReservationTour( int tourId, int numberGuest, int guest2Id, int keyPointWhereGuestArrivedId, bool usedVoucher, bool ratedTour, int guestAgeOnTour)
        {
            TourId = tourId;
            NumberGuest = numberGuest;
            Guest2Id = guest2Id;
            State = Guest2State.InactiveTour;
            KeyPointWhereGuestArrivedId = keyPointWhereGuestArrivedId;
            UsedVoucher = usedVoucher;
            RatedTour = ratedTour;
            GuestAgeOnTour = guestAgeOnTour;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            NumberGuest = Convert.ToInt32(values[2]);  
            Guest2Id = Convert.ToInt32(values[3]);
            State = (Guest2State)Enum.Parse(typeof(Guest2State), values[4]);
            KeyPointWhereGuestArrivedId = Convert.ToInt32(values[5]);
            UsedVoucher = Convert.ToBoolean(values[6]);
            RatedTour = Convert.ToBoolean(values[7]);
            GuestAgeOnTour = Convert.ToInt32(values[8]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), TourId.ToString(), NumberGuest.ToString(), Guest2Id.ToString() ,
                State.ToString(), KeyPointWhereGuestArrivedId.ToString(), UsedVoucher.ToString(), RatedTour.ToString(),GuestAgeOnTour.ToString()};
            return csvvalues;
        }
    }
}
