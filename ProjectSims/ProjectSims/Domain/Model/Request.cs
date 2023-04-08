using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectSims.Serializer;
using ProjectSims.Service;

namespace ProjectSims.Domain.Model
{
    public enum RequestState { Approved, Rejected, Waiting }
    public class Request : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public DateOnly ChangeDate { get; set; }
        public RequestState State { get; set; }
        public string OwnerComment { get; set; }    

        public Request() { }

        public Request(int id, int reservationId, DateOnly changeDate, RequestState state, string comment)
        {
            Id = id;
            ReservationId = reservationId;
            ChangeDate = changeDate;
            State = state;
            OwnerComment = comment;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            ChangeDate = DateOnly.ParseExact(values[2], "dd.MM.yyyy");
            State = Enum.Parse<RequestState>(values[3]);
            OwnerComment = values[4];
            InitializeData();
        }

        public string[] ToCSV()
        {
            string[] csvvalues = {
                Id.ToString(),
                ReservationId.ToString(),
                ChangeDate.ToString("dd.MM.yyyy"),
                State.ToString(),
                OwnerComment
            };
            return csvvalues;
        }

        public void InitializeData()
        {
            AccommodationReservationService reservationService = new AccommodationReservationService();
            Reservation = reservationService.GetReservation(ReservationId);
        }
    }
}
