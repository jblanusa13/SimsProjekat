using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ProjectSims.Domain.Model
{
    public class GuestAccommodation : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public int ReservationId { get; set; }
        public string Name { get; set; }
        public AccommodationType Type { get; set; }
        public int GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public bool Rated { get; set; }
        public List<string> Images { get; set; }
        public GuestAccommodation() 
        {
            Images = new List<string>();
        }
        public GuestAccommodation(int id, int accommodationId, int reservationId, string name, AccommodationType type, int guestId,
            string firstName, string lastName, DateOnly checkInDate, DateOnly checkOutDate, bool rated, List<string> images)
        {
            Id = id;
            AccommodationId = accommodationId;
            ReservationId = reservationId;
            Name = name;
            Type = type;
            GuestId = guestId;
            FirstName = firstName;
            LastName = lastName;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Rated = rated;
            Images = images;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            ReservationId = Convert.ToInt32(values[2]);
            Name = Convert.ToString(values[3]);
            Type = Enum.Parse<AccommodationType>(values[4]);
            GuestId = Convert.ToInt32(values[5]);
            FirstName = Convert.ToString(values[6]);
            LastName = Convert.ToString(values[7]);
            CheckInDate = DateOnly.ParseExact(values[8], "dd.MM.yyyy");
            CheckOutDate = DateOnly.ParseExact(values[9], "dd.MM.yyyy");
            Rated = bool.Parse(values[10]);
            foreach (string image in values[10].Split(","))
            {
                Images.Add(image);
            }
        }

        public string[] ToCSV()
        {
            string ImageString = "";
            foreach (string image in Images)
            {
                if (image != Images.Last())
                {
                    ImageString += image + ",";
                }
            }
            ImageString += Images.Last();
            string[] csvvalues = {
                Id.ToString(),
                AccommodationId.ToString(),
                ReservationId.ToString(),
                Name,
                Type.ToString(),
                GuestId.ToString(),
                FirstName,
                LastName,
                CheckInDate.ToString("dd.MM.yyyy"),
                CheckOutDate.ToString("dd.MM.yyyy"),
                Rated.ToString(),
                ImageString
            };
            return csvvalues;
        }
    }
}
