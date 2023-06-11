using Org.BouncyCastle.Asn1.Ocsp;
using ProjectSims.Repository;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.util;

namespace ProjectSims.Domain.Model
{
    public class NotificationOwnerGuest : ISerializable
    {
        public int Id { get; set; }
        public int Guest1Id { get; set; }
        public Guest1 Guest1 { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
        public int ForumId { get; set; }
        public Forum Forum { get; set; }
        public string ContentNotification { get; set; }
        public NotificationOwnerGuest()
        {
        }
        public NotificationOwnerGuest(int id, int guest1Id, Guest1 guest1, int ownerId, Owner owner,
            int requestId, Request request, int forumId, Forum forum, string content)
        {
            Id = id;
            Guest1Id = guest1Id;
            Guest1 = guest1;
            OwnerId = ownerId;
            Owner = owner;
            RequestId = requestId;
            Request = request;
            ForumId = forumId;
            Forum = forum;
            ContentNotification = content;
        }

        public string[] ToCSV()
        {

            string[] csvvalues = {
                Id.ToString(),
                Guest1Id.ToString(),
                OwnerId.ToString(),
                RequestId.ToString(),
                ForumId.ToString(),
                ContentNotification
            };
            return csvvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest1Id = Convert.ToInt32(values[1]);
            OwnerId = Convert.ToInt32(values[2]);
            RequestId = Convert.ToInt32(values[3]);
            ForumId = Convert.ToInt32(values[4]);
            ContentNotification = values[5];
        }
    }
}
