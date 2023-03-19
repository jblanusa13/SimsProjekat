using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ProjectSims.Model
{
    public enum AccommodationType { Apartman, Kuca, Koliba };
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public AccommodationType Type { get; set; }
        public int GuestMaximum { get; set; }
        public int MinimumReservationDays { get; set; }
        public int DismissalDays { get; set; }
        public string Images { get; set; }
        public Owner Owner { get; set; }
        public int IdOwner { get; set; }
        public Accommodation() { }

        public Accommodation(int id, string name, string location, AccommodationType type, int guestMaximum, int minimumReservationDays, int dismissalDays, string images, Owner owner, int idOwner) {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            GuestMaximum = guestMaximum;
            MinimumReservationDays = minimumReservationDays;
            DismissalDays = dismissalDays;
            Images = images;
            Owner = owner;
            IdOwner = idOwner;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = values[2];
            Type = Enum.Parse<AccommodationType>(values[3]);
            GuestMaximum = Convert.ToInt32(values[4]);
            MinimumReservationDays = Convert.ToInt32(values[5]);
            DismissalDays = Convert.ToInt32(values[6]);
            Images = values[7];
            IdOwner = Convert.ToInt32(values[8]);
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            { 
                Id.ToString(), 
                Name, 
                Location,
                Type.ToString(),
                GuestMaximum.ToString(),
                MinimumReservationDays.ToString(),
                DismissalDays.ToString(),
                Images,
                IdOwner.ToString() 
            };
            return csvValues;
        }
    }
}
