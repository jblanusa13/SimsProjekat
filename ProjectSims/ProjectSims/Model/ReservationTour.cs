using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    public class ReservationTour : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int NumberGuest { get; set; }
        public int Guest2Id { get; set; }

        public ReservationTour() { }

        public ReservationTour( int tourId, int numberGuest, int guest2Id)
        {
            TourId = tourId;
            NumberGuest = numberGuest;
            Guest2Id = guest2Id;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            NumberGuest = Convert.ToInt32(values[2]);  
            Guest2Id = Convert.ToInt32(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), TourId.ToString(), NumberGuest.ToString(), Guest2Id.ToString() };
            return csvvalues;
        }
    }
}
