using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Serializer;

namespace ProjectSims.Model
{
    class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int GuestNumber { get; set; }

        public AccommodationReservation() { }

        public AccommodationReservation(int accommodationId, DateOnly checkInDate, DateOnly checkOutDate, int guestNumber)
        {
            AccommodationId = accommodationId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            GuestNumber = guestNumber;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            CheckInDate = DateOnly.Parse(values[2]);
            CheckOutDate = DateOnly.Parse(values[3]);
            GuestNumber = Convert.ToInt32(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { 
                Id.ToString(), 
                AccommodationId.ToString(),
                CheckInDate.ToString(),
                CheckOutDate.ToString(),
                GuestNumber.ToString()  
            };
            return csvvalues;
        }
    }
}
