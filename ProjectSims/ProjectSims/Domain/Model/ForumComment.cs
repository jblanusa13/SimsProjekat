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
        public int ForumId { get; set; }
        public Forum Forum { get; set; }
        public int GuestId { get; set; }
        public Guest1 Guest { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Comment { get; set; }
        public bool GuestVisited { get; set; }
        public bool IsGuest { get; set; }
        public string ReportNumber { get; set; }

        public ForumComment()
        {

        }

        public ForumComment(int id, int forumId, Forum forum, int guestid, Guest1 guest, int ownerId, Owner owner, string name, string surname, string comment, bool visited, bool isGuest, string reportNum)
        {
            Id = id;
            ForumId = forumId;
            Forum = forum;
            GuestId = guestid;
            Guest = guest;
            OwnerId = ownerId;
            Owner = owner;
            Name = name;
            Surname = surname;
            Comment = comment;
            GuestVisited = visited;
            IsGuest = isGuest;
            ReportNumber = reportNum;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ForumId = Convert.ToInt32(values[1]);
            GuestId = Convert.ToInt32(values[2]);
            OwnerId = Convert.ToInt32(values[3]);
            Comment = values[4];
            GuestVisited = Convert.ToBoolean(values[5]);
            IsGuest = Convert.ToBoolean(values[6]);
            ReportNumber = values[7];
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ForumId.ToString(),
                GuestId.ToString(),
                OwnerId.ToString(),
                Comment,
                GuestVisited.ToString(),
                IsGuest.ToString(),
                ReportNumber
            };
            return csvValues;
        }
    }
}
