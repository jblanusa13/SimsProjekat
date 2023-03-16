using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectSims.Serializer;

namespace ProjectSims.Model
{
    public class Guest1 : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public Guest1() { }

        public Guest1(int id, string username, string password, string firstName, string lastName, string address, string email)
        {
            Id = id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            FirstName = values[3];
            LastName = values[4];
            Address = values[5];
            Email = values[6];
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { 
                Id.ToString(), 
                Username,
                Password,
                FirstName, 
                LastName,
                Address,
                Email };
            return csvvalues;
        }
    }
}
