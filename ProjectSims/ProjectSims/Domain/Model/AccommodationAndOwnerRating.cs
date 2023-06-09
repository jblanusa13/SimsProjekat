using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;
using ProjectSims.Serializer;
using ProjectSims.Service;

namespace ProjectSims.Domain.Model
{
    public class AccommodationAndOwnerRating : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public double Cleanliness { get; set; }
        public double OwnerFairness { get; set; }
        public double Location { get; set; }
        public double ValueForMoney { get; set; }
        public string Comment { get; set; }
        public List<string> Images { get; set; }
        public int RenovationId { get; set; }
        public RenovationRecommendation RenovationRecommendation { get; set; }

        public AccommodationAndOwnerRating()
        {
            Images = new List<string>();
        }

        public AccommodationAndOwnerRating(int id, int reservationId, AccommodationReservation reservation, double cleanliness, double ownerFairness, double location, double valueForMoney, string addedComment, List<string> images, int renovationId, RenovationRecommendation renovationRecommendation)
        {
            Id = id;
            ReservationId = reservationId;
            Reservation = reservation;
            Cleanliness = cleanliness;
            OwnerFairness = ownerFairness;
            Location = location;
            ValueForMoney = valueForMoney;
            Comment = addedComment;
            Images = images;
            RenovationId = renovationId;
            RenovationRecommendation = renovationRecommendation;
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
            ImageString += Images.LastOrDefault();

            string[] csvvalues = 
            {
                Id.ToString(), 
                ReservationId.ToString(), 
                Cleanliness.ToString(),
                OwnerFairness.ToString(), 
                Location.ToString(), 
                ValueForMoney.ToString(),
                Comment, 
                ImageString,
                RenovationId.ToString()
            };
            return csvvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            Cleanliness = Convert.ToDouble(values[2]);
            OwnerFairness = Convert.ToDouble(values[3]);
            Location = Convert.ToDouble(values[4]);
            ValueForMoney = Convert.ToDouble(values[5]);
            Comment = values[6];
            foreach (string image in values[7].Split(","))
            {
                Images.Add(image);
            }
            RenovationId = Convert.ToInt32(values[8]);
        }
    }
}
