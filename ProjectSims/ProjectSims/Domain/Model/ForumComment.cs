using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf.fonts.cmaps;
using System.Windows.Documents;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model
{
    public class ForumComment : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public Guest1 Guest { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public string Comment { get; set; }
        public bool GuestVisited { get; set; }

        public ForumComment()
        {

        }

        public ForumComment(int id, int guestid, Guest1 guest, int ownerId, Owner owner, string comment, bool visited)
        {
            Id = id;
            GuestId = guestid;
            Guest = guest;
            OwnerId = ownerId;
            Owner = owner;
            Comment = comment;
            GuestVisited = visited;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            OwnerId = Convert.ToInt32(values[2]);
            Comment = values[3];
            GuestVisited = Convert.ToBoolean(values[4]);
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                GuestId.ToString(),
                OwnerId.ToString(),
                Comment,
                GuestVisited.ToString()
            };
            return csvValues;
        }
    }
}
