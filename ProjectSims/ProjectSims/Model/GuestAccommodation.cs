using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    public class GuestAccommodation : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public string Name { get; set; }
        public AccommodationType Type { get; set; }
        public int GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public bool Rated { get; set; }
        public GuestAccommodation() { }
        public GuestAccommodation(int id, int accommodationId, string name, AccommodationType type, int guestId, string firstName, string lastName, DateOnly checkInDate, DateOnly checkOutDate, bool rated) 
        {
            Id = id;
            AccommodationId = accommodationId;
            Name = name;
            Type= type;
            GuestId = guestId;
            FirstName = firstName;
            LastName = lastName;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Rated = rated;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            Name = Convert.ToString(values[2]);
            Type = Enum.Parse<AccommodationType>(values[3]);
            GuestId = Convert.ToInt32(values[4]);
            FirstName = Convert.ToString(values[5]);
            LastName = Convert.ToString(values[6]);
            CheckInDate = DateOnly.Parse(values[7]);
            CheckOutDate = DateOnly.Parse(values[8]);
            Rated = bool.Parse(values[9]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = {
                Id.ToString(),
                AccommodationId.ToString(),
                Name,
                Type.ToString(),
                GuestId.ToString(),
                FirstName,
                LastName,
                CheckInDate.ToString(),
                CheckOutDate.ToString(),
                Rated.ToString()
            };
            return csvvalues;
        }
    }
}
