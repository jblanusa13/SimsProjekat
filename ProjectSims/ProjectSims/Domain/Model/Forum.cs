using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model 
{
    public enum ForumStatus { Otvoren, Zatvoren };
    public class Forum : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public Guest1 Guest { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Comment { get; set; }
        public ForumStatus Status { get; set; }
        public bool IsVeryUseful { get; set; }

        public Forum()
        {

        }

        public Forum(int id, int locationId, Location location, string comment, ForumStatus status, int guestId, Guest1 guest, bool isVeryUseful)
        {
            Id = id;
            LocationId = locationId;
            Location = location;
            Comment = comment;
            Status = status;
            GuestId = guestId;
            Guest = guest;
            IsVeryUseful = isVeryUseful;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            LocationId = Convert.ToInt32(values[2]);
            Comment = values[3];
            Status = Enum.Parse<ForumStatus>(values[4]);
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                GuestId.ToString(),
                LocationId.ToString(),
                Comment,
                Status.ToString()
            };
            return csvValues;
        }
    }
}
