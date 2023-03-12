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

        public ReservationTour() { }

        public ReservationTour( int tourId, int numberGuest)
        {
            TourId = tourId;
            NumberGuest = numberGuest;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            NumberGuest = Convert.ToInt32(values[2]);   
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), TourId.ToString(), NumberGuest.ToString() };
            return csvvalues;
        }
    }
}
