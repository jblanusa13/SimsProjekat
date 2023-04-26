using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Serializer;
using ProjectSims.Service;

namespace ProjectSims.Domain.Model
{
    public class AccommodationAndOwnerRating : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public Guest1 Guest { get; set; }
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public int Cleanliness { get; set; }
        public int OwnerFairness { get; set; }
        public int Location { get; set; }
        public int ValueForMoney { get; set; }
        public string AddedComment { get; set; }
        public List<string> Images { get; set; }

        public AccommodationAndOwnerRating()
        {
            Images = new List<string>();
        }

        public AccommodationAndOwnerRating(int id, int guestId, Guest1 guest, int accommodationId, Accommodation accommodation, int cleanliness, int ownerFairness, int location, int valueForMoney, string addedComment, List<string> images)
        {
            Id = id;
            GuestId = guestId;
            Guest = guest;
            AccommodationId = accommodationId;
            Accommodation = accommodation;
            Cleanliness = cleanliness;
            OwnerFairness = ownerFairness;
            Location = location;
            ValueForMoney = valueForMoney;
            AddedComment = addedComment;
            Images = images;
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

            string[] csvvalues = 
            {
                Id.ToString(), 
                GuestId.ToString(), 
                AccommodationId.ToString(), 
                Cleanliness.ToString(),
                OwnerFairness.ToString(), 
                Location.ToString(), 
                ValueForMoney.ToString(),
                AddedComment, 
                ImageString
            };
            return csvvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            Cleanliness = Convert.ToInt32(values[3]);
            OwnerFairness = Convert.ToInt32(values[4]);
            Location = Convert.ToInt32(values[5]);
            ValueForMoney = Convert.ToInt32(values[6]);
            AddedComment = values[7];
            foreach (string image in values[8].Split(","))
            {
                Images.Add(image);
            }

            InitalizeData();
        }

        public void InitalizeData()
        {
            Guest1Service guest1Service = new Guest1Service();
            AccommodationService accommodationService = new AccommodationService();

            Guest = guest1Service.GetGuest1(GuestId);
            Accommodation = accommodationService.GetAccommodation(AccommodationId);
        }

    }
}
