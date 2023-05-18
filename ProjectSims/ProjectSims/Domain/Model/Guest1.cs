using ProjectSims.Repository;
using ProjectSims.Serializer;
using System;

namespace ProjectSims.Domain.Model
{
    public class Guest1 : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SuperGuestId { get; set; }
        public SuperGuest SuperGuest { get; set; }

        public Guest1() { }
        public Guest1(int id, string name, string surname, string address, string email, int userId, int superGuestId, SuperGuest superGuest)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Address = address;
            Email = email;
            UserId = userId;
            SuperGuestId = superGuestId;
            SuperGuest = superGuest;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Address = values[3];
            Email = values[4];
            UserId = Convert.ToInt32(values[5]);
            SuperGuestId = Convert.ToInt32(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = 
            { 
                Id.ToString(), 
                Name, 
                Surname, 
                Address, 
                Email, 
                UserId.ToString(),
                SuperGuestId.ToString()
            };
            return csvvalues;
        }
    }
}
