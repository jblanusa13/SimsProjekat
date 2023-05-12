using ProjectSims.FileHandler;
using ProjectSims.Repository;
using ProjectSims.Serializer;
using ProjectSims.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ProjectSims.Domain.Model
{
    public enum AccommodationType { Kuca, Apartman, Koliba };
    public class Accommodation : ISerializable 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdLocation { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public int GuestsMaximum { get; set; } 
        public int MinimumReservationDays { get; set; }
        public int DismissalDays { get; set; }
        public List<string> Images { get; set; }
        public Owner Owner { get; set; }
        public int IdOwner { get; set; }
        public bool Renovated { get; set; } 

        public Accommodation() 
        {
            DismissalDays = 1;
            Images = new List<string>();
        }

        public Accommodation(int id, string name, int idLocation, Location location, AccommodationType type, 
            int guestsMaximum, int minimumReservationDays, int dismissalDays, 
            List<string> images, int idOwner, bool renovated) {
            Id = id;
            Name = name;
            IdLocation = idLocation;
            Location = location;
            Type = type;
            GuestsMaximum = guestsMaximum;
            MinimumReservationDays = minimumReservationDays;
            DismissalDays = dismissalDays;
            Images = images;
            IdOwner = idOwner;
            Renovated = renovated;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            IdLocation = Convert.ToInt32(values[2]);
            Type = Enum.Parse<AccommodationType>(values[3]);
            GuestsMaximum = Convert.ToInt32(values[4]);
            MinimumReservationDays = Convert.ToInt32(values[5]);
            DismissalDays = Convert.ToInt32(values[6]);
            foreach (string image in values[7].Split(","))
            {
                Images.Add(image);
            }
            IdOwner = Convert.ToInt32(values[8]);
            Renovated = Convert.ToBoolean(values[9]);
            InitializeData();
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
            string[] csvValues =
            { 
                Id.ToString(), 
                Name, 
                IdLocation.ToString(), 
                Type.ToString(), 
                GuestsMaximum.ToString(), 
                MinimumReservationDays.ToString(), 
                DismissalDays.ToString(), 
                ImageString, 
                IdOwner.ToString(),
                Renovated.ToString(),
            };
            return csvValues;
        }

        public void InitializeData()
        {
            LocationRepository locationRepository = new LocationRepository();
            Location = locationRepository.GetById(IdLocation);
        }
    }
}
