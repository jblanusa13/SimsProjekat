using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Xps.Serialization;
using System.Xml.Linq;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;
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
        public string ForumComment { get; set; }
        public bool Reserved { get; set; }
        public Request() { }

        public Request(int id, int reservationId, DateOnly changeDate, RequestState state, string comment, bool reserved, AccommodationReservation reservation)
        {
            Id = id;
            ReservationId = reservationId;
            ChangeDate = changeDate;
            State = state;
            ForumComment = comment;
            Reserved = reserved;
            Reservation = reservation;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            ChangeDate = DateOnly.ParseExact(values[2], "dd.MM.yyyy");
            State = Enum.Parse<RequestState>(values[3]);
            ForumComment = values[4];
            Reserved = Convert.ToBoolean(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = {
                Id.ToString(),
                ReservationId.ToString(),
                ChangeDate.ToString("dd.MM.yyyy"),
                State.ToString(),
                ForumComment,
                Reserved.ToString()
            };
            return csvvalues;
        }
    }
}
