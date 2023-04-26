using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int Cleanliness { get; set; }
        public int OwnerFairness { get; set; }
        public int Location { get; set; }
        public int ValueForMoney { get; set; }
        public string AddedComment { get; set; }
        public List<string> Images { get; set; }
        public int RenovationId { get; set; }
        public RenovationRecommendation RenovationRecommendation { get; set; }

        public AccommodationAndOwnerRating()
        {
            Images = new List<string>();
        }

        public AccommodationAndOwnerRating(int id, int reservationId, AccommodationReservation reservation, int cleanliness, int ownerFairness, int location, int valueForMoney, string addedComment, List<string> images, int renovationId, RenovationRecommendation renovationRecommendation)
        {
            Id = id;
            ReservationId = reservationId;
            Reservation = reservation;
            Cleanliness = cleanliness;
            OwnerFairness = ownerFairness;
            Location = location;
            ValueForMoney = valueForMoney;
            AddedComment = addedComment;
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
            ImageString += Images.Last();

            string[] csvvalues = 
            {
                Id.ToString(), 
                ReservationId.ToString(), 
                Cleanliness.ToString(),
                OwnerFairness.ToString(), 
                Location.ToString(), 
                ValueForMoney.ToString(),
                AddedComment, 
                ImageString,
                RenovationId.ToString()
            };
            return csvvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            Cleanliness = Convert.ToInt32(values[2]);
            OwnerFairness = Convert.ToInt32(values[3]);
            Location = Convert.ToInt32(values[4]);
            ValueForMoney = Convert.ToInt32(values[5]);
            AddedComment = values[6];
            foreach (string image in values[7].Split(","))
            {
                Images.Add(image);
            }
            RenovationId = Convert.ToInt32(values[8]);

            InitalizeData();
        }

        public void InitalizeData()
        {
            AccommodationReservationRepository reservationRepository = new AccommodationReservationRepository();
            RenovationRecommendationRepository recommendationRepository = new RenovationRecommendationRepository();

            Reservation = reservationRepository.Get(ReservationId);
            RenovationRecommendation = recommendationRepository.Get(RenovationId);
        }

    }
}
