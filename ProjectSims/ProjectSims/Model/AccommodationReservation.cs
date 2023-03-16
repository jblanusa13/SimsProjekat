using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Serializer;

namespace ProjectSims.Model
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public int GuestId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int GuestNumber { get; set; }

        public AccommodationReservation() { }

        public AccommodationReservation(int id, int guestId, int accommodationId, DateOnly checkInDate, DateOnly checkOutDate, int guestNumber)
        {
            Id = id;
            GuestId = guestId;
            AccommodationId = accommodationId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            GuestNumber = guestNumber;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            CheckInDate = DateOnly.Parse(values[3]);
            CheckOutDate = DateOnly.Parse(values[4]);
            GuestNumber = Convert.ToInt32(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { 
                Id.ToString(), 
                GuestId.ToString(),
                AccommodationId.ToString(),
                CheckInDate.ToString(),
                CheckOutDate.ToString(),
                GuestNumber.ToString()  
            };
            return csvvalues;
        }
    }
}
