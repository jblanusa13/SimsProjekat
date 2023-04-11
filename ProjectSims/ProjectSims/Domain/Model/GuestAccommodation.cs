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
        public GuestAccommodation(int id, int accommodationId, string name, AccommodationType type, int guestId,
            string firstName, string lastName, DateOnly checkInDate, DateOnly checkOutDate, bool rated, List<string> images)
        {
            Id = id;
            AccommodationId = accommodationId;
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
            Name = Convert.ToString(values[2]);
            Type = Enum.Parse<AccommodationType>(values[3]);
            GuestId = Convert.ToInt32(values[4]);
            FirstName = Convert.ToString(values[5]);
            LastName = Convert.ToString(values[6]);
            CheckInDate = DateOnly.ParseExact(values[7], "dd.MM.yyyy");
            CheckOutDate = DateOnly.ParseExact(values[8], "dd.MM.yyyy");
            Rated = bool.Parse(values[9]);
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

        public static implicit operator GuestAccommodation((string name, string firstName, string lastName, List<string> images) info)
        {
            return new GuestAccommodation { Name = info.name, FirstName = info.firstName, LastName = info.lastName, Images = info.images };
        }
    }
}
