using ProjectSims.Repository;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public class GuestRating : ISerializable
    {
        public int Id { get; set; }
        public int CleanlinessRating { get; set; }
        public int RespectingRulesRating { get; set; }
        public int TidinessRating { get; set; }
        public int CommunicationRating { get; set; }
        public string Comment { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public DateOnly TimeStamp { get; set; }
        public int GuestId { get; set; }


        public GuestRating() { }

        public GuestRating(int id, int cleanlinessRating, int respectingRulesRating, int tidinessRating, int communicationRating, string comment, int reservationId, AccommodationReservation accommodationReservation, DateOnly timeStamp, int guestId)
        {
            Id = id;
            CleanlinessRating = cleanlinessRating;
            RespectingRulesRating = respectingRulesRating;
            TidinessRating = tidinessRating;
            CommunicationRating = communicationRating;
            Comment = comment;
            ReservationId = reservationId;
            Reservation = accommodationReservation;
            TimeStamp = timeStamp;
            GuestId = guestId;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            CleanlinessRating = Convert.ToInt32(values[1]);
            RespectingRulesRating = Convert.ToInt32(values[2]);
            TidinessRating = Convert.ToInt32(values[3]);
            CommunicationRating = Convert.ToInt32(values[4]);
            Comment = values[5];
            ReservationId = Convert.ToInt32(values[6]);
            TimeStamp = DateOnly.ParseExact(values[7], "dd.MM.yyyy");
            GuestId = Convert.ToInt32(values[8]);
            InitializeData();
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                CleanlinessRating.ToString(),
                RespectingRulesRating.ToString(),
                TidinessRating.ToString(),
                CommunicationRating.ToString(),
                Comment,
                ReservationId.ToString(),
                TimeStamp.ToString(),
                GuestId.ToString()};
            return csvValues;
        }

        public void InitializeData()
        {
            AccommodationReservationRepository reservationRepository = new AccommodationReservationRepository();
            Reservation = reservationRepository.GetById(ReservationId);
        }
    }
}
