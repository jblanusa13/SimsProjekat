using ProjectSims.Repository;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public class OwnerComment : ISerializable
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public string Comment { get; set; }
        public OwnerComment() 
        {

        }

        public OwnerComment(int id, int ownerId, Owner owner, string comment)
        {
            Id = id;
            OwnerId = ownerId;
            Owner = owner;
            Comment = comment;
        }
        public string[] ToCSV()
        {
            string[] csvvalues = { 
                Id.ToString(),
                OwnerId.ToString(), 
                Comment
            };
            return csvvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OwnerId = Convert.ToInt32(values[1]);
            Comment = values[2];
        }
    }
}
